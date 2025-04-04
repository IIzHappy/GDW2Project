using UnityEngine;

public class ExplosiveGoose : EnemyController
{
    public GameObject explosion;
    public AudioClip explosionClip;
    public override void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Debug.Log("jhsdkgjdfng");
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(explosionClip, gameObject.transform.position);
            Destroy(this.gameObject);
            Debug.Log("2");
        }
    }
}
