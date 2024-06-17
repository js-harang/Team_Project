using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // 키보드 입력시 동작할 UI들
    [SerializeField]
    GameObject inventoryUI;
    [SerializeField]
    GameObject statusUI;
    [SerializeField]
    GameObject escMenuUI;
    [SerializeField]
    Canvas gameUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameUI.enabled)                // InteractController에서 비활성환 UI 캔버스를 확인후 동작
            {
                gameUI.enabled = true;
                return;
            }

            EscButtonOnOff();
        }

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

    // 스테이터스 UI 활성 / 비활성화
    public void StatusOnOff()
    {
        if (statusUI.activeSelf)
        {
            statusUI.SetActive(false);
            return;
        }
        statusUI.SetActive(true);
    }

    // 인벤토리 UI 활성 / 비활성화
    public void InventoryOnOff()
    {
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
            return;
        }
        inventoryUI.SetActive(true);
    }

    // ESC 메뉴 UI 활성 / 비활성화
    public void EscButtonOnOff()
    {
        if (escMenuUI.activeSelf)
        {
            escMenuUI.SetActive(false);
            return;
        }
        escMenuUI.SetActive(true);
    }
}
