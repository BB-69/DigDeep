using System.Collections.Generic;
using UnityEngine;

public class CheckpointGenerator : MonoBehaviour
{
    [SerializeField] GameObject checkpointGO;
    Checkpoint[] checkpoints = new Checkpoint[4];
    int[] oresCount = new int[4]; //numbers of ore in the level (copper/iron/gold/emerald)

    public void SpawnCheckpointObject(List<Vector2Int> points, int[] ores)
    {
        oresCount = ores;
        for (int i = 0; i < points.Count; i++)
        {

            var go = Instantiate(checkpointGO, new Vector3(points[i].x, points[i].y, 0), Quaternion.identity);

            checkpoints[i] = go.GetComponent<Checkpoint>();
        }
        SetOresNeedOnEachCheckpoint();
    }

    List<int[]> SplitOres(int[] oreCounts)
    {
        List<int[]> result = new List<int[]>();

        foreach (int count in oreCounts)
        {
            int total = Mathf.FloorToInt(count * 0.7f);
            int[] parts = new int[4];
            float[] weights = new float[4];
            float weightSum = 0;

            for (int i = 0; i < 4; i++)
            {
                weights[i] = Random.Range(0.8f, 1.2f);
                weightSum += weights[i];
            }

            int allocated = 0;
            for (int i = 0; i < 4; i++)
            {
                parts[i] = Mathf.FloorToInt((weights[i] / weightSum) * total);
                allocated += parts[i];
            }

            int remainder = total - allocated;
            for (int i = 0; i < remainder; i++)
            {
                int index = Random.Range(0, 4);
                parts[index]++;
            }

            result.Add(parts);
        }

        return result;
    }


    void SetOresNeedOnEachCheckpoint()
    {
        List<int[]> oresSets = SplitOres(oresCount);

        for (int checkpointIndex = 0; checkpointIndex < 4; checkpointIndex++)
        {
            int copper = oresSets[0][checkpointIndex];
            int iron = oresSets[1][checkpointIndex];
            int gold = oresSets[2][checkpointIndex];
            int emerald = oresSets[3][checkpointIndex];

            checkpoints[checkpointIndex].Setup(copper, iron, gold, emerald);
        }
    }
}