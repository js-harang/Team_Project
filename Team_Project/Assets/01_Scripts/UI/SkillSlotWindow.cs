using UnityEngine;

public class SkillSlotWindow : MonoBehaviour
{
    public static SkillSlotWindow skillSlotWindow;

    private void Awake()
    {
        if (skillSlotWindow == null)
        {
            skillSlotWindow = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
