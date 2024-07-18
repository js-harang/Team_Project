using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    FirstBoss fBoss;
    FirstBossAttactkPatterns bPattern;

    GameObject player;
    Vector3 target;

    bool imGoing;
    [SerializeField]
    float speed;
    float time;

    [SerializeField]
    ParticleSystem fire;
    [SerializeField]
    ParticleSystem explosion;

    [SerializeField]
    float explosionRange;

    Collider[] players;
    [SerializeField]
    LayerMask playerLayer;

    // OnEnable 보다 빠르게 실행할 동작
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        fBoss = FindObjectOfType<FirstBoss>().GetComponent<FirstBoss>();
        bPattern = FindObjectOfType<FirstBossAttactkPatterns>().GetComponent<FirstBossAttactkPatterns>();
    }

    // 파이어볼이 활성화 될때마다 실행
    private void OnEnable()
    {
        imGoing = true;
        target = player.transform.position - transform.position;
        fire.Play();
    }

    void Update()
    {
        if (!imGoing)
            return;

        transform.position += target * speed * Time.deltaTime;
        FireExtinguishing();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
            return;

        imGoing = false;
        fire.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        explosion.Play();
        ExplosionDamage();
        StartCoroutine(FireBallActiveFalse());
    }

    // 명중 시 오버랩으로 범위 내 플레이어들에게 대미지
    void ExplosionDamage()
    {
        players = Physics.OverlapSphere(transform.position, explosionRange, playerLayer);

        if (players == null)
            return;

        foreach (Collider player in players)
        {
            DamagedAction damageAct = player.GetComponent<DamagedAction>();
            damageAct.Damaged(fBoss.atkPower);
            damageAct.KnockBack(transform.position, 7);
        }
    }

    // 명중시 다시 오브젝트풀로 장전하는 동작
    IEnumerator FireBallActiveFalse()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        bPattern.fireBalls.Add(gameObject);
    }

    // 시간이 지나면 다시 오브젝트풀로 장전
    void FireExtinguishing()
    {
        time += Time.deltaTime;
        if (time >= 3)
        {
            gameObject.SetActive(false);
            bPattern.fireBalls.Add(gameObject);
        }
    }
}
