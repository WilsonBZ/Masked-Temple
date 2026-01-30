using UnityEngine;

public class Mask : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            MaskManager.canSwitch = true;
            Destroy(gameObject);
        }
    }
}
