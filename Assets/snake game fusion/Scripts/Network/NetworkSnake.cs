using UnityEngine;
using Fusion;
using TMPro;

public class NetworkSnake : NetworkBehaviour, IPlayerLeft
{
    NetworkSnakeCharacterController _networkSnakeCharacterController;
    public static NetworkSnake Local { get; set; }

    [SerializeField] TextMeshPro myNickText;

    [Networked(OnChanged = nameof(OnNickNameChanged))]
    public NetworkString<_2> NickName { get; set; }

    public override void Spawned()
    {
        _networkSnakeCharacterController = GetComponent<NetworkSnakeCharacterController>();

        if (Object.HasInputAuthority)
        {
            Local = this;
            RPC_SetNickName(Runner.UserId);
            Debug.Log("Spawned own car");
        }
        else
        {
            _networkSnakeCharacterController.SetPlayerColorAsOther();
            Debug.Log("Spawned other player car");
        }
    }

    static void OnNickNameChanged(Changed<NetworkSnake> changed)
    {
        changed.Behaviour.OnNickNameChanged();
    }

    void OnNickNameChanged()
    {
        myNickText.text = NickName.ToString();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
    public void RPC_SetNickName(string nickName, RpcInfo info = default)
    {
        NickName = nickName;
    }

    public void PlayerLeft(PlayerRef player)
    {
        if(player == Object.InputAuthority)
        {
            Runner.Despawn(Object);
        }
    }
}
