using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public GameObject player;

    [SerializeField] public float moveSpeed;
    [SerializeField] float reach; //distance from player to do dmg

    Vector2 toPlayer;
    bool inRange = false;

    [SerializeField] public int maxHealth;
    private int health;

    [SerializeField] int damage;
    [SerializeField] float attackDelay; //Time between each attack
    float attackTimer = 0;
    bool hasAttacked = false;
    [SerializeField] float pauseTime; //Time before enemy starts chasing player after pausing
    float pauseTimer = 0;


    void Awake()
    {
        health = maxHealth;
    }


    void FixedUpdate()
    {
        toPlayer = player.transform.position - transform.position;
        inRange = (toPlayer.sqrMagnitude <= reach);
        //if player is within range or goose is paused
        if (inRange)
        {
            if (pauseTimer > 0)
            {
                pauseTimer = 0;
                hasAttacked = true;
            }
            
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
            
            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
        }
        else
        {
            if (hasAttacked && pauseTimer <= 0)
            {
                pauseTimer = pauseTime;
                hasAttacked = false;
            }else if (!hasAttacked && pauseTimer <= 0)
            {
                ChasePlayer();
            }else if (pauseTimer > 0)
            {
                pauseTimer -= Time.deltaTime;
            }
        }
    }

    public void ChasePlayer()
    {
        rb.AddForce(toPlayer.normalized * moveSpeed);
    }

    protected virtual void Attack()
    {
        Debug.Log("enemy attack");
        player.GetComponent<PlayerController>().TakeDamage(damage);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
