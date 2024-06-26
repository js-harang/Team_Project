using UnityEngine;

public class OptionContent : MonoBehaviour
{
    [SerializeField] GameObject[] content;

    public void SetOption()
    {
        switch (GameManager.gm.selectObject.name)
        {
            case "Option_1":
                content[0].SetActive(true);
                content[1].SetActive(false);
                break;
            case "Option_2":
                content[0].SetActive(false);
                content[1].SetActive(true);
                break;
        }
    }
}
