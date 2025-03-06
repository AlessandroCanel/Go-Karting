using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class CarAgent : Agent
{
    [SerializeField] private TrackCheckpoints trackCheckpoints;
    [SerializeField] private Transform spawnPosition;

    public SCC_InputProcessor inputProcessor;

    private void Awake(){
        inputProcessor = GetComponent<SCC_InputProcessor>();
    }
/*
    private void Start() {
        trackCheckpoints.OnPlayerCorrectCheckpoint += TrackCheckpoints_OnCarCorrectCheckpoint;
        trackCheckpoints.OnPlayerWrongCheckpoint += TrackCheckpoints_OnCarWrongCheckpoint;
    }


    private void TrackCheckpoints_OnCarWrongCheckpoint(object sender, TrackCheckpoints.CarCheckpointEventArgs e) {
        if (e.carTransform == transform) {
            AddReward (-1f);
        }
    }
    private void TrackCheckpoints_OnCarCorrectCheckpoint(object sender, TrackCheckpoints.CarCheckpointEventArgs e) {
        if (e.carTransform == transform) {
            AddReward (1f);
        }
    }
*/
    public override void OnEpisodeBegin()
    {
        transform.position = spawnPosition.position; // Later add random
        transform.foward = spawnPosition.foward;
        TrackCheckpoints.ResetCheckpoint(transform);
        inputProcessor.StopCompletely();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector3 checkpointFoward = TrackCheckpoints.GetNextCheckpoint(transform).transform.foward;
        float directionDot = Vector3.Dot(transform.foward, checkpointFoward);
        sensor.AddObservation(directionDot);
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        // Get the actions from the action buffers
        float throttle = actionBuffers.ContinuousActions[0];
        float steering = actionBuffers.ContinuousActions[1];

        // Create a new SCC_Inputs object
        SCC_Inputs inputs = new SCC_Inputs();

        // Set the throttle and steering inputs
        inputs.throttleInput = throttle;
        inputs.steerInput = steering;

        // Pass the inputs to the SCC_InputProcessor
        inputProcessor.OverrideInputs(inputs);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        // Implement your heuristic for manual control
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetKey(KeyCode.UpArrow) ? 1 : Input.GetKey(KeyCode.DownArrow) ? -1 : 0; // Acceleration and braking
        continuousActionsOut[1] = Input.GetKey(KeyCode.RightArrow) ? 1 : Input.GetKey(KeyCode.LeftArrow) ? -1 : 0; // Steering
    }

    private void OnCollision(Collision collision){
        if(collision.gameObject.TryGetComponent<Wall>(out Wall wall)){
            AddReward(-0.5f);
        }
    }
}
