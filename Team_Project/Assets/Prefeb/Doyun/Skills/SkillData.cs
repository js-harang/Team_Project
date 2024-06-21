using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill", menuName = "NewSkill")]
public class SkillData : ScriptableObject
{
    #region 표시관련
    // 공격 이름
    public string attackName;
    // 스킬 아이콘
    public Sprite iconImg;
    #endregion

    #region 공격 정보 관련
    // 공격 타입
    public int atkType;
    // 공격 위치 인덱스
    public int atkPositionIndex;
    // 공격 범위
    public Vector3 atkLenght;
    #endregion

    #region 수치 관련
    // 데미지
    public int damage;
    // 소모 마나
    public int useMana;
    // 쿨타임
    public float coolTime;
    #endregion
}