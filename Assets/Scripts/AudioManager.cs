using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSourceStart;
    [SerializeField] private AudioSource audioSourceLoop;
    [SerializeField] private AudioClip musicStart;
    [SerializeField] private AudioClip musicLoop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    public void PlayMusic()
    {
        audioSourceStart.clip = musicStart;
        audioSourceLoop.clip = musicLoop;
        audioSourceLoop.loop = true;

        double startTime = AudioSettings.dspTime + 0.1;
        audioSourceStart.PlayScheduled(startTime);
        audioSourceLoop.PlayScheduled(startTime + musicStart.length);
    }
}
