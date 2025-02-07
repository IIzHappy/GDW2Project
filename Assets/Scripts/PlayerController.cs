using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] List<Sprite> skins;

    public int maxHP;
    private int curHP;

    public float moveSpeed;
    private Vector2 move;
    public float upgradeSpeed;

    public float baseAttackDelay;
    private float attackDelay;
    private float attackTimer;

    [SerializeField] List<GameObject> projectiles;
    public GameObject curProjectile;
    public Transform spawnPos;
    //public int baseDamage;
    //private int damage;



    void Start()
    {
        attackDelay = baseAttackDelay;
        attackTimer = attackDelay;
        curHP = maxHP;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        rb.AddForce(move);
        if (attackTimer <= 0)
        {
            Debug.Log("player attack");
            attackTimer = attackDelay;
        }
        attackTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        curHP = Mathf.Clamp(curHP - damage, 0, maxHP);

        if (curHP == 0)
        {
            Debug.Log("player died");
            Time.timeScale = 0;
        }
    }

    public void ResetHP()
    {
        curHP = maxHP;
    }

    public void UpgradeHP(int change)
    {
        maxHP += change;
    }

    public void UpgradeAttack(float change)
    {
        attackDelay -= change;
    }

    //public void UpgradeDamage(int change)
    //{
    //    damage += change;
    //}
}
