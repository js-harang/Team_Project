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
            case "A":
                Debug.Log(1234);
                break;

            case "B":
                Debug.Log(1234);
                break;

            case "C":
                Debug.Log(1234); 
                break;
        }
    }

    public void InventoryOpen()
    {
        inventoryUI.SetActive(true);
    }

    public void StatusOpen()
    {
        statusUI.SetActive(true);
    }
}
