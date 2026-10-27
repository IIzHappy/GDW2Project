using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

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
    private bool dead;
    private float deathDelay = 0.8333f;
    private int armourLevel = 0;

    public float moveSpeed;
    private Vector2 move;
    public float upgradeSpeed;

    public List<PlayerWeapon> _weapons = new List<PlayerWeapon>();
    int _curWeapon = 0;
    public float baseAttackDelay;
    private float numUpgradeDelay = 1f;
    private float attackDelay;
    private float attackTimer;
    private Vector2 mouseAim;

    private int damageLevel = 0;

    public Transform rotatePos;

    private bool faceLeft = true;

    private bool dodge = false;
    private float dodgeTime = 0.3f;
    private float dodgeTimer = 0.3f;
    private float dodgeCooldown = 3f;

    public Animator animator;

    public AudioClip PlayerDeath;
    public AudioClip PlayerDamage;
    public AudioClip WeaponThrow;
    public AudioClip UpgradeOpenSound;

    void Awake()
    {
        if (Instance != null)
        {
            if (Instance != this) Destroy(this);
        }
        else
        {
            Instance = this;
            enemyManager.Instance._player = gameObject.transform;
        }
    }

    void Start()
    {
        attackDelay = baseAttackDelay;
        attackTimer = attackDelay;
        curHP = maxHP;
    }

    void Update()
    {
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
            dashCooldown.fillAmount = dodgeCooldown / 3f;
        }

        if (dead)
        {
            deathDelay -= Time.deltaTime;
            if (deathDelay <= 0)
            {
                Die();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            EnemyRewind[] rewindEnemies = FindObjectsByType<EnemyRewind>(FindObjectsSortMode.None);
            foreach (EnemyRewind enemy in rewindEnemies)
            {
                enemy.StartRewinding();
            }
        }

        void FixedUpdate()
        {
            move = move.normalized * moveSpeed;
            rb.AddForce(move);
            move = new Vector2(0, 0);

            mouseAim = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position) - rotatePos.position;
            float angle = Mathf.Atan2(mouseAim.y, mouseAim.x) * Mathf.Rad2Deg;

            rotatePos.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
            if (Mathf.Abs(angle) < 90 && faceLeft)
            {
                Debug.Log("turn right");
                faceLeft = false;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
                _weapons[_curWeapon].FlipWeapon();
                
            }
            else if (Mathf.Abs(angle) > 90 && !faceLeft)
            {
                Debug.Log("turn left");
                faceLeft = true;
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
                _weapons[_curWeapon].FlipWeapon();
            }
            if (attackTimer <= 0)
            {
                Debug.Log("player attack");
                AudioSource.PlayClipAtPoint(WeaponThrow, gameObject.transform.position);
                _weapons[_curWeapon].SpawnProjectile(mouseAim, damageLevel);
                attackTimer = attackDelay;
            }
            attackTimer -= Time.deltaTime;
            shootCooldown.fillAmount = attackTimer / attackDelay;
        }
    }

    public void SwitchWeapon(int weapon)
    {
        _weapons[_curWeapon].DecalOnOff(false);
        _weapons[_curWeapon].gameObject.SetActive(false);
        _curWeapon = weapon;
        _weapons[_curWeapon].gameObject.SetActive(true);
        _weapons[_curWeapon].DecalOnOff(true);
    }

    public void Dodge()
    {
        if (dodgeCooldown < 0f && !dodge)
        {
            dodge = true;
            rb.AddForce(move * 40);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void MoveUp()
    {
        move.y = 1f;
    }

    public void MoveDown()
    {
        move.y -= 1f;
    }

    public void MoveRight()
    {
        move.x = 1f;
    }

    public void MoveLeft()
    {
        move.x = -1f;
    }

    public void TakeDamage(int damage)
    {
        if (Random.Range(0, 10) >= armourLevel && !dodge)
        {
            AudioSource.PlayClipAtPoint(PlayerDamage, gameObject.transform.position);
            curHP = Mathf.Clamp(curHP - damage, 0, maxHP);
            healthBar.fillAmount = (float)curHP / (float)maxHP;
            if (curHP == 0)
            {
                animator.SetBool("Dead", true);
                AudioSource.PlayClipAtPoint(PlayerDeath, gameObject.transform.position);
                dead = true;
            }
        }
    }

    public void Die()
    {
        GameManager.Instance.GameLoss();
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
        AudioSource.PlayClipAtPoint(UpgradeOpenSound, gameObject.transform.position);
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
        Time.timeScale = 1;
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
