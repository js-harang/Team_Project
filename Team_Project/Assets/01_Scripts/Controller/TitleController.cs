using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField, Space(10)]
    TMP_InputField id;
    [SerializeField]
    TMP_InputField pw;

    private void Start()
    {
        Debug.Log(id.text); 
    }

    /// <summary>
    /// ·Î±×ÀÎ
    /// </summary>
    public void LoginCheck()
    {
        if (id.text == "" || pw.text == "")
        {
            Debug.Log("null");
            return;
        }

        GameManager.gm.sceneNumber = 1;
        SceneManager.LoadScene("99_LoadingScene");

        //Search(1, "test");
    }
}
