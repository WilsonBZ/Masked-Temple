using UnityEngine;

public class SBAppearTrigger : MonoBehaviour
{
    [SerializeField] private GameObject spaceBar;
    private bool playerCollided = false;
    private float fadeTimer = 0.5f;
    private float fadeTime = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerCollided = true;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCollided && fadeTimer > 0)
        {
            fadeTimer -= Time.deltaTime;
            Color color = spaceBar.GetComponent<SpriteRenderer>().color;
            spaceBar.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 1 - fadeTimer / fadeTime);
        }

        else if (playerCollided)
        {
            spaceBar.GetComponent<FadeAway>().enabled = true;
            Destroy(gameObject);
        }
    }
}
