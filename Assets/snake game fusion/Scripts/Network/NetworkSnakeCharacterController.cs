using UnityEngine;
using Fusion;

public class NetworkSnakeCharacterController : NetworkTransform
{
    SnakeInputHandler snakeInputHandler;
    NetworkRigidbody2D networkRigidbody2D;

    [SerializeField] Color otherColor;
    [SerializeField] SpriteRenderer myRend;

    [Space(10)]
    [SerializeField] float speed;

    public Vector3 Velocity { get; set; }

    protected override void Awake()
    {
        snakeInputHandler = GetComponent<SnakeInputHandler>();
        networkRigidbody2D = GetComponent<NetworkRigidbody2D>();
    }

    public override void Spawned()
    {
        base.Spawned();

        snakeInputHandler = GetComponent<SnakeInputHandler>();
        networkRigidbody2D = GetComponent<NetworkRigidbody2D>();
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData data))
        {
            //transform.position += Runner.DeltaTime * speed * data.velocity;
            networkRigidbody2D.Rigidbody.velocity = speed * data.velocity;
        }
    }

    public void SetPlayerColorAsOther()
    {
        myRend.color = otherColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("border"))
        {
            snakeInputHandler.isHold = false;
            snakeInputHandler.Velocity = NetworkSpawnSnake.Instance.spawnPoint.position;
            NetworkSpawnSnake.Instance.ResetPlayerPosition(transform);
        }
    }
}
