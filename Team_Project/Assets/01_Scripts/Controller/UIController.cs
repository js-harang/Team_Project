using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryUI;
    [SerializeField]
    GameObject statusUI;
    [SerializeField]
    GameObject escMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            EscMenuOnOff();

        switch (Input.inputString)
        {
            case "u":
                StatusOnOff();
                break;

            case "i":
                InventoryOnOff();
                break;
        }
    }

    public void StatusOnOff()
    {
        if (statusUI.activeSelf)
        {
            statusUI.SetActive(false);
            return;
        }
        statusUI.SetActive(true);
    }

    public void InventoryOnOff()
    {
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
            return;
        }
        inventoryUI.SetActive(true);
    }

    public void EscMenuOnOff()
    {
        if (escMenuUI.activeSelf)
        {
            escMenuUI.SetActive(false);
            return;
        }
        escMenuUI.SetActive(true);
    }
}
