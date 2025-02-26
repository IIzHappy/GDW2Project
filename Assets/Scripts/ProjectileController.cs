using UnityEngine;

public class ProjectileController : MonoBehaviour
{

    [SerializeField] private float speed;
    [SerializeField] private int damage;

    [SerializeField] private Rigidbody2D rb;

    // Update is called once per frame
    void Start()
    {
        rb.AddForce(new Vector2(500 * Mathf.Sin((rb.rotation * Mathf.Deg2Rad)), 500 * Mathf.Cos((rb.rotation * Mathf.Deg2Rad))));
        Debug.Log(Mathf.Cos(rb.rotation));
    }

    public void UpgradeDamage(int dmg)
    {
        damage += dmg;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
