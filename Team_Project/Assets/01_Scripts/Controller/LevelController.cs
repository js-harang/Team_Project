using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController lc;
    private void Awake()
    {
        if (lc != null)
            Destroy(gameObject);
        else
            lc = this;
    }

    GameManager gm;
    public PlayerCombat player;

    int exp;

    private void Start()
    {
        gm = GameManager.gm;
    }

    #region 유저 정보 세팅
    /// <summary>
    /// 경험치 추가
    /// </summary>
    public void SumEXP(int exp)
    {
        this.exp = gm.Exp;

        this.exp += exp;

        // 레벨업 가능한 경험치 인지 확인
        while (this.exp >= gm.MaxExp)
        {
            this.exp -= gm.MaxExp;
            LevelUp();
        }

        gm.Exp = this.exp;
        UIController.ui.SetExpSlider(gm.Exp, gm.MaxExp);
    }

    /// <summary>
    /// 레벨업
    /// </summary>
    private void LevelUp()
    {
        gm.Lv++;
        player.CurrentHp = gm.MaxHp;
        player.CurrentMp = gm.MaxMp;
        UIController.ui.SetLvText();
    }
    #endregion
}
