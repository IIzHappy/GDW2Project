using UnityEngine;

public class GooseSpawnBoundsFix : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.transform.position = new Vector2(0, 0);
        }
    }
}
