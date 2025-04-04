using UnityEngine;

public class BossTwoController : EnemyController
{
    public GameObject bulletPrefab;
    public GameObject enemyM;
    public AudioClip boss2Attack;

    protected override void Attack()
    {
        float angle = Mathf.Atan2(toPlayer.normalized.y, toPlayer.normalized.x) * Mathf.Rad2Deg;
        GameObject temp = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        temp.GetComponent<PushController>().direction = toPlayer.normalized;
        AudioSource.PlayClipAtPoint(boss2Attack, gameObject.transform.position);
    }

    private void OnDestroy()
    {
        //enemyM.GetComponent<enemyManager>().bossKilled = true;
        Debug.Log("boss is dead");
    }
}
