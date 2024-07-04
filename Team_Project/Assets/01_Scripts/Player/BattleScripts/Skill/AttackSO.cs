using UnityEngine;

[CreateAssetMenu (menuName ="AttackSO")]
public class AttackSO : ScriptableObject
{
    // ������� �ִϸ��̼ǵ�
    public AnimatorOverrideController[] animOCs;

    // ��ųâ �� ��ġ�� �ε��� ��ȣ
    public int skillIdx;

    // �޺����ݽ� ����� ����
    public float[] damage;
    public float coolTime;

    public bool isComboing;

    public int useMana;
    
    [Space(10)]
    // ��ų ������
    public Sprite sprite;

    [Space(10)]
    // ���ݽ� ����� ���� ��ġ�� �迭 �ε��� ��ȣ
    public int atkPosIdx;
    // ���� ����
    public Vector3 atkLength;
}