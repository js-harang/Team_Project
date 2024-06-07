using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneController : MonoBehaviour
{
    [SerializeField, Space(10)]
    Slider loadingBar;
    [SerializeField]
    TextMeshProUGUI loadingTxt;

    public void LoadingScene(string sceneName)
    {
        StartCoroutine(LoadingProcessCoroutine(sceneName));
    }

    // 비동기화 씬 로드
    IEnumerator LoadingProcessCoroutine(string sceneName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            loadingBar.value = ao.progress;
            loadingTxt.text = (ao.progress * 100f).ToString() + "%";

            if (ao.progress >= 0.9f)
                break;

            yield return null;
        }

        loadingBar.value = 1f;
        loadingTxt.text = "100%";

        yield return new WaitForSeconds(1f);
    }
}
