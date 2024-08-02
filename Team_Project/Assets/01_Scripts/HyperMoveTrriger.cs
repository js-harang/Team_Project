using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HyperMoveTrriger : MonoBehaviour
{
    [SerializeField] Transform landingPosition;

    PlayerState pState;

    [SerializeField, Space(10)]
    Slider loadingBar;
    [SerializeField]
    TextMeshProUGUI loadingTxt;

    [SerializeField, Space(10)] Image background;
    [SerializeField] List<Sprite> backgroundImg;

    [SerializeField, Space(10)] GameObject loadingObj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            pState = other.GetComponent<PlayerState>();
            pState.UnitState = UnitState.Wait;

            loadingObj.SetActive(true);
            LoadingStart(other);
        }
    }

    public void LoadingStart(Collider other)
    {
        StartCoroutine(LoadingProcessCoroutine(other));

        background.sprite = backgroundImg[Random.Range(0, backgroundImg.Count)];
    }

    // 비동기화 씬 로드
    IEnumerator LoadingProcessCoroutine(Collider other)
    {
        bool trigger = false;
        float timmer = 0;

        while (!trigger)
        {
            yield return new WaitForSeconds(0.1f);
            timmer += 4.5f;

            loadingBar.value = timmer / 100;
            loadingTxt.text = Mathf.RoundToInt(timmer).ToString() + "%";

            if (timmer >= 90f)
            {
                trigger = true;
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        loadingBar.value = 1f;
        loadingTxt.text = "100%";
        other.transform.position = landingPosition.position;

        yield return new WaitForSeconds(1f);

        loadingObj.SetActive(false);
        pState.UnitState = UnitState.Idle;
    }
}
