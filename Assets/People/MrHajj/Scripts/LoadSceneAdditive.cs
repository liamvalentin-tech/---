using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadSceneAdditive : AsyncSceneLoading
{
void Start()
{
    for(int i = 0; i < scene_name.Length; i++)
    {
        LoadScene(scene_name[i]);
    }
}

public override IEnumerator LoadSceneAsync(string scene_name)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene_name, LoadSceneMode.Additive);


        while (!operation.isDone)
        {
            float progressValue=Mathf.Clamp01(operation.progress/0.9f);

            LoadingBarFill.fillAmount = progressValue;

            yield return null;
        }
        LoadingScreen.SetActive(false);
    }


}
