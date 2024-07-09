using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireBall : MonoBehaviour
{
    [SerializeField]
    FirstBoss fBoss;
    GameObject player;
    Vector3 target;

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
    }

    private void OnEnable()
    {
        target = player.transform.position - transform.position;
        fire.Play();
    }

    void Update()
    {
        transform.position += target * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        fire.Stop();
        explosion.Play();
        ExplosionDamage();
        StartCoroutine(FireBallActiveFalse());
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
    }
}
