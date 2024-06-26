using UnityEngine;
using UnityEngine.UI;

public class SelectBasic : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Image>().sprite = GetComponent<SpriteSwap>().swapImage[2];

        GameManager.gm.selectObject = gameObject;
    }
}
