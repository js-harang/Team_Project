using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    [SerializeField] Sprite createSprite;

    [SerializeField, Space(10)] GameObject[] selectImg;
    [SerializeField] GameObject[] selectCharater;

    public void SelectedSlot(int num)
    {
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