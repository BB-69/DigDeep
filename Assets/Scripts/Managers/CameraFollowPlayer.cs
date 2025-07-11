using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraSetUp : MonoBehaviour
{
    [SerializeField] CinemachineCamera cinemachineCamera;
    [SerializeField] CinemachineConfiner2D cinemachineConfiner2D;
    [SerializeField] Tilemap tilemap;
    [SerializeField] PolygonCollider2D confinerCollider;

    void Start()
    {
        // cinemachineCamera = GetComponentInChildren<CinemachineCamera>();
        // cinemachineConfiner2D = GetComponentInChildren<CinemachineConfiner2D>();
    }

    public void SetPlayer(Transform player)
    {
        cinemachineCamera.Follow = player;
        cinemachineCamera.LookAt = player;
    }

    public void SetBounds()
    {
        // if (tilemap == null || confinerCollider == null) return;

        // Bounds bounds = tilemap.localBounds;

        // Vector2 min = bounds.min;
        // Vector2 max = bounds.max;

        // Vector2[] points = new Vector2[4];

        // points[0] = new Vector2(min.x, min.y);
        // points[1] = new Vector2(min.x, max.y);
        // points[2] = new Vector2(max.x, max.y);
        // points[3] = new Vector2(max.x, min.y);

        // confinerCollider.pathCount = 1;
        // confinerCollider.SetPath(0, points);
        // cinemachineConfiner2D.InvalidateBoundingShapeCache();
    }

}