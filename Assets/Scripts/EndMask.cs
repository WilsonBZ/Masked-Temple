using UnityEngine;
using UnityEngine.Playables;

public class EndMask : MonoBehaviour
{
    [SerializeField] private CutsceneManager cutsceneManager;
    [SerializeField] private PlayableDirector cutscene;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pickUpSound;

    private bool canDestroy = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            audioSource.PlayOneShot(pickUpSound);
            canDestroy = true;
            MaskManager.canSwitch = false;
            transform.GetComponentInChildren<SpriteRenderer>().enabled = false;
            cutsceneManager.SetCutscene(cutscene, 3);
            cutsceneManager.PlayCutscene();
        }
    }

    private void Update()
    {
        if (!audioSource.isPlaying && canDestroy)
            Destroy(gameObject);
    }
}
