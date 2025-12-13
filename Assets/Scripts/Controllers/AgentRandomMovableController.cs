using UnityEngine;
using UnityEngine.AI;

public class AgentRandomMovableController : Controller
{
    private readonly AgentCharacter _agentCharacter;
    private readonly NavMeshQueryFilter _queryFilter;

    private readonly float _timeToChangeDirection = 2f;
    private readonly float _moveRadius = 10f;

    private float _timer;

    public AgentRandomMovableController(
        AgentCharacter agentCharacter,
        NavMeshQueryFilter queryFilter)
    {
        _agentCharacter = agentCharacter;
        _queryFilter = queryFilter;
    }

    public override void Enable()
    {
        base.Enable();
        SetNewRandomTarget();
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_agentCharacter.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
        {
            if (_agentCharacter.InJumpProcess == false)
                _agentCharacter.Jump(offMeshLinkData);

            return;
        }

        _timer += deltaTime;

        if (_timer >= _timeToChangeDirection)
        {
            SetNewRandomTarget();
            _timer = 0f;
        }
    }

    private void SetNewRandomTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _moveRadius;
        randomDirection += _agentCharacter.Position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, _moveRadius, _queryFilter))
        {
            _agentCharacter.SetDestination(hit.position);
            _agentCharacter.ResumeMove();
        }
    }
}
