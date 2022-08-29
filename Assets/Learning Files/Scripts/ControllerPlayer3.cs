using UnityEngine;
using Fusion;

public class ControllerPlayer3 : NetworkBehaviour
{
    protected NetworkRigidbody2D _nrb2d;

    [Networked]
    public Vector2 MovementDirection { get; set; }

    [HideInInspector]
    public Vector2 Velocity { get; set; }

    public float acceleration = 10.0f;
    public float braking = 10.0f;
    public float maxSpeed = 2.0f;



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

        Vector2 direction;
        if (GetInput(out LearningNetworkInputData input))
        {
            direction = default;

            if (input.IsDown(NetworkInputPrototype.BUTTON_FORWARD))
            {
                direction += Vector2.up;
            }

            if (input.IsDown(NetworkInputPrototype.BUTTON_BACKWARD))
            {
                direction -= Vector2.up;
            }

            if (input.IsDown(NetworkInputPrototype.BUTTON_LEFT))
            {
                direction -= Vector2.right;
            }

            if (input.IsDown(NetworkInputPrototype.BUTTON_RIGHT))
            {
                direction += Vector2.right;
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
            Vector2 direction2d = new Vector2(direction.x, direction.y);
            _nrb2d.Rigidbody.AddForce(direction2d * Speed);
        }
        else
        {
            var deltaTime = Runner.DeltaTime;
            var previousPos = transform.position;
            var moveVelocity = Velocity;

            var horizontalVel = default(Vector2);
            horizontalVel.x = moveVelocity.x;
            horizontalVel.y = moveVelocity.y;

            if (direction == default)
            {
                horizontalVel = Vector3.Lerp(horizontalVel, default, braking * deltaTime);
            }
            else
            {
                horizontalVel = Vector3.ClampMagnitude(horizontalVel + direction * acceleration * deltaTime, maxSpeed);
            }

            moveVelocity.x = horizontalVel.x;
            moveVelocity.y = horizontalVel.y;

            transform.position +=  new Vector3(moveVelocity.x, moveVelocity.y, 0) * deltaTime;
            Velocity = (transform.position - previousPos) * Runner.Simulation.Config.TickRate;
        }
    }
}
