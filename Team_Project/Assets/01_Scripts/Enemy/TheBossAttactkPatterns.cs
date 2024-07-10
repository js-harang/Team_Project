using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheBossAttactkPatterns : MonoBehaviour
{
    FirstBoss fBoss;

    // 보스의 공격 판정 생성 위치
    [SerializeField]
    Transform mouseTransform;

    public List<GameObject> fireBalls;
    [SerializeField]
    GameObject firePref;

    [SerializeField]
    float biteLength;

    Collider[] players;
    [SerializeField]
    LayerMask playerLayer;

    private void Start()
    {
        fBoss = GetComponent<FirstBoss>();
        
        FireBallReload();
    }

    public void BiteAttack()
    {
        float atkPower = fBoss.atkPower;
        Vector3 attackSize = new Vector3(biteLength, biteLength, biteLength);
        players = Physics.OverlapBox(mouseTransform.position, attackSize, Quaternion.identity, playerLayer);

        foreach (Collider player in players)
        {
            if (player.gameObject.CompareTag("Player"))
            {
                DamagedAction damageAct = player.GetComponent<DamagedAction>();
                damageAct.Damaged(atkPower);
            }
        }
    }

    public void FireAttack()
    {
        GameObject fireBall = fireBalls[0];
        fireBalls.Remove(fireBall);
        fireBall.transform.position = mouseTransform.position;

        if (fBoss.player.transform.position.x < transform.position.x)
            fireBall.transform.rotation = Quaternion.Euler(0, 0, -90);
        else
            fireBall.transform.rotation = Quaternion.Euler(0, 0, 90);

        fireBall.SetActive(true);
    }

    void FireBallReload()
    {
        fireBalls = new List<GameObject>();
        int firePoolSize = 3;
        for (int i = 0; i < firePoolSize; i++)
        {
            GameObject fireBall = Instantiate(firePref);
            fireBalls.Add(fireBall);
        }
    }
}
