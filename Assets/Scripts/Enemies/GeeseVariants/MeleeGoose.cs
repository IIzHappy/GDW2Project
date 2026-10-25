using UnityEngine;

public class MeleeGoose : EnemyController
{
    protected override void Attack()
    {
        //need to add animation
        player.GetComponent<PlayerController>().TakeDamage(damage);
    }
}
