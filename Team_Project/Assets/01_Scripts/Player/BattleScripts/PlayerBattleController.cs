using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleController : BattleStatus
{
    // (X Ű �Ϲݰ���), (C ����), (A, S, D ��ų)

    // ���ݵ� �迭
    public PlayerAttack[] pAttacks;
    PlayerAttack pAttack;

    // ������ �迭 ��ȣ
    public int atkIndex;

    // ���� ���� ��ġ ����
    public Transform atkPos;
    public Transform[] atkPositions;
    // ���� ����
    public Vector3 atkLenght;

    // �÷��̾� ����
    PlayerState pState;

    Animator pAnim;

    private void Start()
    {
        StartSetting();
        ChangeAttack(atkIndex);
    }

    void Update()
    {
        AttackKeyInput();       // ����Ű �Է�
    }

    public void AttackEnemy()
    {
        pAttack.Attack(atkPos,atkPower);
    }

    void ChangeAttack(int atkIndex)      // Ű�Է� ������ �ش��ϴ� �������� ��ũ��Ʈ ��ȯ
    {
        pAttack = pAttacks[atkIndex];
        pAttack.InitSetting();          // �ش��ϴ� ���� �������� ��ȯ
    }

    public void AttackAnim(PlayerAttack pAttack)
    {
        pAnim.SetTrigger("Attack" + pAttack.aST.atkType);
    }

    void AttackKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.X))        // �Ϲ� ���� �Է�
        {
            Debug.Log("�Ϲݰ���");
            atkIndex = 0;

            atkPos = atkPositions[atkIndex];
            ChangeAttack(atkIndex);
            AttackAnim(pAttack);
        }
        else if (Input.GetKeyDown(KeyCode.Z))   // A Ű ��ų �Է�
        {
            Debug.Log("Z��ų ����");
            atkIndex = 1;

            atkPos = atkPositions[atkIndex];
            ChangeAttack(atkIndex);
            AttackAnim(pAttack);
        }
    }

    // ������ ������
    public void Hurt(float damage)
    {
        pState.UnitState = UnitState.Hurt;
        Debug.Log("Player Damaged :" + damage);
    }

    void StartSetting()
    {
        pState = GetComponent<PlayerState>();
        pAnim = GetComponentInChildren<Animator>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
       // Gizmos.DrawWireCube(atkPos.position, pAttack.aST.atkLenght);
    }
}