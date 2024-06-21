using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlotUi : MonoBehaviour
{
    public GameObject skillSlotPanel;
    bool isActivate;

    private void Start()
    {
        skillSlotPanel.SetActive(isActivate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isActivate = !isActivate;
            skillSlotPanel.SetActive(isActivate);
        }
    }
}
