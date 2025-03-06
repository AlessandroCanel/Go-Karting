using System.Collections.Generic;
using UnityEngine;

public class TrackCheckpoints : MonoBehaviour
{
    [SerializeField] private List<CheckpointSingle> checkpointList;

    private Dictionary<KartController, int> nextCheckpointIndices = new Dictionary<KartController, int>();

        public void ResetCheckpoints()
    {
        nextCheckpointIndices.Clear();
    }

    private void Start()
    {
        // Initialize the checkpoints
        for (int i = 0; i < checkpointList.Count; i++)
        {
            checkpointList[i].OnCheckpointTriggered += CheckpointTriggered;
        }
    }

    private void CheckpointTriggered(CheckpointSingle checkpoint, KartController kart)
    {
        if (!nextCheckpointIndices.ContainsKey(kart))
        {
            nextCheckpointIndices[kart] = 0;
        }

        if (checkpoint == checkpointList[nextCheckpointIndices[kart]])
        {
            kart.AddReward(100f);
            Debug.Log("Correct checkpoint");
            nextCheckpointIndices[kart] = (nextCheckpointIndices[kart] + 1) % checkpointList.Count;
        }
        else
        {
            kart.AddReward(-15f);
            Debug.Log("Wrong checkpoint");
        }
    }
}
