using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadMainScene : MonoBehaviour
{
    public Image loadingBar; 
    public string sceneToLoad;
    public float loadingTime = 5;

    private void Start()
    {
        StartCoroutine(LoadingCoroutine());
		//LoadSceneAsync();
    }

    private IEnumerator LoadingCoroutine()
    {
        float elapsedTime = 0f;
        float incrementTime = loadingTime / 10f;
        float currentFillAmount = 0f;

        if(sceneToLoad == "") yield break;

		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncOperation.allowSceneActivation = false;


        while (elapsedTime < loadingTime)
        {
            elapsedTime += incrementTime;
            currentFillAmount += Random.Range(0.1f, 0.2f);

            if (currentFillAmount > 1f)
            {
                currentFillAmount = 1f;
            }
			
			if(elapsedTime > loadingTime * 0.9f)
				asyncOperation.allowSceneActivation = true;

            loadingBar.fillAmount = currentFillAmount;

            yield return new WaitForSeconds(incrementTime);
        }

        loadingBar.fillAmount = 1f;

    }

	public async void LoadSceneAsync(string scene)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            loadingBar.fillAmount = progress;

            if (asyncOperation.progress >= 0.9f)
            {
                asyncOperation.allowSceneActivation = true;
            }

            await System.Threading.Tasks.Task.Delay(1);
        }
    }

}
