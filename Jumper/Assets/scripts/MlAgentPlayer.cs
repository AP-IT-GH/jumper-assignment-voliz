using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using UnityEngine;

public class MLAgentPlayer : Agent
{
    public float force = 15f;
    public Transform reset = null;
    public TextMesh score = null;
    public GameObject thrust = null;
    private Rigidbody rb = null;
    private float points = 0;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        ResetMyAgent();
    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        var action = actionBuffers.ContinuousActions[0];
        if (action == 1)
        {
            UpForce();
            //thrust.SetActive(true);
        }
        else
        {
            //thrust.SetActive(false);
        }
    }

    public override void OnEpisodeBegin()
    {
        ResetMyAgent();
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var continuousActionsOut = actionsOut.ContinuousActions;
        continuousActionsOut[0] = 0;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            continuousActionsOut[0] = 1;
            Debug.Log("Up arrow key pressed");
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            AddReward(-1.0f);
            Destroy(collision.gameObject);
            EndEpisode();
        }
        if (collision.gameObject.CompareTag("TopWall"))
        {
            AddReward(-0.9f);
            EndEpisode();
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            AddReward(0.04f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wallreward"))
        {
            AddReward(0.1f);
            points++;
            score.text = points.ToString();
        }
    }

    private void UpForce()
    {
        rb.AddForce(Vector3.up * force, ForceMode.Acceleration);
    }

    private void ResetMyAgent()
    {
        transform.position = reset.position;
    }
}
