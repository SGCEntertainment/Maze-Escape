using Fusion;
using UnityEngine;

public class SnakeController : SnakeComponent
{
	[SerializeField] float speed;

	public Rigidbody2D Rigidbody;
	[Networked] private SnakeInput.NetworkInputData Inputs { get; set; }
	[Networked] public RoomPlayer RoomUser { get; set; }


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
		Rigidbody.position += Runner.DeltaTime * speed * input.newPostition;
	}
}
