using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] Sprite createSprite;

    [SerializeField, Space(10)] GameObject[] selectImg;
    [SerializeField] GameObject[] selectCharater;
    [SerializeField] TextMeshProUGUI[] infoTxt;
    string[] data;
    string[] slot;

    private void OnEnable()
    {
        StartCoroutine(LoadData());
    }

    IEnumerator LoadData()
    {
        string url = GameManager.gm.path + "load_character.php";
        WWWForm form = new();
        form.AddField("uid", 0000000001/*PlayerPrefs.GetString("uid")*/);
        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                data = www.downloadHandler.text.Split("<br>");
                Test();
            }
        }
    }

    private void Test()
    {
        foreach (string s in data)
            Debug.Log(s);
    }

    private void Start()
    {
        
    }

    public void GameStartBtn()
    {
        GameManager.gm.sceneNumber = 2;
        SceneManager.LoadScene("99_LoadingScene");
    }

    public void BackBtn()
    {
        GameManager.gm.sceneNumber = 0;
        SceneManager.LoadScene("99_LoadingScene");
    }

    public void SelectedSlot(int num)
    {
        GameManager.gm.slotNum = num;

        Image image = selectImg[num].GetComponent<Image>();

        if (image.sprite == createSprite)
            SceneManager.LoadScene("11_CreateScene");
        else
        {
            foreach (var charater in selectCharater)
            {
                if (charater == null)
                    return;

                Animator anim = charater.GetComponent<Animator>();
                anim.SetBool("select", false);
            }

            Animator animator = selectCharater[num].GetComponent<Animator>();
            animator.SetBool("select", true);
        }
    }
}
