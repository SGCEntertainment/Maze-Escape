using Fusion;

public class MyStats : SnakeComponent
{
    [System.Serializable]
    public struct UserInfo : INetworkStruct
    {
        [Networked] public NetworkString<_32> FirstName { get; set; }
        [Networked] public NetworkString<_32> LastName { get; set; }
        [Networked] public NetworkString<_128> Photo { get; set; }

        public static UserInfo Defaults
        {
            get
            {
                var result = new UserInfo
                {
                    FirstName = "Стандартная",
                    LastName = "Страница",
                    Photo = "https://vk.com/images/camera_200.png"
                };

                return result;
            }
        }
    }

    [Networked, UnitySerializeField] UserInfo UserInfoData { get; set; } = UserInfo.Defaults;

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_Configure(string fn, string ln, string photo)
    {
        var copy = UserInfoData;
        copy.FirstName = fn;
        copy.LastName = ln;
        copy.Photo = photo;
        UserInfoData = copy;
    }

    public override void Spawned()
    {
        if(!Object.HasInputAuthority)
        {
            return;
        }

        string fn = UIManager.Instance.Container.first_name;
        string ln = UIManager.Instance.Container.last_name;
        string photo = UIManager.Instance.Container.photo_100;

        RPC_Configure(fn, ln, photo);
    }

    private void OnMouseEnter()
    {
        if (!Object.HasInputAuthority)
        {
            RPC_ShowMyStats(Runner.LocalPlayer, true);
        }
    }

    private void OnMouseExit()
    {
        if (!Object.HasInputAuthority)
        {
            RPC_ShowMyStats(Runner.LocalPlayer, false);
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_ShowMyStats([RpcTarget] PlayerRef _, bool IsShow)
    {
        UIManager.Instance.ShowStatsGO(IsShow, UserInfoData.FirstName.Value, UserInfoData.LastName.Value, UserInfoData.Photo.Value);
    }
}