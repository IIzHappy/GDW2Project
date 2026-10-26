using UnityEngine;

public class EnemyMoveCommand : EnemyCommand
{
    private Rigidbody2D rb;
    private Vector2 previousPos;
    private Vector2 targetPos;

    public EnemyMoveCommand(Rigidbody2D rb, Vector2 previousPos, Vector2 targetPos)
    {
        this.rb = rb;
        this.previousPos = previousPos;
        this.targetPos = targetPos;
    }

    public void Execute()
    {
        rb.position = targetPos;
    }

    public void Rewind()
    {
        rb.position = previousPos;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
