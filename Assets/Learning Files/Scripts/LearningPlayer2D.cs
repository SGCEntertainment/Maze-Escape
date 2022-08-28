using Fusion;
using UnityEngine;

public class LearningPlayer2D : NetworkBehaviour
{
    private NetworkRigidbody2D nr2d;
    public float Speed;

    [Networked]
    public Vector2 MovementDirection { get; set; }

    private void Awake()
    {
        nr2d = GetComponent<NetworkRigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        Vector3 direction;
        if (GetInput(out LearningNetworkInputData data))
        {
            Vector2 moveDirection = data.direction2D;
            direction = moveDirection;

            direction = direction.normalized;
            MovementDirection = direction;
        }
        else
        {
            direction = MovementDirection;
        }

        Vector2 direction2d = new Vector2(direction.x, direction.y);
        nr2d.Rigidbody.MovePosition(nr2d.Rigidbody.position + Runner.DeltaTime * Speed * direction2d);
    }
}
