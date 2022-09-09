using UnityEngine.UI;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //public static UIManager Instance => Singleton<UIManager>.Instance;
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if(!instance)
            {
                instance = FindObjectOfType<UIManager>();
            }

            return instance;
        }
    }

    [Space(10)]
    [SerializeField] GameObject loadingGO;
    [SerializeField] GameObject game;

    [Space(10)]
    [SerializeField] Text playersCountText;
    [SerializeField] Text stepsCountText;

    [Space(10)]
    [SerializeField] InputField myNinckNameInputField;
    [SerializeField] Text otherStepCount;
    [SerializeField] GameObject StatsGO;

    private void Start()
    {
        myNinckNameInputField.onValueChanged.AddListener((s) =>
        {
            if(string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
            {
                return;
            }
            
            //SaveNickName(s);
        });
    }

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

    public void UpdateStepsCountText(int value)
    {
        stepsCountText.text = string.Format("x {0:000}", value);
    }

    public void UpdateOtherStepCount(int value, bool IsShow = true)
    {
        StatsGO.SetActive(IsShow);
        otherStepCount.text = string.Format("{0:000}", value);
    }

    public void SetUserInfoData(string userInfoJsonData)
    {
        Debug.Log(userInfoJsonData);
    }
}
