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
    // ���� ����
    public Vector3 atkLenght;

    // �÷��̾� ����
    PlayerState pState;

    private void Start()
    {
        ChangeAttack(atkIndex);
        pState = GetComponent<PlayerState>();
    }

    void Update()
    {
        AttackKeyInput();       // ����Ű �Է�
    }

    public void AttackEnemy()
    {
        pAttack.Attack(atkPos);
    }

    void ChangeAttack(int atkIndex)      // Ű�Է� ������ �ش��ϴ� �������� ��ũ��Ʈ ��ȯ
    {
        pAttack = pAttacks[atkIndex];
        pAttack.InitSetting();          // �ش��ϴ� ���� �������� ��ȯ
    }

    void AttackKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.X))        // �Ϲ� ���� �Է�
        {
            Debug.Log("�Ϲݰ��� ��ȯ");
            atkIndex = 0;
            ChangeAttack(atkIndex);
        }
        else if (Input.GetKeyDown(KeyCode.A))   // A Ű ��ų �Է�
        {
            Debug.Log("A��ų ��ȯ");
            atkIndex = 1;
            ChangeAttack(atkIndex);
        }
    }

    // ������ ������
    public void Hurt(float damage)
    {
        pState.UnitState = UnitState.Hurt;
        Debug.Log("Player Damaged :" + damage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(atkPos.position, atkLenght);
    }
}