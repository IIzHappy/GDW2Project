using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyManager : MonoBehaviour
{
    public static enemyManager Instance;
    float _spawnTimer;
    float _spawnDelay;
    float _waveDelay;
    Queue<EnemyWave> _enemyWaves;
    EnemySpawner _waveEnemy;
    int _waveSpawns;
    int _waveSpawned;

    [SerializeField] float _spawnDistance = 10f;

    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
    [SerializeField] GameObject boss1;
    [SerializeField] GameObject boss2;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] LayerMask _colCheckLayers;
    Collider2D _colCheck;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject enemyManagerGO;
    [SerializeField] GameObject winScreen;
    public float timer;
    public float timer2;
    public float waveNum = 1f;
    public float universalTimer;
    public GameObject Player;
    private PlayerController pc;
    public GameObject Boss;
    public GameObject Boss2;
    public bool waveChange = false;
    private bool bossSpawned = false;
    public bool bossKilled = false;
    public int lvlNum;
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
        pc = PlayerController.Instance;
        Player = pc.gameObject;
        AudioSource.PlayClipAtPoint(levelStart, gameObject.transform.position);
        _colCheckLayers = LayerMask.GetMask("Water");
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
        //delay between waves
        if (_waveDelay > 0) _waveDelay -= Time.deltaTime;
        else
        {
            //spawning delay
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= _spawnDelay)
            {
                _spawnTimer -= _spawnDelay;

                SpawnEnemy(_waveEnemy);
                _waveSpawned++;

                if (_waveSpawned >= _waveSpawns)
                {
                    if (_enemyWaves.Count == 0) GameManager.Instance.LoadLevel();
                    else LoadWave();
                }
            }
        }
        timer -= Time.deltaTime;
        timer2 -= Time.deltaTime;
        universalTimer += Time.deltaTime;
        


        if (Boss == null && Boss2 == null)
        {
            bossKilled = true;
        }
        if (waveNum == 11)
        {
            if (lvlNum == 3)
            {
                winScreen.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                SceneManager.LoadScene(lvlNum + 1);
            }
        }
    }

    void SpawnEnemy(EnemySpawner spawner)
    {
        GenerateSpawnPos();
    }

    private Vector3 GenerateSpawnPos()
    {
        const int maxAttempts = 100;
        for (int i = 0; i < maxAttempts; i++)
        {
            float radians = UnityEngine.Random.Range(0, 360) * Mathf.Deg2Rad;
            Vector3 position = new Vector3(Mathf.Sin(radians), Mathf.Cos(radians), 0) * _spawnDistance + Player.transform.position;
            
            _colCheck = Physics2D.OverlapCircle(position, 3, _colCheckLayers);

            if(_colCheck != null) return position;
            
        }
        Debug.LogWarning("Couldn't find spawn pos");
        return Vector3.zero;
    }



 private void waveOneSpawning()
    {
        if (timer < 0f && waveChange == false)
        {
            SpawnEnemyOne();
            timer = spawnTimer;
        }
        if (universalTimer > 10f)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer - 0.1f;
                waveChange = false;
                universalTimer = 0f;
                waveNum++;
                pc.StartUpgrade();
            }

        }
    }

    private void waveTwoSpawning()
    {
        if (timer < 0f && waveChange == false)
        {
            SpawnEnemyOne();
            timer = spawnTimer;
        }
        if (universalTimer > 20f)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer + 1.1f;
                waveChange = false;
                universalTimer = 0f;
                waveNum++;
                pc.StartUpgrade();
            }

        }
    }

    private void waveThreeSpawning()
    {
        if (timer < 0f && waveChange == false)
        {
            SpawnEnemyTwo();
            timer = spawnTimer;
        }
        if (universalTimer > 25f)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer - 0.5f;
                waveChange = false;
                universalTimer = 0f;
                waveNum++;
                pc.StartUpgrade();
            }

        }
    }

    private void waveFourSpawning()
    {
        if (timer < 0f && waveChange == false)
        {
            SpawnEnemyTwo();
            timer = spawnTimer;
        }
        if (timer2 < spawnTimer / 4f && waveChange == false)
        {
            SpawnEnemyOne();
            timer2 = spawnTimer;
        }
        if (universalTimer > 30f)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer - 0.5f;
                waveChange = false;
                universalTimer = 0f;
                waveNum++;
                pc.StartUpgrade();
            }

        }
    }

    private void waveFiveSpawning()
    {

        if (timer < 0f && waveChange == false)
        {
            SpawnEnemyTwo();
            timer = spawnTimer;
        }
        if (timer2 < spawnTimer / 2f && waveChange == false)
        {
            SpawnEnemyOne();
            timer2 = spawnTimer;
        }
        if (universalTimer > 15f)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer + 1.0f;
                waveChange = false;
                universalTimer = 0f;
                waveNum++;
                pc.StartUpgrade();
            }

        }

    }

    private void waveSixSpawning()
    {
        if (timer < 0f && waveChange == false)
        {
            SpawnEnemyThree();
            timer = spawnTimer;
        }
        if (universalTimer > 20f)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer + 0f;
                waveChange = false;
                universalTimer = 0f;
                waveNum++;
                pc.StartUpgrade();
            }

        }

    }

    private void waveSevenSpawning()
    {
        if (timer < 0f && waveChange == false)
        {
            SpawnEnemyThree();
            timer = spawnTimer;
        }
        if (timer2 < spawnTimer / 2f && waveChange == false)
        {
            SpawnEnemyOne();
            timer2 = spawnTimer;
        }
        if (universalTimer > 30f)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer - 1.0f;
                waveChange = false;
                universalTimer = 0f;
                waveNum++;
                pc.StartUpgrade();
            }

        }
    }

    private void waveEightSpawning() {
        if (timer < 0f && waveChange == false)
        {
            SpawnEnemyFour();
            timer = spawnTimer;
        }
        if (timer2 < spawnTimer / 4f && waveChange == false)
        {
            SpawnEnemyTwo();
            timer2 = spawnTimer;
        }
        if (universalTimer > 20f)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer + 1.0f;
                waveChange = false;
                universalTimer = 0f;
                waveNum++;
                pc.StartUpgrade();
            }

        }
    }

    private void waveNineSpawning()
    {
        if (timer < 0f && waveChange == false)
        {
            SpawnEnemyFour();
            SpawnEnemyThree();
            SpawnEnemyTwo();
            timer = spawnTimer;
        }
        if (timer2 < spawnTimer / 2f && waveChange == false)
        {
            SpawnEnemyOne();
            timer2 = spawnTimer;
        }
        if (universalTimer > 15f)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer +2f;
                waveChange = false;
                universalTimer = 0f;
                waveNum++;
                pc.StartUpgrade();
            }

        }

    }

    private void waveTenSpawning()
    {
        if (bossSpawned == false)
        {
            SpawnBosses();
            bossSpawned = true;
        }
        if (timer < 0f && waveChange == false)
        {
            SpawnEnemyFour();
            SpawnEnemyTwo();
            timer = spawnTimer;
        }
        if (timer2 < spawnTimer / 4f && waveChange == false)
        {
            SpawnEnemyOne();
            SpawnEnemyThree();
            timer2 = spawnTimer;
        }
        if (universalTimer > 40f && bossKilled == true)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer + 0f;
                waveChange = false;
                universalTimer = 0f;
                waveNum++;
            }

        }

    }



    // spawning logic
    private void SpawnEnemyOne()
    {
        Vector3 position = GenerateSpawnPosition();
          


        position += Player.transform.position;
        GameObject spawnedEnemy = Instantiate(enemy1);
        spawnedEnemy.transform.position = position;
        spawnedEnemy.GetComponent<EnemyController>().player = Player;
        spawnedEnemy.transform.parent = transform;
       
    }

    private void SpawnEnemyTwo()
    {
        Vector3 position = GenerateSpawnPosition();



        position += Player.transform.position;
        GameObject spawnedEnemy = Instantiate(enemy2);
        spawnedEnemy.transform.position = position;
        spawnedEnemy.GetComponent<EnemyController>().player = Player;
        spawnedEnemy.transform.parent = transform;

    }

    private void SpawnEnemyThree()
    {
        Vector3 position = GenerateSpawnPosition();



        position += Player.transform.position;
        GameObject spawnedEnemy = Instantiate(enemy3);
        spawnedEnemy.transform.position = position;
        spawnedEnemy.GetComponent<EnemyController>().player = Player;
        spawnedEnemy.transform.parent = transform;

    }

    private void SpawnEnemyFour()
    {
        Vector3 position = GenerateSpawnPosition();



        position += Player.transform.position;
        GameObject spawnedEnemy = Instantiate(enemy4);
        spawnedEnemy.transform.position = position;
        spawnedEnemy.GetComponent<EnemyController>().player = Player;
        spawnedEnemy.transform.parent = transform;
    }

    private void SpawnBossOne()
    {
        Vector3 position = GenerateSpawnPosition();



        position += Player.transform.position;
        GameObject spawnedEnemy = Instantiate(boss1);
        Boss = spawnedEnemy;
        spawnedEnemy.transform.position = position;
        spawnedEnemy.GetComponent<EnemyController>().player = Player;
        spawnedEnemy.transform.parent = transform;
        Boss.GetComponent<BossOneController>().enemyM = gameObject;
        Boss.GetComponent<BossOneController>().player = Player;
    }

    private void SpawnBossTwo()
    {
        Vector3 position = GenerateSpawnPosition();



        position += Player.transform.position;
        GameObject spawnedEnemy = Instantiate(boss2);
        Boss2 = spawnedEnemy;
        spawnedEnemy.transform.position = position;
        spawnedEnemy.GetComponent<EnemyController>().player = Player;
        spawnedEnemy.transform.parent = transform;
        Boss2.GetComponent<BossTwoController>().enemyM = gameObject;
        Boss2.GetComponent<BossTwoController>().player = Player;
    }

    private void SpawnBosses()
    {
        if (lvlNum == 1)
        {
            SpawnBossOne();
        }

        if(lvlNum == 2)
        {
            SpawnBossTwo();
        }

        if(lvlNum == 3)
        {
            SpawnBossOne();
            SpawnBossTwo();
        }
    }

    private Vector3 GenerateSpawnPosition()
    {
        return GenerateSpawnPos();
        Vector3 position = new Vector3();

        float n = UnityEngine.Random.value > 0.5f ? -1f : 1f;
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * n;
        }else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * n;
        }
        position.z = 0;

        return position;
    }
}
