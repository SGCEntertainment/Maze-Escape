using Fusion;
using UnityEngine;

public class SnakeController : SnakeComponent
{
	[SerializeField] float speed;

	public NetworkRigidbody2D Rigidbody;
	[Networked] private SnakeInput.NetworkInputData Inputs { get; set; }
	[Networked] public RoomPlayer RoomUser { get; set; }

	[Networked] Vector2 MoveDirection { get; set; }

	public override void FixedUpdateNetwork()
	{
		base.FixedUpdateNetwork();

		if (GetInput(out SnakeInput.NetworkInputData input))
		{
			Inputs = input;
		}

		Move(Inputs);
	}

	private void Move(SnakeInput.NetworkInputData input)
	{
		Vector2 moveDirection = MoveDirection;
		Rigidbody.Rigidbody.position += speed * moveDirection;

		MoveDirection = input.direction * Runner.DeltaTime;
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
		MazeGenerator.Instance.ResetPlayerPosition(Rigidbody);
    }
}
