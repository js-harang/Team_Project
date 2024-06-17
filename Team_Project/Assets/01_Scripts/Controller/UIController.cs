using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Ű���� �Է½� ������ UI��
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
            if (!gameUI.enabled)                // InteractController���� ��Ȱ��ȯ UI ĵ������ Ȯ���� ����
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

    // �������ͽ� UI Ȱ�� / ��Ȱ��ȭ
    public void StatusOnOff()
    {
        if (statusUI.activeSelf)
        {
            statusUI.SetActive(false);
            return;
        }
        statusUI.SetActive(true);
    }

    // �κ��丮 UI Ȱ�� / ��Ȱ��ȭ
    public void InventoryOnOff()
    {
        if (inventoryUI.activeSelf)
        {
            inventoryUI.SetActive(false);
            return;
        }
        inventoryUI.SetActive(true);
    }

    // ESC �޴� UI Ȱ�� / ��Ȱ��ȭ
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
