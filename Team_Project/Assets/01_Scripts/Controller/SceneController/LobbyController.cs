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

    [SerializeField, Space(10)] GameObject popup;
    bool isPopupActive = false;
    [SerializeField] TextMeshProUGUI popupTxt;

    [SerializeField] GameObject checkPopup;
    bool isCheckActive = false;
    [SerializeField] TextMeshProUGUI checkTxt;

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

    private void Start()
    {
        GameManager.gm.slotNum = -1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCheckActive)
                CheckBtn();
            else if (isPopupActive)
                DeleteCancleBtn();
            else
                BackBtn();
        }
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

    public void DeleteUnitBtn()
    {
        if (GameManager.gm.slotNum == -1)
        {
            checkTxt.text = "삭제할 캐릭터를 선택해주세요";
            isCheckActive = true;
            checkPopup.SetActive(isCheckActive);
            return;
        }

        SlotInfo info = selectSlots.slots[GameManager.gm.slotNum].GetComponent<SlotInfo>();

        Animator anim;
        anim = unitSlots.slots[GameManager.gm.slotNum].transform.GetChild(0).GetComponent<Animator>();
        anim.SetBool("select", false);

        popupTxt.text = info.unitName + "을(를) 삭제하시겠습니까?";
        isPopupActive = true;
        popup.SetActive(isPopupActive);
    }

    public void CheckBtn()
    {
        isCheckActive = false;
        checkPopup.SetActive(isCheckActive);
    }

    public void DeleteCheckBtn()
    {
        StartCoroutine(DeleteDataPost());
    }

    IEnumerator DeleteDataPost()
    {
        SlotInfo info = selectSlots.slots[GameManager.gm.slotNum].GetComponent<SlotInfo>();
        string url = GameManager.gm.path + "delete_character.php";
        WWWForm form = new();
        form.AddField("unit_uid", info.unitUid);
        form.AddField("name", info.unitName);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                switch (www.downloadHandler.text)
                {
                    case "y":
                        checkTxt.text = "캐릭터 삭제가 성공되었습니다.";

                        Destroy(unitSlots.slots[GameManager.gm.slotNum].transform.GetChild(0).gameObject);
                        var obj = selectSlots.slots[GameManager.gm.slotNum].transform;
                        obj.GetChild(0).gameObject.GetComponent<Image>().sprite = createSprite;
                        obj.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "캐릭터 생성";
                        break;
                    case "n":
                        break;
                    default:
                        checkTxt.text = "알 수 없는 오류가 발생하였습니다.";
                        break;
                }

                isPopupActive = false;
                popup.SetActive(isPopupActive);
                isCheckActive = true;
                checkPopup.SetActive(isCheckActive);
                GameManager.gm.slotNum = -1;
            }
        }
    }

    public void DeleteCancleBtn()
    {
        isPopupActive = false;
        popup.SetActive(isPopupActive);
        GameManager.gm.slotNum = -1;
    }
}
