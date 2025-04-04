using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] List<Sprite> skins;

    [SerializeField] Image healthBar;
    [SerializeField] Image shootCooldown;
    [SerializeField] Image dashCooldown;

    [SerializeField] GameObject upgradeScreen;
    [SerializeField] GameObject UIScreen;
    [SerializeField] GameObject LoseScreen;

    public int maxHP;
    public int curHP;
    private int armourLevel = 0;

    public float moveSpeed;
    private Vector2 move;
    public float upgradeSpeed;

    public float baseAttackDelay;
    private float numUpgradeDelay = 1f;
    private float attackDelay;
    private float attackTimer;
    private Vector2 mouseAim;

    private int damageLevel = 0;

    [SerializeField] GameObject weapon;
    [SerializeField] GameObject decals;
    [SerializeField] List<GameObject> projectiles;
    public GameObject curProjectile;
    public Transform rotatePos;
    public Transform spawnPos;

    private bool faceLeft = true;

    private bool dodge = false;
    private float dodgeTime = 0.3f;
    private float dodgeTimer = 0.3f;
    private float dodgeCooldown = 3f;

    public Animator animator;

    void Start()
    {
        attackDelay = baseAttackDelay;
        attackTimer = attackDelay;
        curHP = maxHP;
        curProjectile = projectiles[0];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && dodgeCooldown < 0f && !dodge)
        {
            dodge = true;
            rb.AddForce(move * 40);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        if (dodge)
        {
            dodgeTimer -= Time.deltaTime;
            if (dodgeTimer < 0)
            {
                dodge = false;
                gameObject.GetComponent<Collider2D>().enabled = true;
                dodgeTimer = dodgeTime;
                dodgeCooldown = 3f;
            }
        }
        else
        {
            dodgeCooldown -= Time.deltaTime;
            dashCooldown.fillAmount = dodgeCooldown/3f;
        }
    }

    void FixedUpdate()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        rb.AddForce(move);

        mouseAim = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position) - rotatePos.position;
        float angle = Mathf.Atan2(mouseAim.y, mouseAim.x) * Mathf.Rad2Deg;

        rotatePos.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
        if (Mathf.Abs(angle) < 90 && faceLeft)
        {
            Debug.Log("turn right");
            faceLeft = false;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
            if (decals !=  null)
            {
                decals.transform.localScale = new Vector3(-decals.transform.localScale.x, decals.transform.localScale.y, decals.transform.localScale.z);
            }
            if (weapon.tag == "WeaponFlip")
            {
                weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, -weapon.transform.localScale.y, weapon.transform.localScale.z);
                weapon.transform.localPosition = new Vector3(weapon.transform.localPosition.x, -weapon.transform.localPosition.y, weapon.transform.localPosition.z);
            }
        }
        else if (Mathf.Abs(angle) > 90 && !faceLeft)
        {
            Debug.Log("turn left");
            faceLeft = true;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
            if (decals != null)
            {
                decals.transform.localScale = new Vector3(-decals.transform.localScale.x, decals.transform.localScale.y, decals.transform.localScale.z);
            }
            if (weapon.tag == "WeaponFlip")
            {
                weapon.transform.localScale = new Vector3(weapon.transform.localScale.x, -weapon.transform.localScale.y, weapon.transform.localScale.z);
                weapon.transform.localPosition = new Vector3(weapon.transform.localPosition.x, -weapon.transform.localPosition.y, weapon.transform.localPosition.z);
            }
        }
        if (attackTimer <= 0)
        {
            Debug.Log("player attack");
            GameObject temp = Instantiate(curProjectile, spawnPos.position, Quaternion.AngleAxis(angle, Vector3.forward));
            temp.GetComponent<ProjectileController>().direction = mouseAim.normalized;
            temp.GetComponent<ProjectileController>().UpgradeDamage(2 * damageLevel);
            attackTimer = attackDelay;
        }
        attackTimer -= Time.deltaTime;
        shootCooldown.fillAmount = attackTimer/attackDelay;
    }

    public void TakeDamage(int damage)
    {
        if (Random.Range(0, 10) >= armourLevel && !dodge)
        {
            curHP = Mathf.Clamp(curHP - damage, 0, maxHP);
            healthBar.fillAmount = (float)curHP / (float)maxHP;
            if (curHP == 0)
            {
                animator.SetBool("Dead", true);
                Time.timeScale = 0;
                LoseScreen.SetActive(true);
            }
        }
    }

    public void ResetHP()
    {
        curHP = maxHP;
    }

    public void UpgradeHP(int change)
    {
        maxHP += change;
        curHP += change;
        healthBar.fillAmount = (float)curHP / (float)maxHP;
    }

    public void UpgradeAttackSpeed(float change)
    {
        attackDelay -= (change)/numUpgradeDelay;
        numUpgradeDelay++;
    }

    public void UpgradeAttackDamage()
    {
        damageLevel++;
    }

    public void UpgradeSpeed(float change)
    {
        moveSpeed += change;
    }

    public void UpgradeArmour()
    {
        if (armourLevel < 5)
        {
            armourLevel++;
        }
    }

    public void StartUpgrade()
    {
        UIScreen.SetActive(false);
        upgradeScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void EndUpgrade()
    {
        UIScreen.SetActive(true);
        upgradeScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            curHP += 2;
            if (curHP > maxHP)
            {
                curHP = maxHP;
            }
            healthBar.fillAmount = (float)curHP / (float)maxHP;
            Destroy(collision.gameObject);
        }
    }
}
