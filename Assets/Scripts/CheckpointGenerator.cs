using UnityEngine;

public class CheckpointGenerator : MonoBehaviour
{
    [SerializeField] GameObject checkpointGO;
    int[] oresCount = new int[4];

    public void SpawnCheckpointObject(Vector2Int[] points, int playerPosIndex, int[] ores)
    {
        oresCount = ores;
        for (int i = 0; i < points.Length; i++)
        {
            var go = Instantiate(checkpointGO, new Vector3(points[i].x, points[i].y, 0), Quaternion.identity);
            if (i == playerPosIndex)
            {
                go.GetComponent<Checkpoint>().isSpawnpoint = true;
                go.GetComponent<Checkpoint>().cleared = true;
            }
        }
    }

    void SetOresNeedOnEachCheckpoint()
    {
        
    }
}