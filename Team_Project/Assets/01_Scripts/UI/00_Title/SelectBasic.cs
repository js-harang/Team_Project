using UnityEngine;

public class SelectBasic : MonoBehaviour
{
    private void Start()
    {
        GameManager.gm.selectObject = gameObject;
        GetComponent<SpriteSwap>().ClickUpChange();
    }
}
