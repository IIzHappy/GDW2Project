using UnityEngine;

public class ExplosiveController : MonoBehaviour
{
    public bool doesDmg = true; //dmg or healing
    public int value; //dmg or healing amount
    public float fullSize;
    public float expandTime;
    private float timer;
    public float pauseTime;
    private bool paused = false;

    public void Update()
    {
        if (!paused)
        {
            float scale = Mathf.Lerp(0, fullSize, timer / expandTime);
            transform.localScale = new Vector2(scale, scale);
            timer += Time.deltaTime;
            if (timer >= expandTime)
            {
                paused = true;
                timer = 0;
            }
        } else
        {
            duringPause();
        }
    }

    public virtual void duringPause()
    {
        timer += Time.deltaTime;
        if (timer >= pauseTime)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (doesDmg)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().TakeDamage(value);
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(value);
            }
        } else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyController>().Heal(value);
            }
        }
    }
}
