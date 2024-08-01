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
        public GameObject[] slots;
        public Image[] selectImg;
        public Sprite[] unitImg;
        public TextMeshProUGUI[] infoTxt;
    }
    [Header("Select Slots")]
    public SelectSlots selectSlots;

    [Serializable]
    public struct UnitSlots
    {
        public GameObject[] slots;
        public GameObject[] units;
    }
    [Header("Unit Slots")]
    public UnitSlots unitSlots;

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
        form.AddField("uid", GameManager.gm.Uid);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                if (www.downloadHandler != null)
                    datas = www.downloadHandler.text.Split("<br>");
            }
        }

        RoadDatas();
    }

    private void RoadDatas()
    {
        if (datas.Length <= 0)
            return;

        Array.Resize(ref datas, datas.Length - 1);

        foreach (string data in datas)
        {
            string[] info = data.Split(" ");

            int slot = int.Parse(info[0]);
            SlotInfo slotinfo = selectSlots.slots[slot].GetComponent<SlotInfo>();

            int unitClass = int.Parse(info[1]);
            slotinfo.unitUid = info[2];
            slotinfo.unitName = info[3];
            slotinfo.lv = int.Parse(info[4]);
            slotinfo.exp = int.Parse(info[5]);
            slotinfo.credit = int.Parse(info[6]);
            slotinfo.skill = info[7];

            selectSlots.selectImg[slot].sprite = selectSlots.unitImg[unitClass];
            selectSlots.infoTxt[slot].text = "Lv." + info[4] + "\n\n" + info[3];

            GameObject unit = Instantiate(unitSlots.units[unitClass]);
            unit.transform.SetParent(unitSlots.slots[slot].transform, false);
        }
    }

    public void GameStartBtn()
    {
        if (GameManager.gm.slotNum != -1)
        {
            SlotInfo info = selectSlots.slots[GameManager.gm.slotNum].GetComponent<SlotInfo>();
            GameManager.gm.UnitUid = info.unitUid;
            GameManager.gm.UnitName = info.unitName;
            GameManager.gm.Lv = info.lv;
            Debug.Log("GameStart" + GameManager.gm.MaxExp);
            GameManager.gm.Exp = info.exp;
            GameManager.gm.Credit = info.credit;
            GameManager.gm.Skill = info.skill;

            GameManager.gm.sceneNumber = 2;
            SceneManager.LoadScene("99_LoadingScene");
        }
        else
            Debug.Log("캐릭터를 선택해주세요");
    }

    public void BackBtn()
    {
        GameManager.gm.sceneNumber = 0;
        SceneManager.LoadScene("99_LoadingScene");
    }

    public void SelectedSlot(int num)
    {
        GameManager.gm.slotNum = num;
        Animator anim;

        if (selectSlots.selectImg[num].sprite == createSprite)
            SceneManager.LoadScene("11_CreateScene");
        else
        {
            foreach (var slot in unitSlots.slots)
            {
                if (slot.transform.childCount == 0)
                    continue;

                anim = slot.transform.GetChild(0).GetComponent<Animator>();
                anim.SetBool("select", false);
            }

            anim = unitSlots.slots[num].transform.GetChild(0).GetComponent<Animator>();
            anim.SetBool("select", true);
        }
    }
}
