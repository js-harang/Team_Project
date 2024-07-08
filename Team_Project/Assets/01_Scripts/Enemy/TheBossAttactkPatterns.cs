using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBossAttactkPatterns : MonoBehaviour
{
    FirstBoss fBoss;
    // 보스의 공격 판정 생성 위치
    [SerializeField]
    Transform mouseTransform;
    [SerializeField]
    GameObject fireBreath;

    [SerializeField]
    float biteLength;

    Collider[] players;
    public LayerMask playerLayer;


    private void Start()
    {
        fBoss = GetComponent<FirstBoss>();
    }

    public void BiteAttack()
    {
        int atkPower = fBoss.atkPower;
        Vector3 attackSize = new Vector3(biteLength, biteLength, biteLength);
        players = Physics.OverlapBox(mouseTransform.position, attackSize, Quaternion.identity, playerLayer);

        foreach (Collider player in players)
        {
            if (player.gameObject.CompareTag("Player"))
            {;
                DamagedAction damageAct = player.GetComponent<DamagedAction>();
                damageAct.Damaged(atkPower);
            }
        }
    }

    public void FireAttack()
    {

    }

    private void OnDrawGizmos()
    {
        Vector3 attackSize = new Vector3(biteLength, biteLength, biteLength);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(mouseTransform.position, attackSize);
    }
}
