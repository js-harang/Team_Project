using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;

    private void Awake()
    {
        if (gm != null)
            Destroy(this);
        else
        {
            gm = this;

            DontDestroyOnLoad(gm);
        }
    }

    public int sceneNumber;
}
