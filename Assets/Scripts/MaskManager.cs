using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileInfo
{
    public TileBase tileBase;
    public Vector3Int position;

    public TileInfo(TileBase tileBase, Vector3Int position)
    {
        this.tileBase = tileBase;
        this.position = position;
    }
}

public class MaskManager : MonoBehaviour
{
    public Transform[] points;
    public Tilemap tileMap;

    private Transform player;
    private Transform mainCamera;

    // Cooldown
    private float cooldownTime = 0.75f;
    private float cooldownTimer;

    // Mask
    public static bool canSwitch = false;
    public static int maskNum = 0;
    [SerializeField] private float switchDistance = 20.0f;

    // Blocked Switch
    private List<TileInfo> disabledTiles = new List<TileInfo>();

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
        CheckDisabledTiles();
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

    private void BlockedSwitch()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Vector3Int tilePos = tileMap.WorldToCell(points[i].position);
            TileBase tileBase = tileMap.GetTile(tilePos);

            if (tileBase == null) continue;

            TileInfo tileInfo = new TileInfo(tileBase, tilePos);
            disabledTiles.Add(tileInfo);

            tileMap.SetTile(tilePos, null);
        }
    }

    private void SwitchMask()
    {
        if (Input.GetKeyDown(KeyCode.W) && CanSwitchMask() && canSwitch) {
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

            BlockedSwitch();
        }
    }

    private void CheckDisabledTiles()
    {
        bool disabled;
        for (int i = 0; i < disabledTiles.Count; i++)
        {
            disabled = false;
            for (int j = 0; j < points.Length; j++)
            {
                if (disabledTiles[i].position.Equals(tileMap.WorldToCell(points[j].position)))
                {
                    disabled = true;
                    break;
                }
            }

            if (!disabled)
            {
                tileMap.SetTile(disabledTiles[i].position, disabledTiles[i].tileBase);
                disabledTiles.RemoveAt(i);
            }
        }
    }
}
