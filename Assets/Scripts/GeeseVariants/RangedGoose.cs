using UnityEngine;

public class RangedGoose : EnemyController
{
    public GameObject bulletPrefab;
    public AudioClip enemyBulletSound;

    protected override void Attack()
    {
        float angle = Mathf.Atan2(toPlayer.normalized.y, toPlayer.normalized.x) * Mathf.Rad2Deg;
        GameObject temp = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        temp.GetComponent<ProjectileController>().direction = toPlayer.normalized;
        AudioSource.PlayClipAtPoint(enemyBulletSound, gameObject.transform.position);
    }
}
