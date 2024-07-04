using UnityEngine;

[CreateAssetMenu (menuName ="AttackSO")]
public class AttackSO : ScriptableObject
{
    // 공격재생 애니메이션들
    public AnimatorOverrideController[] animOCs;

    // 스킬창 에 위치할 인덱스 번호
    public int skillIdx;

    // 콤보공격시 대미지 순서
    public float[] damage;
    public float coolTime;

    public bool isComboing;

    public int useMana;
    
    [Space(10)]
    // 스킬 아이콘
    public Sprite sprite;

    [Space(10)]
    // 공격시 사용할 공격 위치의 배열 인덱스 번호
    public int atkPosIdx;
    // 공격 범위
    public Vector3 atkLength;
}