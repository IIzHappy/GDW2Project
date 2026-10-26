using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Spawner", menuName = "New Enemy Spawner")]
[System.Serializable]
public class EnemySpawner : ScriptableObject
{
    [SerializeField] GameObject _enemy;

    public virtual void SpawnEnemy(Vector3 position)
    {
        Instantiate(_enemy, position, Quaternion.identity, enemyManager.Instance.gameObject.transform);
    }
}
