using UnityEngine;
using Fusion;

public class NetworkSnake : NetworkBehaviour, IPlayerLeft
{
    NetworkSnakeCharacterController _networkSnakeCharacterController;
    public static NetworkSnake Local { get; set; }

    public override void Spawned()
    {
        _networkSnakeCharacterController = GetComponent<NetworkSnakeCharacterController>();

        if (Object.HasInputAuthority)
        {
            Local = this;
            Debug.Log("Spawned own car");
        }
        else
        {
            _networkSnakeCharacterController.SetPlayerColorAsOther();
            Debug.Log("Spawned other player car");
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if(player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }
}
