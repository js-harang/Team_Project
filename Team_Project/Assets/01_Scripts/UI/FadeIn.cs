using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    static FadeIn fade;

    private void Awake()
    {
        if (fade != null)
            Destroy(gameObject);
        else
        {
            fade = this;

            DontDestroyOnLoad(fade);
        }
    }

    [SerializeField] Image fadeImg;
    [SerializeField] float fadeTime;

    private void Start()
    {
        SceneManager.sceneLoaded += LoadedsceneEvent;
    }

    private void LoadedsceneEvent(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "99_LoadingScene")
            return;

        fadeImg.color = new Color(0, 0, 0, 1);

        StartCoroutine(StartFadeIn());
    }

    IEnumerator StartFadeIn()
    {
        float currentTime = 0;
        float percent;

        while (fadeImg.color.a > 0)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = fadeImg.color;
            color.a = Mathf.Lerp(1, 0, percent);
            fadeImg.color = color;

            yield return null;
        }
    }
}
