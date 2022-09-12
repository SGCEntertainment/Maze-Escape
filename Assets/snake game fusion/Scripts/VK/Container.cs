using System.Collections;
using UnityEngine.Networking;
using UnityEngine;
using System;

[Serializable]
public class Container
{
    public string first_name;
    public string last_name;
    public string photo_100;

    public IEnumerator GetTexture(string photo_100_Url, Action<Texture2D> photo_100TextureAction)
    {
        UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(photo_100_Url);
        yield return unityWebRequest.SendWebRequest();

        photo_100TextureAction.Invoke(DownloadHandlerTexture.GetContent(unityWebRequest));
    }
}