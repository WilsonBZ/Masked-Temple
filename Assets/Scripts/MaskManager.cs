using System.Runtime.CompilerServices;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
    private Transform player;
    private Transform mainCamera;

    // Cooldown
    private float cooldownTime = 0.75f;
    private float cooldownTimer;

    // Mask
    public static bool canSwitch = false;
    private int maskNum = 0;
    [SerializeField] private float switchDistance = 20.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        mainCamera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        SwitchMask();
    }

    private void UpdateTimer()
    {
        cooldownTimer = Mathf.Clamp(cooldownTimer - Time.deltaTime, 0, cooldownTime);
    }

    private bool CanSwitchMask()
    {
        if (cooldownTimer <= 0f)
            return true;

        return false;
    }

    private void SwitchMask()
    {
        if (Input.GetKeyDown(KeyCode.R) && CanSwitchMask() && canSwitch) {
            cooldownTimer = cooldownTime;

            if (maskNum == 0)
            {
                maskNum = 1;
                player.position = new Vector2(player.position.x, player.position.y - switchDistance);
                mainCamera.position = new Vector3(mainCamera.position.x, mainCamera.position.y - switchDistance, mainCamera.position.z);
            }


            else
            {
                maskNum = 0;
                player.position = new Vector2(player.position.x, player.position.y + switchDistance);
                mainCamera.position = new Vector3(mainCamera.position.x, mainCamera.position.y + switchDistance, mainCamera.position.z);
            }
        }
    }
}
