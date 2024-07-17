using System.Collections;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject[] enemys;
    BattleController bC;

    // 플레이어와 접촉시의 동작
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bC = FindObjectOfType<BattleController>().GetComponent<BattleController>();
            bC.BattleState = BattleState.NowBattle;
            StartCoroutine(EnemysActivate(enemys));
            StartCoroutine(bC.BlockWallOnOff(bC.waveNum));
            gameObject.SetActive(false);
        }
    }

    // 에너미 스포너들을 약간의 간격을 주고 활성화 시킴
    IEnumerator EnemysActivate(GameObject[] enemys)
    {
        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}