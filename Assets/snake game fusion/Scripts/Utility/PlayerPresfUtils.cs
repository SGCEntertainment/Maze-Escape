using UnityEngine;

static class PlayerPresfUtils
{
    const string nickKey = "nickname";
    public static void SetNickName(string nickName)
    {
        PlayerPrefs.SetString(nickKey, nickName);
        PlayerPrefs.Save();
    }

    public static string GetNickName()
    {
        return PlayerPrefs.HasKey(nickKey) ? PlayerPrefs.GetString(nickKey) : string.Empty;
    }
}
