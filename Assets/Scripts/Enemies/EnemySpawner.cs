using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    public void SpawnEnemy(Vector3 position)
    {
        Instantiate(_enemy, position, Quaternion.identity, enemyManager.Instance.gameObject.transform);
    }
}
