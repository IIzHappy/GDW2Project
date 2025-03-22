using UnityEngine;

public class PushController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float lifeSpan;
    [SerializeField] private bool expire = false;

    [SerializeField] private Rigidbody2D rb;

    public Vector2 direction;

    void Start()
    {

    }

    void Update()
    {
        rb.linearVelocity = (direction * speed);
        if (expire)
        {
            lifeSpan -= Time.deltaTime;
            if (lifeSpan <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
