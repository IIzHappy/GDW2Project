using UnityEngine;

[System.Serializable]
public class EnemyWave : MonoBehaviour
{
    public int _waveDelay;
    public int _spawnDelay;
    public EnemySpawner _enemy;
    public int _count;
}
