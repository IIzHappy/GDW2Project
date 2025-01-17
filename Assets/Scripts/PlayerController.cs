using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] List<Sprite> skins;

    public int maxHP;
    private int curHP;

    public float moveSpeed;
    private Vector2 move;



    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;
        rb.AddForce(move);
    }

    public void TakeDamage(int damage)
    {

    }
}
