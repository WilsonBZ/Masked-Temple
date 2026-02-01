using UnityEngine;
using UnityEngine.Playables;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private PlayableDirector cutscene;
    [SerializeField] private MaskManager mManager;
    [SerializeField] private AudioManager aManager;
    private PlayerMovement player;

    private bool cutscenePlaying = false;
    private int cutSceneEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        PlayCutscene();
        aManager.PlayMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (cutscene.state != PlayState.Playing && cutscenePlaying)
        {
            player.canMove = true;
            OnCutSceneEnd(cutSceneEnd);
            cutscenePlaying = false;
        }
    }

    public void SetCutscene(PlayableDirector newCutscene, int cutSceneEnd)
    {
        cutscene = newCutscene;
        this.cutSceneEnd = cutSceneEnd;
    }

    public void PlayCutscene()
    {
        cutscene.Play();
        player.canMove = false;
        cutscenePlaying = true;
    }

    public void OnCutSceneEnd(int cutSceneEnd)
    {
        if (cutSceneEnd == 0)
            return;

        else if (cutSceneEnd == 1)
        {
            MaskManager.cutSceneMask = true;
        }

        else if (cutSceneEnd == 2)
        {
            mManager.TeleportObjects();
            MaskManager.canSwitch = true;
        }

        cutSceneEnd = 0;
    }
}
