using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public float _waveDelay = 1;
    public float _spawnDelay = 1;
    public EnemySpawner _enemy;
    [Min (1)]public int _count = 10;
}
