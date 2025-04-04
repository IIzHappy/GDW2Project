using UnityEngine;

public class MenuBob : MonoBehaviour
{
    public int speed;
    public float timer = 1;
    private bool direction;
    private Vector2 move;
    Rigidbody2D rb;
    public bool ismoving;
    public Vector2 defaultPos;
    private void Awake()
    {
        defaultPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (ismoving)
        {
            timer += Time.deltaTime;
            if (timer < 2)
            {
                direction = true;
            }
            if (timer > 2)
            {
                direction = false;
            }
            if (timer > 4)
            {
                timer = 0;
            }
            if (direction)
            {
                move = Vector2.up * speed;
            }

            if (!direction)
            {
                move = Vector2.down * speed;
            }
            rb.linearVelocity = move;
        }
    }
    public void mouseentered()
    {
        ismoving = true;
    }
    public void mouseLeft()
    {
        ismoving = false;
        timer = 1;
        gameObject.transform.position = defaultPos;
        rb.linearVelocity = new Vector2(0, 0);
    }
}
