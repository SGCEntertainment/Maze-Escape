using Fusion;
using UnityEngine;

public class MyStats : SnakeComponent
{
    [System.Serializable]
    public struct UserInfo : INetworkStruct
    {
        [Networked] public int ID { get; set; }
        [UnitySerializeField] public NetworkString<_32> FirstName { get; set; }
        [UnitySerializeField] public NetworkString<_32> LastName { get; set; }
        [UnitySerializeField] public NetworkString<_256> Photo { get; set; }

        public static UserInfo Defaults
        {
            get
            {
                var result = new UserInfo
                {
                    ID = 508882623,
                    FirstName = "Philipp",
                    LastName = "Sokolov",
                    Photo = "https://vk.com/images/camera_100.png"
                };

                return result;
            }
        }
    }

    const string vkBaseUrl = "https://vk.com/id";

    [Networked, UnitySerializeField] UserInfo UserInfoData { get; set; } = UserInfo.Defaults;

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    public void RPC_Configure(int ID, string fn, string ln, string photo)
    {
        var copy = UserInfoData;

        copy.ID = ID;
        copy.FirstName = fn;
        copy.LastName = ln;
        copy.Photo = photo;

        UserInfoData = copy;
    }

    public override void Spawned()
    {
        if (!Object.HasInputAuthority)
        {
            return;
        }

        int id = UIManager.Instance.Container.id;
        string fn = UIManager.Instance.Container.first_name;
        string ln = UIManager.Instance.Container.last_name;
        string photo = UIManager.Instance.Container.photo_100;

        RPC_Configure(id, fn, ln, photo);
    }

    private void Update()
    {
        if (Object.HasInputAuthority && Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit.collider != null && hit.transform.CompareTag("Player"))
            {
                MyStats colliderStats = hit.collider.GetComponent<MyStats>();
                bool im = colliderStats.UserInfoData.ID == UserInfoData.ID;
                if(!im)
                {
                    Application.OpenURL(string.Concat(vkBaseUrl, colliderStats.UserInfoData.ID));
                }
            }
        }
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