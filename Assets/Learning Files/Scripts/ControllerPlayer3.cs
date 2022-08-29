using UnityEngine;
using Fusion;

public class ControllerPlayer3 : NetworkBehaviour
{
    protected NetworkRigidbody2D _nrb2d;

    [Networked]
    public Vector3 MovementDirection { get; set; }

    public float Speed = 6f;

    public void Awake()
    {
        CacheComponents();
    }

    public override void Spawned()
    {
        CacheComponents();
    }

    private void CacheComponents()
    {
        if (!_nrb2d)
        {
            _nrb2d = GetComponent<NetworkRigidbody2D>();
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.Config.PhysicsEngine == NetworkProjectConfig.PhysicsEngines.None)
        {
            return;
        }

        Vector3 direction;
        if (GetInput(out LearningNetworkInputData input))
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


        if (_nrb2d && !_nrb2d.Rigidbody.isKinematic)
        {
            Vector2 direction2d = new Vector2(direction.x, direction.y + direction.z);
            _nrb2d.Rigidbody.AddForce(direction2d * Speed);
        }
        else
        {
            direction = new Vector3(direction.x, direction.z, 0);
            transform.position += (direction * Speed * Runner.DeltaTime);
        }
    }
}
