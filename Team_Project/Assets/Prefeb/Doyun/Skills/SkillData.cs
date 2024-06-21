using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill", menuName = "NewSkill")]
public class SkillData : ScriptableObject
{
    #region ǥ�ð���
    // ���� �̸�
    public string attackName;
    // ��ų ������
    public Sprite iconImg;
    #endregion

    #region ���� ���� ����
    // ���� Ÿ��
    public int atkType;
    // ���� ��ġ �ε���
    public int atkPositionIndex;
    // ���� ����
    public Vector3 atkLenght;
    #endregion

    #region ��ġ ����
    // ������
    public int damage;
    // �Ҹ� ����
    public int useMana;
    // ��Ÿ��
    public float coolTime;
    #endregion
}