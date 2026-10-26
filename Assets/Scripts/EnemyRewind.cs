using Unity.Hierarchy;
using UnityEngine;
using System.Collections.Generic;

public class EnemyRewind : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyController enemy;
    private Stack<EnemyCommand> history = new Stack<EnemyCommand>();
    private Vector2 lastPos;
    private float posRecordTimer;
    private bool isRewinding;
    public float posRecordInterval = 0.1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyController>();
        lastPos = rb.position;
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            if (history.Count> 0)
            {
                history.Pop().Rewind();
            }
                
            else
            {
                StopRewinding();
                
            }
            return;
        }

        posRecordTimer += Time.fixedDeltaTime;

        if (posRecordTimer >= posRecordInterval)
        {
            history.Push(new EnemyMoveCommand(rb, lastPos));
            lastPos = rb.position;
            posRecordTimer = 0f;
        }
    }


    public void StartRewinding()
    {
        isRewinding = true;
        enemy.enabled = false;
        rb.linearVelocity = Vector2.zero;


    }

    public void StopRewinding()
    {
        isRewinding = false;
        enemy.enabled = true;
        lastPos = rb.position;
        rb.linearVelocity = Vector2.zero;
    }
}
