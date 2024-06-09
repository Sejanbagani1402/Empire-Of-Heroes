using UnityEngine;
using UnityEngine.AI;

public class RandomMovement : MonoBehaviour
{
    public float walkRadius = 10f;
    private NavMeshAgent agent;
    private Vector3 targetPoint;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        // Ensure the agent is on the NavMesh
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            agent.Warp(hit.position);  // Ensure the agent's internal state is updated
            SetRandomDestination();
        }
        else
        {
            Debug.LogError("Agent is not on the NavMesh. Please place the agent on a walkable area.");
        }
    }

    void Update()
    {
        if (agent.isOnNavMesh)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                SetRandomDestination();
            }
        }
        else
        {
            Debug.LogError("Agent is not on the NavMesh in Update");
        }
    }

    void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * walkRadius;
        randomDirection += transform.position;
        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, NavMesh.AllAreas))
        {
            targetPoint = hit.position;
            agent.SetDestination(targetPoint);
            Debug.Log("New Target Point: " + targetPoint);
        }
        else
        {
            Debug.Log("Failed to find a valid NavMesh position");
        }
    }
}