
using UnityEngine;
using UnityEngine.AI;

public class GolemMovement : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent agent;
    private GolemStatus status;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        status = GetComponent<GolemStatus>();

        if (player == null)
        {
            PlayerMovement foundPlayer =
                FindFirstObjectByType<PlayerMovement>();

            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
        }
    }

    void Update()
    {
        if (player == null || agent == null)
        {
            return;
        }

        if (!agent.isOnNavMesh)
        {
            return;
        }

        if (status != null &&
            status.currentState == GolemState.Frozen)
        {
            agent.isStopped = true;
            return;
        }

        agent.isStopped = false;
        agent.SetDestination(player.position);
    }
}
