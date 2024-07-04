using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstBoss : BossEnemy
{
    // �ٰŸ� ���� ����
    [SerializeField, Space(10)]
    float meleeAttackDistance;
    [SerializeField]
    // ���Ÿ� ���� ����
    float rangeAttackDistance; 

    public override void BossStart()
    {
        // ���� �� ��Ʋ ��Ʈ�ѷ��� ���� ��ü�� ������Ŵ
        bCon = FindObjectOfType<BattleController>().GetComponent<BattleController>();
        bCon.BattleState = BattleState.BossAppear;
        bCon.BossCount++;

        //�ʿ��� ������ ������
        bossAnim = GetComponentInChildren<Animator>();
        player = GameObject.FindWithTag("Player");
    }
    public override void BossMovement()
    {
        switch (BState)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Move:
                Move();
                break;
            default:
                break;
        }
    }

    public override void Appear()
    {
        LookAtPlayer();
        CalcBehaveDelay();
        StartCoroutine(AppearAction());
    }

    IEnumerator AppearAction()
    {
        yield return new WaitForSeconds(2.6f);
        bossAnim.SetTrigger("idle");
        yield return new WaitForSeconds(1f);
        BState = BossState.Move;
    }

    public override void Idle()
    {
        bossAnim.SetTrigger("idle");

        if (TimeCount())
            RandomPattern();
    }

    public override void LookAtPlayer()
    {
        if (player.transform.position.x < transform.position.x)
            transform.forward = Vector3.left;
        else
            transform.forward = Vector3.right;
    }

    public override void Move()
    {
        Vector3 dir = player.transform.position - transform.position;
        LookAtPlayer();
        bossAnim.SetTrigger("move");
        transform.position += dir * moveSpeed * Time.deltaTime;

        if (TimeCount())
            RandomPattern();
    }

    public override void CalcBehaveDelay()
    {
        behaveDelay = Random.Range(1f, 3f);
    }

    public override bool TimeCount()
    {
        time += Time.deltaTime;
        if (time >= behaveDelay)
            return true;
        else
            return false;
    }

    public override void RandomPattern()
    {
        var randomState = System.Enum.GetValues(enumType:typeof(BossState));
        BState = (BossState)randomState.GetValue(Random.Range(1, 4));
        Debug.Log(BState);
    }

    public override void Attack()
    {
        LookAtPlayer();
        bossAnim.SetTrigger("biteAttack");

        if (TimeCount())
            RandomPattern();
    }

    public override void Hit()
    {
        BossStateUpdate();
    }

    public override void BossStateUpdate()
    {
        bossStateUI.SetActive(true);
        bossName_text.text = bossName;
        bossHpBar.value = currentHp / maxHp;
    }

    public override void Die()
    {

    }
}
