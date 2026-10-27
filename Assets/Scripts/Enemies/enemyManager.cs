using NUnit.Framework;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;

public class enemyManager : MonoBehaviour
{
    public static enemyManager Instance;

    public bool _spawning = false;

    float _spawnTimer;
    float _spawnDelay;
    float _waveDelay;
    Queue<EnemyWave> _enemyWaves = new Queue<EnemyWave>();
    EnemySpawner _waveEnemy;
    int _waveSpawns;
    int _waveSpawned;

    LayerMask _colCheckLayer;
    Collider2D _colCheck;
    [SerializeField] float _spawnDistance = 10f;
    
    public Transform _player;
    public AudioClip levelStart;

    void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this) Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        AudioSource.PlayClipAtPoint(levelStart, gameObject.transform.position);
        _colCheckLayer = LayerMask.GetMask("Water");
    }

    public void ClearManager()
    {
        _spawnTimer = 0;
        _waveDelay = 0;
        _enemyWaves.Clear();
        _waveEnemy = null;
        _waveSpawns = 0;
        _waveSpawned = 0;

        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void LoadLevel(List<EnemyWave> enemyWaves)
    {
        foreach (EnemyWave wave in enemyWaves)
        {
            _enemyWaves.Enqueue(wave);
        }
        LoadWave();
    }

    public void LoadWave()
    {
        EnemyWave wave = _enemyWaves.Dequeue();
        _waveEnemy = wave._enemy;
        _waveSpawned = 0;
        _waveSpawns = wave._count;
        _waveDelay = wave._waveDelay - _spawnTimer;
        _spawnTimer = 0;
        _spawnDelay = wave._spawnDelay;
    }

    private void Update()
    {
        if (!_spawning) return;

        //delay between waves
        if (_waveDelay > 0)
        {
            _waveDelay -= Time.deltaTime;
            return;
        }

        //spawning delay
        _spawnTimer += Time.deltaTime;

        if (_spawnTimer >= _spawnDelay)
        {
            _spawnTimer -= _spawnDelay;

            if (_waveSpawned > _waveSpawns)
            {
                if (_player.childCount == 0)
                {
                    if (_enemyWaves.Count == 0) GameManager.Instance.LoadLevel();
                    else LoadWave();
                }
            }
            else
            {
                SpawnEnemy(_waveEnemy);
                _waveSpawned++;
            }
        }
    }

    void SpawnEnemy(EnemySpawner spawner)
    {
        spawner.SpawnEnemy(GenerateSpawnPosition());
    }

    private Vector3 GenerateSpawnPosition()
    {
        const int maxAttempts = 100;
        for (int i = 0; i < maxAttempts; i++)
        {
            float radians = Random.Range(0, 360) * Mathf.Deg2Rad;
            Vector3 position = new Vector3(Mathf.Sin(radians), Mathf.Cos(radians), 0) * _spawnDistance + _player.position;
            
            _colCheck = Physics2D.OverlapCircle(position, 3, _colCheckLayer);

            if(_colCheck != null) return position;
            
        }
        Debug.LogWarning("Couldn't find spawn pos");
        return Vector3.zero;
    }
}
