using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    public GameObject player;
    private SpriteRenderer sr;
    private bool faceLeft = true;

    public float moveSpeed;
    public float reach; //distance from player to do dmg

    protected Vector2 toPlayer;
    protected bool inRange = false;

    public int maxHealth;
    protected int health;

    public int damage;
    public float attackDelay; //Time between each attack
    float attackTimer = 0;
    protected bool hasAttacked = false;
    public float pauseTime; //Time before enemy starts chasing player after pausing
    float pauseTimer = 0;


    void Awake()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        FindPlayer();
        if (toPlayer.x > 0 && faceLeft)
        {
            faceLeft = false;
            sr.flipX = true;
        }
        else if (toPlayer.x < 0 && !faceLeft)
        {
            faceLeft = true;
            sr.flipX = false;
        }

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
            }
            else if (!hasAttacked && pauseTimer <= 0)
            {
                ChasePlayer();
            }
            else if (pauseTimer > 0)
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
        player.GetComponent<PlayerController>().TakeDamage(damage);
    }

    public virtual void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public virtual void Heal(int healing)
    {
        health = Mathf.Clamp(health + healing, health, maxHealth);
    }
}
