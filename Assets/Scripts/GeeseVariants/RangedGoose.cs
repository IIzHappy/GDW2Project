using UnityEngine;

public class RangedGoose : EnemyController
{
    public GameObject bulletPrefab;

    protected override void Attack()
    {
        float angle = Mathf.Atan2(toPlayer.x, toPlayer.y) * Mathf.Rad2Deg;
        GameObject temp = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        temp.GetComponent<ProjectileController>().direction = toPlayer.normalized;
    }
}
