using UnityEngine;
using UnityEngine.AI;

public class AgentMover
{
    private NavMeshAgent _agent;

    public AgentMover(NavMeshAgent agent, float movementSpeed, float acceleration)
    {
        _agent = agent;
        _agent.speed = movementSpeed;
        _agent.acceleration = acceleration;
    }

    public void SetDestination(Vector3 position) => _agent.SetDestination(position);
    public void SetSpeed(float speed) => _agent.speed = speed;
    public void Stop() => _agent.isStopped = true;
    public void Resume() => _agent.isStopped = false;
}
