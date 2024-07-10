using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    FirstBoss fBoss;
    TheBossAttactkPatterns bPattern;

    GameObject player;
    Vector3 target;

    bool imGoing;
    [SerializeField]
    float speed;

    [SerializeField]
    ParticleSystem fire;
    [SerializeField]
    ParticleSystem explosion;

    [SerializeField]
    float explosionRange;

    Collider[] players;
    [SerializeField]
    LayerMask playerLayer;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        fBoss = FindObjectOfType<FirstBoss>().GetComponent<FirstBoss>();
        bPattern = FindObjectOfType<TheBossAttactkPatterns>().GetComponent<TheBossAttactkPatterns>();
    }

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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            imGoing = false;
            fire.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            explosion.Play();
            ExplosionDamage();
            StartCoroutine(FireBallActiveFalse());
        }
    }

    void ExplosionDamage()
    {
        players = Physics.OverlapSphere(transform.position, explosionRange, playerLayer);

        if (players == null)
            return;

        foreach (Collider player in players)
        {
            DamagedAction damageAct = player.GetComponent<DamagedAction>();
            damageAct.Damaged(fBoss.atkPower);
        }
    }

    IEnumerator FireBallActiveFalse()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        bPattern.fireBalls.Add(gameObject);
    }
}
