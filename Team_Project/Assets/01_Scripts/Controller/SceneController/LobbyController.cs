using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [Serializable]
    public struct SelectSlots
    {
        public Image[] selectImg;
        public Sprite noImg;
        public Sprite[] characterImg;
        public TextMeshProUGUI[] infoTxt;
    }
    [Header("Select Slots")]
    public SelectSlots selectSlots;

    [Serializable]
    public struct CharacterSlots
    {
        public GameObject[] slots;
        public GameObject[] character;
    }
    [Header("Character Slots")]
    public CharacterSlots characterSlots;

    [SerializeField, Space(10)] Sprite createSprite;

    string[] datas;

    private void OnEnable()
    {
        StartCoroutine(LoadData());
    }

    IEnumerator LoadData()
    {
        string url = GameManager.gm.path + "load_character.php";
        WWWForm form = new();
        form.AddField("uid", 0000000001/*GameManager.gm.uid*/);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                datas = www.downloadHandler.text.Split("<br>");
            }
        }

        RoadDatas();
    }

    private void RoadDatas()
    {
        Array.Resize(ref datas, datas.Length - 1);

        foreach (string data in datas)
        {
            string[] info = data.Split(" ");

            int slot = int.Parse(info[0]);
            int characterClass = int.Parse(info[3]);

            selectSlots.selectImg[slot].sprite = selectSlots.characterImg[characterClass];
            selectSlots.infoTxt[slot].text = "Lv." + info[2] + "\n\n" + info[1];

        }
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

        if (selectSlots.selectImg[num].sprite == createSprite)
            SceneManager.LoadScene("11_CreateScene");
        else
        {
            foreach (var charater in characterSlots.slots)
            {
                if (charater == null)
                    return;

                Animator anim = charater.GetComponent<Animator>();
                anim.SetBool("select", false);
            }

            Animator animator = characterSlots.slots[num].GetComponent<Animator>();
            animator.SetBool("select", true);
        }
    }
}
