using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class KartController :   Agent
{
    public float speed = 20.0f;
    public float rotationSpeed = 10.0f;

    public Vector3 centerOfMass = new Vector3(0, -1f, 0);

    private Rigidbody rb;

    private Vector3 startPosition;
    private Quaternion startRotation;

    public float episodeDuration = 1000f;  // 10 secs
    public TrackCheckpoints trackCheckpoints;
    

    private IEnumerator EpisodeCoroutine()
    {
        yield return new WaitForSeconds(episodeDuration);
        EndEpisode();
    }

    private void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass;

        //Start the kart in a decent position
        startPosition = transform.position;
        startRotation = transform.rotation;
    }
    public override void OnEpisodeBegin()
    {
        trackCheckpoints.ResetCheckpoints();

        // Reset the kart's position and rotation
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Reset the kart's velocity
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        StartCoroutine(EpisodeCoroutine());

    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            AddReward(-10.0f);  // Give a negative reward for hitting a wall
            Debug.Log("Messed up");
        }
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        // Add existing observations
        sensor.AddObservation(rb.velocity.x);
        sensor.AddObservation(rb.velocity.z);

        // Get the ray perception sensor component
        var raySensorComponent = GetComponent<RayPerceptionSensorComponent3D>();
        RayPerceptionInput input = raySensorComponent.GetRayPerceptionInput();
        RayPerceptionOutput output = RayPerceptionSensor.Perceive(input);

        // Add ray perception observations
        foreach (var rayOutput in output.RayOutputs)
        {
            sensor.AddObservation(rayOutput.HitTagIndex);  // The index of the hit object's tag in the detectable tags list
            sensor.AddObservation(rayOutput.HitFraction);  // The fraction of the ray length at which the hit occurre
        }
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        // Apply the actions to the kart (e.g., steering, acceleration)
        float moveVertical = actions.ContinuousActions[0];
        float moveHorizontal = actions.ContinuousActions[1];

        // Calculate the forward and turning forces
        Vector3 movement = transform.forward * Mathf.Max(0, moveVertical) * speed;
        float turn = moveHorizontal * rotationSpeed;

        // Apply the forces to the Rigidbody
        rb.AddForce(movement);
        rb.AddTorque(0, turn, 0);

        // Calculate a reward
        float reward = CalculateReward();

        // Add the reward
        AddReward(reward);

        //Debug.Log("Reward: " + reward);
    }

    private float CalculateReward()
    {
        float reward = 0f;

                if (rb.velocity.z > 0)
                {
                    reward += rb.velocity.z * 100;
                }
                else if (rb.velocity.z == 0)
                {
                    reward -= 1f;
                }

            return reward;

    }

    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.R))
        {
            EndEpisode();
        }
    }

    private void FixedUpdate()
    {
        // Get input from the user
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the forward force
        Vector3 movement = transform.forward * moveVertical * speed;

        // Apply the forward force to the Rigidbody
        rb.AddForce(movement);

        // Only rotate the kart if it's moving
        if (rb.velocity.magnitude > 0.1f)
        {
            // Calculate the turning force
            float turn = moveHorizontal * rotationSpeed;

            // Apply the turning force to the Rigidbody
            rb.AddTorque(0, turn, 0);
        }
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = Input.GetAxis("Vertical");
        continuousActionsOut[1] = Input.GetAxis("Horizontal");
    }

}
