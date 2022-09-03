using Fusion;
using UnityEngine;

public class SnakeController : SnakeComponent
{
    SnakeNetworkInput Inputs;

    [SerializeField] float speed;
	[Networked] public RoomPlayer RoomUser { get; set; }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (GetInput(out SnakeNetworkInput input))
        {
            Inputs = input;
        }

        Move(Inputs);
    }

    private void Move(SnakeNetworkInput inputs)
    {
        Vector2 currentPosition = transform.position;
        Vector2 targetPosition = currentPosition + Runner.DeltaTime * speed * inputs.inputDirection.normalized;

        Snake.Rigidbody2D.MovePosition(targetPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
		MazeGenerator.Instance.ResetPlayerPosition(Snake.NetworkRigidbody2D);
    }
}