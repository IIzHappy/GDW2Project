using UnityEngine;

public class EnemyMoveCommand : EnemyCommand
{
    private Rigidbody2D rb;
    private Vector2 previousPos;

    public EnemyMoveCommand(Rigidbody2D rb, Vector2 previousPos)
    {
        this.rb = rb;
        this.previousPos = previousPos;
    }

    public void Execute()
    {

    }

    public void Rewind()
    {
        rb.position = previousPos;
    }
}
