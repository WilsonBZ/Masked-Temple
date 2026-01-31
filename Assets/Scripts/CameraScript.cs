using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class CameraScript : MonoBehaviour
{
    private Transform player;
    private Camera mainCamera;

    private Vector2 pos;

    // Colliders
    [SerializeField] private Vector2 colliderPos;
    [SerializeField] private Vector2 colliderSize;
    [SerializeField] private LayerMask colliderMask;

    // Camera Points
    [SerializeField] private Transform[] points;
    private int pointIndex = 0;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.limeGreen;
        Gizmos.DrawWireCube(transform.position + (Vector3)colliderPos, colliderSize);
        Gizmos.DrawWireCube(transform.position - (Vector3)colliderPos, colliderSize);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        mainCamera = transform.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera(CheckCollider());
    }

    private int CheckCollider()
    {
        pos = transform.position;
        if (Physics2D.OverlapBox(pos + colliderPos, colliderSize, 0, colliderMask))
            return 1;

        else if (Physics2D.OverlapBox(pos - colliderPos, colliderSize, 0, colliderMask))
            return 2;

        return 0;
    }

    private void MoveCamera(int action)
    {
        if (action == 0)
            return;

        else if (action == 1)
        {
            pointIndex++;

            if (pointIndex >= points.Length)
            {
                pointIndex = points.Length - 1;
                return;
            }

            transform.position = new Vector3(points[pointIndex].position.x, transform.position.y, transform.position.z);
            pos = transform.position;
            player.position = new Vector2(pos.x - colliderPos.x + 0.5f, player.position.y);
        }

        else if (action == 2)
        {
            pointIndex--;

            if (pointIndex < 0)
            {
                pointIndex = 0;
                return;
            }

            transform.position = new Vector3(points[pointIndex].position.x, transform.position.y, transform.position.z);
            pos = transform.position;
            player.position = new Vector2(pos.x + colliderPos.x - 0.5f, player.position.y);
        }
    }
}
