using UnityEngine;
using UnityEngine.UI;

public class SelectBasic : MonoBehaviour
{
    [SerializeField] GameObject[] options;
    OptionContent optionContent;

    private void OnEnable()
    {
        foreach (GameObject option in options)
            option.GetComponent<Image>().sprite = GetComponent<SpriteSwap>().swapImage[0];

        GetComponent<Image>().sprite = GetComponent<SpriteSwap>().swapImage[2];

        GameManager.gm.selectObject = gameObject;

        optionContent = FindObjectOfType<OptionContent>();
        optionContent.SetOption();
    }
}
