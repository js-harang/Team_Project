using UnityEngine;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour
{
    /// <summary>
    /// 0 = normal
    /// 1 = highlight
    /// 2 = select
    /// </summary>
    public Sprite[] swapImage;

    public void PointEneterChange()
    {
        if (GameManager.gm.selectObject == gameObject)
            return;

        GetComponent<Image>().sprite = swapImage[1];
    }

    public void PointExitChange()
    {
        if (GameManager.gm.selectObject == gameObject)
            return;

        GetComponent<Image>().sprite = swapImage[0];
    }

    public void ClickDownChange()
    {
        GetComponent<Image>().sprite = swapImage[0];
    }

    public void ClickUpChange()
    {
        GameManager.gm.selectObject.GetComponent<Image>().sprite = swapImage[0];
        GameManager.gm.selectObject = gameObject;
        GetComponent<Image>().sprite = swapImage[2];

        GameObject.Find("Preferences").GetComponent<OptionContent>().SetOption();
    }
}
