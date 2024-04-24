using UnityEngine;

public class lightreplenisher : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.Instance.Torch.Restore();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerController.Instance.Torch.Restore();
    }
}
