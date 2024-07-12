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
            Debug.Log("�̸� �Է��ϼ���");
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
                        Debug.Log("ĳ���� ���� ����");
                        break;
                    case "name exists":
                        Debug.Log("�ߺ��� �г���");
                        break;
                    default:
                        Debug.Log("�� �� ���� ������ �߻��߽��ϴ�.");
                        break;
                }
            }
        }
    }
}
