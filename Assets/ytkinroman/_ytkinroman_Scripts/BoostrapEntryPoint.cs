using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BoostrapEntryPoint : MonoBehaviour
{
    [SerializeField] private string _nextSceneName;
    [SerializeField] private float _delayValue = 3.0f;


    private void Start ()
    {
        StartCoroutine(LoadingGameCoroutine(_nextSceneName));
    }
    

    private IEnumerator LoadingGameCoroutine (string sceneName)
    {
        Application.targetFrameRate = 60;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone) {
            if (asyncOperation.progress >= 0.9f) {
                yield return new WaitForSeconds(_delayValue);
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}