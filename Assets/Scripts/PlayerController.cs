using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] List<Sprite> skins;

    public int maxHP;
    private int curHP;
    public int upgradeHP;

    public float moveSpeed;
    private Vector2 move;
    public float upgradeSpeed;

    public float baseAttackDelay;
    private float attackDelay;
    public float attackTimer;
    public float upgradeAttack;

    public float baseDamage;
    private float damage;
    public float upgradeDamage;

    void Start()
    {
        attackTimer = attackDelay;
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
        }
    }

    public void ResetHP()
    {
        curHP = maxHP;
    }

    public void UpgradeHP()
    {
        maxHP += upgradeHP;
    }

    public void UpgradeAttack()
    {
        attackDelay -= upgradeAttack;
    }

    public void UpgradeDamage()
    {
        damage += upgradeDamage;
    }
}
