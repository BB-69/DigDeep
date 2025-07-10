using System.Collections.Generic;
using UnityEngine;

public class CheckpointGenerator : MonoBehaviour
{
    [SerializeField] GameObject checkpointGO;
    Checkpoint[] checkpoints;
    int[] oresCount = new int[4]; //numbers of ore in the level (copper/iron/gold/emerald)

    public void SpawnCheckpointObject(Vector2Int[] points, int playerPosIndex, int[] ores)
    {
        for (int i = 0; i < points.Length; i++)
        {
            var go = Instantiate(checkpointGO, new Vector3(points[i].x, points[i].y, 0), Quaternion.identity);
            if (i == playerPosIndex)
            {
                go.GetComponent<Checkpoint>().isSpawnpoint = true;
                go.GetComponent<Checkpoint>().cleared = true;
            }
            else { checkpoints[i] = go.GetComponent<Checkpoint>(); }
        }
        SetOresNeedOnEachCheckpoint();
    }

    List<int[]> SplitOres(int[] oreCounts)
    {
        List<int[]> result = new List<int[]>();

        foreach (int count in oreCounts)
        {
            int need = Mathf.FloorToInt(count * 0.7f);
            int[] parts = new int[4]; // default is [0,0,0,0]
            int remaining = need;

            for (int i = 0; i < 3; i++)
            {
                parts[i] = Random.Range(0, remaining + 1);
                remaining -= parts[i];
            }

            parts[3] = remaining;
            result.Add(parts);
        }

        return result;
    }

    void SetOresNeedOnEachCheckpoint()
    {
        List<int[]> oresSets = SplitOres(oresCount);
        for (int i = 0; i < 4; i++)
        {
            checkpoints[i].Setup(oresSets[i][0], oresSets[i][1], oresSets[i][2], oresSets[i][3]);
        }
    }
}