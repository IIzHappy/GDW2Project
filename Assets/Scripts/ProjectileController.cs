using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float lifeSpan;
    [SerializeField] private bool expire = false;
    [SerializeField] private bool destroyOnCollision = true;
    [SerializeField] private bool explode = false;
    [SerializeField] private GameObject explosion;

    [SerializeField] private Rigidbody2D rb;

    public Vector2 direction;
    
    void Start()
    {
        rb.AddForce(direction * speed);
    }

    void Update()
    {
        if (expire)
        {
            lifeSpan -= Time.deltaTime;
            if (lifeSpan <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void UpgradeDamage(int dmg)
    {
        damage += dmg;
    }

    //private IEnumerator passThrough()
    //{
    //    gameObject.GetComponent<Collider2D>().isTrigger = true;

    //    yield return new WaitForSeconds(0.5f);

    //    gameObject.GetComponent<Collider2D>().isTrigger = true;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }

        if (explode)
        {
            Instantiate(explosion, this.gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        }
        if (destroyOnCollision)
        {
            Destroy(gameObject);
        }
        //else
        //{
        //    StartCoroutine(passThrough());
        //    rb.AddForce(direction * speed);
        //}
    }
}
