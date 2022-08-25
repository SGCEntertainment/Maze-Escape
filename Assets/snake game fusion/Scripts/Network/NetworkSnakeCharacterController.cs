using UnityEngine;
using Fusion;

public class NetworkSnakeCharacterController : NetworkTransform
{
    SnakeInputHandler snakeInputHandler;

    [SerializeField] Color otherColor;
    [SerializeField] SpriteRenderer myRend;

    [Space(10)]
    [SerializeField] float speed;

    [Networked]
    public Vector3 Velocity { get; set; }

    protected override void Awake()
    {
        snakeInputHandler = GetComponent<SnakeInputHandler>();
    }

    public override void Spawned()
    {
        base.Spawned();
        snakeInputHandler = GetComponent<SnakeInputHandler>();
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData data))
        {
            transform.position += Runner.DeltaTime * speed * data.velocity;
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
