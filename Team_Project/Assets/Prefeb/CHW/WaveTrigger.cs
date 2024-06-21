using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemySpawner; 

    // �÷��̾�� ���˽��� ����
    private void OnTriggerEnter(Collider other)
    {
        BattleController bC = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bC.BattleState = BattleState.NowBattle;
        StartCoroutine(EnemySpawnerActivate(enemySpawner));
        gameObject.SetActive(false);
    }

    // ���ʹ� �����ʵ��� �ణ�� ������ �ְ� Ȱ��ȭ ��Ŵ
    IEnumerator EnemySpawnerActivate(GameObject[] enemySpawner) 
    {
        for (int i = 0; i < enemySpawner.Length; i++)
        {
            enemySpawner[i].SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
