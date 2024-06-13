using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject statusUI;

    private void Update()
    {
        switch (Input.inputString)
        {
            case "u":
                StatusOpen();
                break;

            case "i":
                InventoryOpen();
                break;
        }
    }

    public void StatusOpen()
    {
        if (statusUI.activeSelf)
        {
            statusUI.SetActive(false);
            return;
        }
        statusUI.SetActive(true);
    }

    public void InventoryOpen()
    {
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
            return;
        }
        inventoryUI.SetActive(true);
    }
}
