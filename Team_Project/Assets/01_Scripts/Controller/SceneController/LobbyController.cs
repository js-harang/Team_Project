using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] Sprite createSprite;

    [SerializeField, Space(10)] GameObject[] selectImg;
    [SerializeField] GameObject[] selectCharater;

    public void GameStartBtn()
    {
        GameManager.gm.sceneNumber = 2;
        SceneManager.LoadScene("99_LoadingScene");
    }

    public void BackBtn()
    {
        GameManager.gm.sceneNumber = 0;
        SceneManager.LoadScene("99_LoadingScene");
    }

    public void SelectedSlot(int num)
    {
        GameManager.gm.slotNum = num;

        Image image = selectImg[num].GetComponent<Image>();

        if (image.sprite == createSprite)
            SceneManager.LoadScene("11_CreateScene");
        else
        {
            foreach (var charater in selectCharater)
            {
                Animator anim = charater.GetComponent<Animator>();
                anim.SetBool("select", false);
            }

            Animator animator = selectCharater[num].GetComponent<Animator>();
            animator.SetBool("select", true);
        }
    }
}
