using UnityEngine;

public class ExplosiveGoose : EnemyController
{
    public GameObject explosion;
    public override void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Debug.Log("jhsdkgjdfng");
            Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            Debug.Log("2");
        }
    }
}
