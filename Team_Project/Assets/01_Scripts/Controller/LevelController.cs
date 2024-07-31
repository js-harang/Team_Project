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
    }

    /// <summary>
    /// ������
    /// </summary>
    private void LevelUp()
    {
        gm.Lv++;
        UIController.ui.SetLvText();
    }
    #endregion
}
