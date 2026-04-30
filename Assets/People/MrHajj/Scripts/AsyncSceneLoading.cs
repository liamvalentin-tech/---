using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncSceneLoading : MonoBehaviour
{
    public Image LoadingBarFill;
    public GameObject LoadingScreen;
    public string[] scene_name;
    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
        LoadingScreen.SetActive(true);
           LoadScene(scene_name[0]);
        }

        
    }
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public virtual IEnumerator LoadSceneAsync(string scene_name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene_name);

        LoadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue=Mathf.Clamp01(operation.progress/0.9f);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
    }
}
