using UnityEngine;

public class CheckpointSingle : MonoBehaviour
{
    public delegate void CheckpointTriggeredDelegate(CheckpointSingle checkpoint, KartController kart);
    public event CheckpointTriggeredDelegate OnCheckpointTriggered;

    private void OnTriggerEnter(Collider other)
    {
        KartController kart = other.GetComponent<KartController>();
        if (kart != null)
        {
            Debug.Log("Trigger Checkpoint");
            OnCheckpointTriggered?.Invoke(this, kart);
        }
    }
}
