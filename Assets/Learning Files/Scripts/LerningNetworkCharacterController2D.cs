using Fusion;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[OrderBefore(typeof(NetworkTransform))]
[DisallowMultipleComponent]
public class LerningNetworkCharacterController2D : NetworkTransform
{
    public float acceleration = 10.0f;
    public float braking = 10.0f;
    public float maxSpeed = 2.0f;
    public float rotationSpeed = 15.0f;

    [HideInInspector, Networked]
    public Vector3 Velocity { get; set; }

    public Rigidbody2D rg2d { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        CacheController();
    }

    public override void Spawned()
    {
        base.Spawned();
        CacheController();
    }

    private void CacheController()
    {
        if (!rg2d)
        {
            rg2d = GetComponent<Rigidbody2D>();
            Assert.Check(rg2d != null, $"An object with {nameof(NetworkCharacterControllerPrototype)} must also have a {nameof(CharacterController)} component.");
        }
    }

    protected override void CopyFromBufferToEngine()
    {
        rg2d.simulated = true;
        base.CopyFromBufferToEngine();
        rg2d.simulated = true;
    }

    public virtual void Move(Vector3 direction)
    {
        var deltaTime = Runner.DeltaTime;
        var previousPos = transform.position;
        var moveVelocity = Velocity;

        direction = direction.normalized;

        var horizontalVel = default(Vector3);
        horizontalVel.x = moveVelocity.x;
        horizontalVel.y = moveVelocity.z;
        //horizontalVel.z = moveVelocity.z;

        if (direction == default)
        {
            horizontalVel = Vector3.Lerp(horizontalVel, default, braking * deltaTime);
        }
        else
        {
            horizontalVel = Vector3.ClampMagnitude(horizontalVel + direction * acceleration * deltaTime, maxSpeed);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Runner.DeltaTime);
        }

        moveVelocity.x = horizontalVel.x;
        moveVelocity.y = horizontalVel.z;

        rg2d.position += (Vector2)moveVelocity * deltaTime;

        Velocity = (transform.position - previousPos) * Runner.Simulation.Config.TickRate;
    }
}
