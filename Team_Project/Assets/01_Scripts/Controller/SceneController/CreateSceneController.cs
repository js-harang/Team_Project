using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class CreateSceneController : MonoBehaviour
{
    [SerializeField] TMP_InputField characterName;

    public void CreateBtn()
    {
        if (characterName.text == "")
        {
            Debug.Log("이름 입력하세요");
            return;
        }

        StartCoroutine(SaveDataPost());
    }

    IEnumerator SaveDataPost()
    {
        string url = GameManager.gm.path + "create_character.php";
        WWWForm form = new();
        form.AddField("uid", PlayerPrefs.GetString("uid"));
        form.AddField("name", characterName.text);
        form.AddField("class", 1);

        using (UnityWebRequest www = UnityWebRequest.Post(url, form))
        {
            yield return www.SendWebRequest();

            if (www.error == null)
            {
                switch (www.downloadHandler.text)
                {
                    case "success":
                        Debug.Log("캐릭터 생성 성공");
                        break;
                    case "name exists":
                        Debug.Log("중복된 닉네임");
                        break;
                    default:
                        Debug.Log("알 수 없는 오류가 발생했습니다.");
                        break;
                }
            }
        }
    }
}
