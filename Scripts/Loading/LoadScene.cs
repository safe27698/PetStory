using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if (PlayerPrefs.HasKey("LoadScene"))
        //{
        //    StartCoroutine(LoadYourAsyncScene());
        //    Debug.Log("Load Scene : " + PlayerPrefs.GetString("LoadScene"));
        //}
    }
    void Update()
    {
        Debug.LogError(Input.touchCount);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Debug.LogError("Began");
                    break;
                case TouchPhase.Moved:
                    Debug.LogError("Moved");
                    break;
                case TouchPhase.Stationary:
                    Debug.LogError("Stationary");
                    break;
                case TouchPhase.Ended:
                    Debug.LogError("Ended");
                    break;
                case TouchPhase.Canceled:
                    Debug.LogError("Canceled");
                    break;
            }
        }
    }

    IEnumerator LoadYourAsyncScene()
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        string load = PlayerPrefs.GetString("LoadScene");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(load);
        asyncLoad.allowSceneActivation = true;
        yield return null;
        // Wait until the asynchronous scene fully loads
        //while (!asyncLoad.isDone)
        //{
        //    yield return null;
        //}
    }
}
