using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance => Singleton<UIManager>.Instance;

    [Space(10)]
    [SerializeField] GameObject loadingGO;
    [SerializeField] GameObject game;

    [Space(10)]
    [SerializeField] Text playersCountText;

    [Space(10)]
    [SerializeField] Image myIcon;
    [SerializeField] Text myName;

    [Space(10)]
    [SerializeField] GameObject StatsGO;

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
        Container container = JsonUtility.FromJson<Container>(userInfoJsonData);
        myName.text = $"{container.first_name}\n{container.last_name}";
    }

    public void ShowStatsGO(bool IsActive)
    {
        StatsGO.SetActive(IsActive);
    }
}
