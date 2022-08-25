using UnityEngine;
using Fusion;

public class NetworkSnakeCharacterController : NetworkBehaviour
{
    SnakeInputHandler snakeInputHandler;

    [SerializeField] Color otherColor;
    [SerializeField] SpriteRenderer myRend;

    [Space(10)]
    [SerializeField] float speed;

    private void Awake()
    {
        snakeInputHandler = GetComponent<SnakeInputHandler>();
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData data))
        {
            transform.position += Runner.DeltaTime * speed * data.direction;
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
            snakeInputHandler.direction = NetworkSpawnSnake.Instance.spawnPoint.position;
            NetworkSpawnSnake.Instance.ResetPlayerPosition(transform);
        }
    }
}
