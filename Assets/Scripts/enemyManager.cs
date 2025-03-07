using System.Threading;
using UnityEngine;

public class enemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;
    [SerializeField] GameObject enemy3;
    [SerializeField] GameObject enemy4;
    [SerializeField] GameObject boss1;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] GameObject enemyManagerGO;
   public float timer;
    public float timer2;
   public float waveNum = 1f;
    public float universalTimer;
    public GameObject Player;
    private PlayerController pc;
    private GameObject Boss;
    public bool waveChange = false;
    private bool bossSpawned = false;
    public bool bossKilled = false;

    void Start()
    {
        pc = Player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        timer2 -= Time.deltaTime;
        universalTimer += Time.deltaTime;
        if( waveNum == 1)
        {
            waveOneSpawning();
        }
        if( waveNum == 2)
        {
            waveTwoSpawning();
        }
        if (waveNum == 3)
        {
            waveThreeSpawning();
        }
        if (waveNum == 4)
        {
            waveFourSpawning();
        }
        if (waveNum == 5)
        {
            waveFiveSpawning();
        }
        if (waveNum == 6)
        {
            waveSixSpawning();
        }
        if (waveNum == 7)
        {
            waveSevenSpawning();
        }
        if (waveNum == 8)
        {
            waveEightSpawning();
        }
        if (waveNum == 9)
        {
            waveNineSpawning();
        }
        if (waveNum == 10)
        {
            waveTenSpawning();
        }
        if (boss1 == null)
        {
            bossKilled = true;
        }
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
                spawnTimer = spawnTimer +0f;
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
            SpawnBossOne();
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
                spawnTimer = spawnTimer + 1.1f;
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
