using UnityEngine;

public class Mask : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickUpSound;

    private bool canDestroy = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(pickUpSound);
            MaskManager.canSwitch = true;
            canDestroy = true;
            transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying && canDestroy)
            Destroy(gameObject);
    }
}
