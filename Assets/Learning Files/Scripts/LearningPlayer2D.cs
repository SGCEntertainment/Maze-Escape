using Fusion;
using UnityEngine;

public class LearningPlayer2D : NetworkBehaviour
{
    private LerningNetworkCharacterController2D ncc2d;

    [Networked]
    public Vector3 MovementDirection { get; set; }

    private void Awake()
    {
        ncc2d = GetComponent<LerningNetworkCharacterController2D>();
    }

    public override void FixedUpdateNetwork()
    {
        Vector3 direction;
        if (GetInput(out LearningNetworkInputData data))
        {
            data.direction.Normalize();

            direction = data.direction;
            MovementDirection = direction;
        }
        else
        {
            direction = MovementDirection;
        }

        ncc2d.Move(direction);
    }
}
