using UnityEngine;

[CreateAssetMenu (menuName ="AttackSO")]
public class AttackSO : ScriptableObject
{
    // ������� �ִϸ��̼ǵ�
    public AnimatorOverrideController[] animOCs;

    // ��ų�� �ε��� ��ȣ
    public int skillIdx;

    // �޺����ݽ� ����� ����
    public float[] damage;
    public float coolTime;

    public bool isComboing;
    public bool canMoveAtk;

    public int useMana;

    public float knockBackForce;
    public float delayComboTime;

    public AudioClip hitSound;

    [Space(10)]
    // ��ų ������
    public Sprite sprite;

    [Space(10)]
    // ���ݽ� ����� ���� ��ġ�� �迭 �ε��� ��ȣ
    public int atkPosIdx;
    // ���� ����
    public Vector3 atkLength;
}