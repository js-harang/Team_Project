using UnityEngine;

public class Attack : MonoBehaviour
{
    #region PlayerCombat ���� ���
    // ������� �ִϸ��̼ǵ�
    public AnimatorOverrideController[] animOCs;

    [Space(10)]
    // �޺����ݽ� ����� ����
    public float[] damage;
    public int useMana;
    public float knockBackForce;
    
    [Space(10)]
    public float coolTime;
    public float delayComboTime;
    
    [Space(10)]
    // ���ݽ� ����� ���� ��ġ�� �迭 �ε��� ��ȣ
    public int atkPosIdx;
    // ���� ����
    public Vector3 atkLength;

    [Space(10)]
    public AudioClip hitSound;
    
    [Space(10)]
    public bool isComboing;
    public bool canMoveAtk;
    #endregion

    #region ��ųâ ���ÿ��� ���
    [Space(10)]
    // ��ų�� �ε��� ��ȣ
    public int skillIdx;

    [Space(10)]
    // ��ų ������
    public Sprite sprite;
    #endregion
}