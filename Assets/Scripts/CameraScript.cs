using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private Camera mainCamera;

    [SerializeField] private float width;
    [SerializeField] private float height;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
