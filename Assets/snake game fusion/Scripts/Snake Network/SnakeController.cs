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

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        Vector2 direction = Vector2.zero;
        if (GetInput(out CustomNetworkInputStruct input))
        {
            direction = input.direction.normalized;
        }

        Move(direction);
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