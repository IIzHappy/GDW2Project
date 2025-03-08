using UnityEngine;

public class ExplosiveGoose : EnemyController
{
    public override void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            //add explosion
            Destroy(this.gameObject);
        }
    }
}
