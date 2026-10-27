using UnityEngine;

public abstract class Command
{
    public abstract void Execute();
}

public class Dodge : Command
{
    private PlayerController _controller;
    public Dodge (PlayerController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.Dodge();
    }
}

public class MoveUp : Command
{
    private PlayerController _controller;
    public MoveUp(PlayerController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.MoveUp();
    }
}

public class MoveDown : Command
{
    private PlayerController _controller;
    public MoveDown(PlayerController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.MoveDown();
    }
}

public class MoveRight : Command
{
    private PlayerController _controller;
    public MoveRight(PlayerController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.MoveRight();
    }
}

public class MoveLeft : Command
{
    private PlayerController _controller;
    public MoveLeft(PlayerController controller)
    {
        _controller = controller;
    }

    public override void Execute()
    {
        _controller.MoveLeft();
    }
}

public class DoNothing : Command
{
    public override void Execute()
    {
    }
}

public class InputHandler : MonoBehaviour
{
    PlayerController _pc;
    Command buttonA;
    Command buttonS;
    Command buttonD;
    Command buttonW;
    Command button_Space;

    private void Awake()
    {
        _pc = gameObject.GetComponent<PlayerController>();
    }
    private void Start()
    {
        buttonA = new MoveLeft(_pc);
        buttonS = new MoveDown(_pc);
        buttonD = new MoveRight(_pc);
        buttonW = new MoveUp(_pc);
        button_Space = new Dodge(_pc);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            buttonA.Execute();
        }
        if (Input.GetKey(KeyCode.S))
        {
            buttonS.Execute();
        }
        if (Input.GetKey(KeyCode.D))
        {
            buttonD.Execute();
        }
        if (Input.GetKey(KeyCode.W))
        {
            buttonW.Execute();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            button_Space.Execute();
        }
    }
}