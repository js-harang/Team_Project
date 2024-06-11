using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    // (X Ű �Ϲݰ���), (C ����), (A, S, D ��ų)

    // ���ݵ� �迭
    public PlayerAttack[] pAttacks;
    PlayerAttack pAttack;

    // ������ �迭 ��ȣ
    public int atkIndex;

    // ���� ���� ��ġ ����
    public Transform atkPos;

    private void Start()
    {
        pAttacks[0].InitSetting();
    }

    void Update()
    {
        AttackKeyInput();       // ����Ű �Է�
    }

    public void AttackEnemy()
    {
        pAttack.Attack(atkPos);
    }

    void ChangeAttck(int atkIndex)      // Ű�Է� ������ �ش��ϴ� �������� ��ũ��Ʈ ��ȯ
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
            ChangeAttck(atkIndex);
        }
        else if (Input.GetKeyDown(KeyCode.A))   // A Ű ��ų �Է�
        {
            Debug.Log("A��ų ��ȯ");
            atkIndex = 1;
            ChangeAttck(atkIndex);
        }
    }
}