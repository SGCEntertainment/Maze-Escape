using System.Runtime.InteropServices;
using UnityEngine;

public class JSCaller : MonoBehaviour
{
    public static JSCaller Instance => Singleton<JSCaller>.Instance;

    [DllImport("__Internal")]
    private static extern void VKWebAppJoinGroupMethod(int group_id);

    [DllImport("__Internal")]
    private static extern void VKWebAppCheckNativeAdsMethod();

    [DllImport("__Internal")]
    private static extern void VKWebAppShowNativeAdsRewardMethod();

    [DllImport("__Internal")]
    private static extern void VKWebAppShowNativeAdsInterstitialMethod();

    public void VKWebAppJoinGroupMethodOnClick(int group_id)
    {
        VKWebAppJoinGroupMethod(group_id);
    }

    public void VKWebAppCheckNativeAdsMethodOnClick()
    {
        VKWebAppCheckNativeAdsMethod();
    }

    public void VKWebAppShowNativeAdsRewardMethodOnClick()
    {
        VKWebAppShowNativeAdsRewardMethod();
    }

    public void VKWebAppShowNativeAdsInterstitialMethodOnClick()
    {
        VKWebAppShowNativeAdsInterstitialMethod();
    }
}
