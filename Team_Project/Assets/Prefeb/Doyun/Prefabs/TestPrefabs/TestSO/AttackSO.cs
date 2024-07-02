using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu (menuName ="AttackSO")]
public class AttackSO : ScriptableObject
{
    // 콤보 공격 일시 재생 애니메이션들
    public AnimatorOverrideController[] animOCs;
    
    // 콤보공격시 대미지 순서
    public float[] damage;
    public float coolTime;
    
    [Space(10)]
    // 스킬 아이콘
    public Sprite icon;

    [Space(10)]
    // 공격시 사용할 공격 위치의 배열 인덱스 번호
    public int atkPosIdx;
    // 공격 범위
    public Vector3 atkLength;
}
