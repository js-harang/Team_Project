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

    #region ���� ���� ����
    /// <summary>
    /// ����ġ �߰�
    /// </summary>
    public void SumEXP(int exp)
    {
        this.exp = gm.Exp;

        this.exp += exp;

        // ������ ������ ����ġ ���� Ȯ��
        while (this.exp >= gm.MaxExp)
        {
            this.exp -= gm.MaxExp;
            LevelUp();
        }

        gm.Exp = this.exp;
        UIController.ui.SetExpSlider(gm.Exp, gm.MaxExp);
    }

    /// <summary>
    /// ������
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
