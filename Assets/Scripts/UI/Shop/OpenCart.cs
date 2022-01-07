using UnityEngine;

public class OpenCart : MonoBehaviour
{

    public GameObject Panel;
    public static bool isPanelOpen = false;

    public void Open()
    {
        if (Panel != null)
        {
            Animator animator = Panel.GetComponent<Animator>();
            if (animator != null)
            {
                bool isOpen = animator.GetBool("open");
                isPanelOpen = isOpen;
                animator.SetBool("open", !isOpen);
            }
        }
    }
}
