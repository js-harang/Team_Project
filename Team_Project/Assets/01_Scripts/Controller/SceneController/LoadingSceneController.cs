using System.Collections;
using System.Collections.Generic;
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

    [SerializeField, Space(10)] Image background;
    [SerializeField] List<Sprite> backgroundImg;

    public void Start()
    {
        StartCoroutine(LoadingProcessCoroutine(GameManager.gm.sceneNumber));

        background.sprite = backgroundImg[Random.Range(0, backgroundImg.Count)];
    }

    // 비동기화 씬 로드
    IEnumerator LoadingProcessCoroutine(int sceneNumber)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(sceneNumber);
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

        ao.allowSceneActivation = true;
    }
}
