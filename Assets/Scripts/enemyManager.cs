using System.Threading;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject enemyManagerGO;
   public float timer;
    float waveNum = 1f;
    public float universalTimer;
    public GameObject Player;
    public bool waveChange = false;

    private void Update()
    {
        timer -= Time.deltaTime;
        universalTimer += Time.deltaTime;
        if (timer < 0f && waveChange == false)
        {
            SpawnEnemy();
            timer = spawnTimer;
        }
        if (universalTimer > 10f)
        {
            waveChange = true;
            if (waveChange == true && enemyManagerGO.transform.childCount == 0)
            {
                spawnTimer = spawnTimer * 0.9f;
                waveChange = false;
                universalTimer = 0f;
            }

        }
    }

    private void SpawnEnemy()
    {
        Vector3 position = GenerateSpawnPosition();
          


        position += Player.transform.position;
        GameObject spawnedEnemy = Instantiate(enemy1);
        spawnedEnemy.transform.position = position;
        spawnedEnemy.GetComponent<EnemyController>().player = Player;
        spawnedEnemy.transform.parent = transform;
       
    }

    private Vector3 GenerateSpawnPosition()
    {
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
