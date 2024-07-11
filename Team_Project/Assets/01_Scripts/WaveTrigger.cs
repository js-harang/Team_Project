using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemys;
    BattleController bC;

    // �÷��̾�� ���˽��� ����
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bC = FindObjectOfType<BattleController>().GetComponent<BattleController>();
            StartCoroutine(EnemysActivate(enemys));
            StartCoroutine(bC.BlockWallOnOff(bC.waveNum));
            bC.BattleState = BattleState.NowBattle;
            gameObject.SetActive(false);
        }
    }

    // ���ʹ� �����ʵ��� �ణ�� ������ �ְ� Ȱ��ȭ ��Ŵ
    IEnumerator EnemysActivate(GameObject[] enemys)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}