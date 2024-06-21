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
        bC = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bC.BattleState = BattleState.NowBattle;
        StartCoroutine(EnemysActivate(enemys));
        gameObject.SetActive(false);
    }

    // ���ʹ� �����ʵ��� �ణ�� ������ �ְ� Ȱ��ȭ ��Ŵ
    IEnumerator EnemysActivate(GameObject[] enemys) 
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(true);
            bC.EnemyCount++;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
