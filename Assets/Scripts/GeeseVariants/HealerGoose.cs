using UnityEngine;

public class HealerGoose : EnemyController
{
    public GameObject heal;
    public bool changed = false;
    public AudioClip enemyHealSound;

    private void Update()
    {
        changed = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (pauseTimer > 0)
        {
            pauseTimer = 0;
            hasAttacked = true;
        }
        if (collision.gameObject.tag == "Enemy")
            {
                

                if (!hasAttacked && attackTimer <= 0)
                {
                    attackTimer = attackDelay;
                }
                else if (attackTimer - Time.deltaTime <= 0)
                {
                    Attack();
                    hasAttacked = true;
                    attackTimer = attackDelay;
                }

            if (attackTimer > 0 && !changed)
            {
                attackTimer -= Time.deltaTime;
                changed = true;
            }
        }
        }

    protected override void Attack()
    {
        Instantiate(heal, gameObject.transform);
        AudioSource.PlayClipAtPoint(enemyHealSound, gameObject.transform.position);
        
    }
    public override void Heal(int healing)
    {
        //Can't heal
    }
}
