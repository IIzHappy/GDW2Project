using UnityEngine;

public class HealerGoose : EnemyController
{
    public GameObject heal;

    protected override void Attack()
    {
        Instantiate(heal, gameObject.transform);
    }
    public override void Heal(int healing)
    {
        //Can't heal
    }
}
