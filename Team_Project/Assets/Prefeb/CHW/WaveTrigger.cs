using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        BattleController bC = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bC.BattleState = BattleState.StartWave;
        gameObject.SetActive(false);
    }
}
