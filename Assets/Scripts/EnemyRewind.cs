using System.Collections.Generic;
using System.Windows.Input;
using Unity.Hierarchy;
using UnityEngine;

public class EnemyRewind : MonoBehaviour
{
    private Rigidbody2D rb;
    private EnemyController enemy;
    private Stack<EnemyCommand> history = new Stack<EnemyCommand>();
    private Vector2 lastPos;
    private float posRecordTimer;
    private bool isRewinding;
    [SerializeField] private float posRecordInterval = 0.1f;
    [SerializeField] private float rewindTime = 3f;

     void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemy = GetComponent<EnemyController>();
        lastPos = rb.position;
    }

    void FixedUpdate()
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

        RecordMovement();
    }

    private void RecordMovement()
    {
        posRecordTimer += Time.fixedDeltaTime;
        if (posRecordTimer >= posRecordInterval) {
            Vector2 currentPos = rb.position;
            if ((currentPos - lastPos).sqrMagnitude > 0.000001f){
                EnemyCommand command = new EnemyMoveCommand(
                    rb,
                    lastPos,
                    currentPos
                );
                history.Push(command);
                int maxCommands = Mathf.CeilToInt(rewindTime / posRecordInterval);
                if (history.Count > maxCommands)
                {
                    EnemyCommand[] commands = history.ToArray();
                    history.Clear();

                    for (int i = maxCommands - 1; i >= 0; i--)
                    {
                        history.Push(commands[i]);
                    }
                }
            }
            lastPos = currentPos;
            posRecordTimer = 0f;
        }
    }

    public void StartRewinding()
    {
        isRewinding = true;
        enemy.enabled = false;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;


    }

    public void StopRewinding()
    {
        isRewinding = false;
        rb.angularVelocity = 0f;
        lastPos = rb.position;
        rb.linearVelocity = Vector2.zero;
        posRecordTimer = 0f;
        enemy.enabled = true;
    }
}
