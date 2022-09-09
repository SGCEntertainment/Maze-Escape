using Fusion;
using UnityEngine.UI;

public class MyStats : SnakeComponent
{
    UserInfo UserInfo { get; set; }

    private void OnMouseEnter()
    {
        if (Object.HasInputAuthority)
        {
            return;
        }

        RPC_ShowMyStats(Runner.LocalPlayer, true);
    }

    private void OnMouseExit()
    {
        if (Object.HasInputAuthority)
        {
            return;
        }

        RPC_ShowMyStats(Runner.LocalPlayer, false);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_ShowMyStats([RpcTarget] PlayerRef _, bool IsShow)
    {
        UIManager.Instance.UpdateOtherStepCount(Snake.StepCount, IsShow);
    }
}