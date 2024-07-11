using UnityEngine;

public class Attack : MonoBehaviour
{
    #region PlayerCombat 에서 사용
    // 공격재생 애니메이션들
    public AnimatorOverrideController[] animOCs;

    [Space(10)]
    // 콤보공격시 대미지 순서
    public float[] damage;
    public int useMana;
    public float knockBackForce;
    
    [Space(10)]
    public float coolTime;
    public float delayComboTime;
    
    [Space(10)]
    // 공격시 사용할 공격 위치의 배열 인덱스 번호
    public int atkPosIdx;
    // 공격 범위
    public Vector3 atkLength;

    [Space(10)]
    public AudioClip hitSound;
    
    [Space(10)]
    public bool isComboing;
    public bool canMoveAtk;
    #endregion

    #region 스킬창 관련에서 사용
    [Space(10)]
    // 스킬의 인덱스 번호
    public int skillIdx;

    [Space(10)]
    // 스킬 아이콘
    public Sprite sprite;
    #endregion
}