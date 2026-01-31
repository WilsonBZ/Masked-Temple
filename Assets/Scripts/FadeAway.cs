using UnityEngine;

public class FadeAway : MonoBehaviour
{
    private float fadeTimer = 0.25f;
    private float fadeTime = 0.25f;
    private bool isFading = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeTimer > 0.0f && isFading)
        {
            fadeTimer -= Time.deltaTime;
            Color color = gameObject.GetComponent<SpriteRenderer>().color;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, fadeTimer / fadeTime);
        }

        else if (isFading)
        {
            Destroy(gameObject);
        }
    }

    public void Fade()
    {
        isFading = true;
    }
}
