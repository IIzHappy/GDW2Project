using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] List<Sprite> skins;

    [SerializeField] Image healthBar;

    public int maxHP;
    private int curHP;

    public float moveSpeed;
    private Vector2 move;
    public float upgradeSpeed;

    public float baseAttackDelay;
    private float attackDelay;
    private float attackTimer;

    private int damageLevel = 0;

    [SerializeField] List<GameObject> projectiles;
    public GameObject curProjectile;
    public Transform spawnPos;



    void Start()
    {
        attackDelay = baseAttackDelay;
        attackTimer = attackDelay;
        curHP = maxHP;
        curProjectile = projectiles[0];
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
            GameObject temp = Instantiate(curProjectile, spawnPos);
            temp.GetComponent<ProjectileController>().UpgradeDamage(2 * damageLevel);
            attackTimer = attackDelay;
        }
        attackTimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        curHP = Mathf.Clamp(curHP - damage, 0, maxHP);
        healthBar.fillAmount = (float) curHP / (float) maxHP;
        if (curHP == 0)
        {
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
}
