using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    static DontDestroy preferences;

    private void Awake()
    {
        if (preferences != null)
            Destroy(this);
        else
        {
            preferences = this;

            DontDestroyOnLoad(preferences);
        }
    }
}
