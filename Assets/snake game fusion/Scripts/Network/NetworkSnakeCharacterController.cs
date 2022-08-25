using UnityEngine;
using Fusion;

public class NetworkSnakeCharacterController : NetworkBehaviour
{
    SnakeInputHandler snakeInputHandler;

    [SerializeField] Color otherColor;
    [SerializeField] SpriteRenderer myRend;

    private void Awake()
    {
        snakeInputHandler = GetComponent<SnakeInputHandler>();
    }

    public override void FixedUpdateNetwork()
    {
        if(GetInput(out NetworkInputData data))
        {
            transform.position = data.newPosition;
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
            snakeInputHandler.newPosition = NetworkSpawnSnake.Instance.spawnPoint.position;
            NetworkSpawnSnake.Instance.ResetPlayerPosition(transform);
        }
    }
}
