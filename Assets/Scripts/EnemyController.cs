using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public GameObject player;

    public float moveSpeed;
    public float reach; //distance from player to do dmg

    public Vector2 toPlayer;
    public bool inRange = false;

    public int maxHealth;
    private int health;

    public int damage;
    public float attackDelay; //Time between each attack
    float attackTimer = 0;
    bool hasAttacked = false;
    public float pauseTime; //Time before enemy starts chasing player after pausing
    float pauseTimer = 0;


    void Awake()
    {
        health = maxHealth;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }


    void FixedUpdate()
    {
        FindPlayer();
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

    protected virtual void FindPlayer()
    {
        toPlayer = player.transform.position - transform.position;
        inRange = (toPlayer.sqrMagnitude <= reach);
    }

    public void ChasePlayer()
    {
        rb.AddForce(toPlayer.normalized * moveSpeed);
    }

    protected virtual void Attack()
    {
        Debug.Log(gameObject.name.ToString() + " goose attack - " + damage);
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
