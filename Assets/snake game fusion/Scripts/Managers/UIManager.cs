using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => Singleton<UIManager>.Instance;

    [HideInInspector]
    public Container Container { get; set; }

    [Space(10)]
    [SerializeField] GameObject loadingGO;
    [SerializeField] GameObject game;

    [Space(10)]
    [SerializeField] Text playersCountText;

    [Space(10)]
    [SerializeField] RawImage myIcon;
    [SerializeField] Text myName;

    [Space(10)]
    [SerializeField] GameObject StatsGO;
    [SerializeField] Text otherName;
    [SerializeField] RawImage otherIcon;

    #if UNITY_EDITOR || UNITY_STANDALONE
    private void Start()
    {
        SetUserInfoData(string.Empty);
    }
    #endif

    public void Show(int id)
    {
        if(id == 0)
        {
            loadingGO.SetActive(true);
            game.SetActive(false);
        }
        else if(id == 1)
        {
            loadingGO.SetActive(false);
            game.SetActive(true);
        }
    }

    public void UpdatePlayersCount(int value)
    {
        playersCountText.text = string.Format("x {0:000}", value);
    }

    public void SetUserInfoData(string userInfoJsonData)
    {
        Container = JsonUtility.FromJson<Container>(userInfoJsonData);

        #if UNITY_EDITOR || UNITY_STANDALONE
        int rv = Random.Range(0, 1000);
        Container = new Container()
        {
            id = rv,
            first_name = $"no name {rv}",
            last_name = $"no last name {rv}",
            photo_100 = "https://vk.com/images/camera_100.png"
        };
        #endif

        myName.text = $"{Container.first_name}\n{Container.last_name}";
        StartCoroutine(Container.GetTexture(Container.photo_100, (texture) =>
        {
            myIcon.texture = texture;
        }));
    }

    public void ShowStatsGO(bool IsActive, string fn , string ln, string photo)
    {
        StatsGO.SetActive(IsActive);
        if(!StatsGO.activeSelf)
        {
            return;
        }

        otherName.text = $"{fn}\n{ln}";
        StartCoroutine(Container.GetTexture(photo, (texture) =>
        {
            otherIcon.texture = texture;
        }));
    }
}
