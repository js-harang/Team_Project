using UnityEngine;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour
{
    [SerializeField]
    Sprite[] swapImage;

    private void Update()
    {
        if (GameManager.gm.selectObject != this)
            GetComponent<Image>().sprite = swapImage[0];
    }

    public void ClickDownChange()
    {
        GetComponent<Image>().sprite = swapImage[1];
    }

    public void ClickUpChange()
    {
        GetComponent<Image>().sprite = swapImage[2];
    }
}
