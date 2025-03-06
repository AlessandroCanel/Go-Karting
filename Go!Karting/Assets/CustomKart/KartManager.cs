using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class KartBehaviour : MonoBehaviour
{
    public static List<KartBehaviour> AllKarts = new List<KartBehaviour>();

    private void Awake()
    {
        AllKarts.Add(this);
    }

    private void OnDestroy()
    {
        AllKarts.Remove(this);
    }

    // Rest of your code...
}
