using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject player;

    [SerializeField] public float moveSpeed;
    [SerializeField] float reach; //distance from player to do dmg

    Vector2 toPlayer;
    bool inRange = false;

    [SerializeField] int damage;
    [SerializeField] float attackDelay; //Time between each attack
    float attackTimer = 0;
    bool hasAttacked = false;
    [SerializeField] float pauseTime; //Time before enemy starts chasing player after pausing
    float pauseTimer = 0;


    void Awake()
    {
        
    }


    void FixedUpdate()
    {
        toPlayer = player.transform.localPosition - transform.position;
        inRange = (toPlayer.sqrMagnitude < reach);
        //if player is within range or goose is paused
        if (inRange)
        {
            if (pauseTimer > 0)
            {
                pauseTimer = 0;
                hasAttacked = true;
                Debug.Log("pause reset");
            }
            if (!hasAttacked)
            {
                attackTimer = attackDelay;
            }
            else if (attackTimer <= 0)
            {
                Attack();
                hasAttacked = true;
                attackTimer = attackDelay;
            }
        }
        else
        {
            if (!hasAttacked && pauseTimer <= 0)
            {
                ChasePlayer();
            }
            if (hasAttacked && pauseTimer <= 0)
            {
                Debug.Log("paused");
                pauseTimer = pauseTime;
                hasAttacked = false;
            }
            if (pauseTimer > 0)
            {
                pauseTimer -= Time.deltaTime;
            }
        }
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void ChasePlayer()
    {
        rb.AddForce(toPlayer.normalized * moveSpeed);
    }

    private void Attack()
    {
        Debug.Log("attcked");
    }
}
