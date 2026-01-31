using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private Transform player;
    private Transform mainCamera;

    [SerializeField] private int pointIndex;
    [SerializeField] private bool spawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        mainCamera = GameObject.FindWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            MaskManager.canSwitch = true;
            player.position = transform.position;
            CameraScript.pointIndex = pointIndex - 1;
            mainCamera.position = new Vector3(8 * CameraScript.pointIndex, mainCamera.position.y, mainCamera.position.z);

            spawn = false;
        }
    }
}
