using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public PlayerAttack skill;

    public PlayerBattleController player;

    [SerializeField]
    float coolTime;

    public Image img;
    public Image coolTimeIcon;

    private void Start()
    {
        //img.sprite = skill.iconImg;
        coolTimeIcon.fillAmount = 0;
        coolTime = 0f;
    }

    public void OnClicked()
    {
        if (coolTime > 0)
        {
            Debug.Log("ÄðÅ¸ÀÓ");
            return;
        }
        // Debug.Log(skill.attackName);

        player.pAttack = skill;
        player.ChangeAttack(skill.aST.atkType);
        player.AttackStart();

        StartCoroutine(SkillCoolTime());
    }

    IEnumerator SkillCoolTime()
    {
        float tick = 1f / skill.aST.atkDelay;
        float t = 0;
        coolTime = skill.aST.atkDelay;

        coolTimeIcon.fillAmount = 1;

        while (coolTimeIcon.fillAmount > 0)
        {
            coolTimeIcon.fillAmount = Mathf.Lerp(1, 0, t);
            t += (Time.deltaTime * tick);

            coolTime -= Time.deltaTime;

            yield return null;
        }
    }
}