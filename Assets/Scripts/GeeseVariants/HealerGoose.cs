using UnityEngine;

public class HealerGoose : EnemyController
{
    public GameObject heal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasAttacked == false && collision.gameObject.tag == "Enemy")
        {
            Attack();
            hasAttacked = true;
        }
    }

    protected override void Attack()
    {
        Instantiate(heal, gameObject.transform);
        
    }
    public override void Heal(int healing)
    {
        //Can't heal
    }
}
