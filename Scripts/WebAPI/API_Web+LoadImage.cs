using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using CI.HttpClient;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using UnityEngine.Events;

public partial class API_Web : MonoBehaviour
{
    public Image LoadImage(string url)
    {


        return null;
    }

    Texture Result(Texture texture)
    {
        RawImage images = null;
        images.material.mainTexture = texture;
        return texture;
    }

    IEnumerator GetTexture(string url,UnityAction<Texture> callback)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            callback(myTexture);
        }
    }
}
