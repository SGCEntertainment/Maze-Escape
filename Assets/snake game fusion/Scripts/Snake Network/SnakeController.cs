using Fusion;
using UnityEngine;

public class SnakeController : SnakeComponent
{
	[SerializeField] float speed;

	public NetworkRigidbody2D Rigidbody;
	//[Networked] private SnakeInput.NetworkInputData Inputs { get; set; }
	[Networked] public RoomPlayer RoomUser { get; set; }

    //public override void FixedUpdateNetwork()
    //{
    //	base.FixedUpdateNetwork();

    //	if (GetInput(out SnakeInput.NetworkInputData input))
    //	{
    //		Inputs = input;
    //	}

    //	Move(Inputs);
    //}

    [Networked]
    public Vector2 MovementDirection { get; set; }

    public override void FixedUpdateNetwork()
    {
        Vector3 direction;
        if (GetInput(out CustomNetworkInputStruct input))
        {
            direction = default;

            if (input.IsDown(NetworkInputPrototype.BUTTON_FORWARD))
            {
                direction += Vector3.forward;
            }

            if (input.IsDown(NetworkInputPrototype.BUTTON_BACKWARD))
            {
                direction -= Vector3.forward;
            }

            if (input.IsDown(NetworkInputPrototype.BUTTON_LEFT))
            {
                direction -= Vector3.right;
            }

            if (input.IsDown(NetworkInputPrototype.BUTTON_RIGHT))
            {
                direction += Vector3.right;
            }

            direction = direction.normalized;
            MovementDirection = direction;

        }
        else
        {
            direction = MovementDirection;
        }


        Vector2 direction2d = new Vector2(direction.x, direction.y + direction.z);
        Rigidbody.Rigidbody.AddForce(direction2d * speed);
    }

    //   private void Move(SnakeInput.NetworkInputData input)
    //{
    //	Rigidbody.Rigidbody.position += Runner.DeltaTime * speed * input.direction;
    //}
    private void Move(Vector2 direction)
    {
        Rigidbody.Rigidbody.MovePosition(Rigidbody.Rigidbody.position + Runner.DeltaTime * speed * direction);
        //Rigidbody.Rigidbody.position += Runner.DeltaTime * speed * direction;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
		MazeGenerator.Instance.ResetPlayerPosition(Rigidbody);
    }
}