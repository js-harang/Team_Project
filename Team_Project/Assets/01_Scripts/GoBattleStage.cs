using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBattleStage : MonoBehaviour
{
    [SerializeField]
    Canvas goBattleUI;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            goBattleUI.enabled = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            goBattleUI.enabled = false;
        }
    }

    public void GoBattle()
    {
        GameManager.gm.sceneNumber = 4;
        SceneManager.LoadScene("99_LoadingScene");
    }
}
