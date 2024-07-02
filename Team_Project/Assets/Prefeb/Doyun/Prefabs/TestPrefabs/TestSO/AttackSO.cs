using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName ="AttackSO")]
public class AttackSO : ScriptableObject
{
    // �޺� ���� �Ͻ� ��� �ִϸ��̼ǵ�
    public AnimatorOverrideController[] animOCs;
    
    // �޺����ݽ� ����� ����
    public float[] damage;
    public float coolTime;
    
    [Space(10)]
    // ��ų ������
    public Sprite icon;

    [Space(10)]
    // ���ݽ� ����� ���� ��ġ�� �迭 �ε��� ��ȣ
    public int atkPosIdx;
    // ���� ����
    public Vector3 atkLength;
}
