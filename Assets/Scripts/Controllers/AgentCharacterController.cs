using UnityEngine;
using UnityEngine.AI;

public class AgentCharacterController : Controller
{
    private readonly AgentCharacter _agentCharacter;
    private readonly InputController _inputController;

    private Vector3 _cachedTargetPosition;

    public AgentCharacterController(AgentCharacter agentCharacter, InputController inputController)
    {
        _agentCharacter = agentCharacter;
        _inputController = inputController;

        _cachedTargetPosition = _inputController.TargetPosition;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_agentCharacter.IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData))
        {
            if (_agentCharacter.InJumpProcess == false)
            {
                //_agentCharacter.SetRotationDirection(offMeshLinkData.endPos - offMeshLinkData.startPos);
                _agentCharacter.Jump(offMeshLinkData);
            }

            return;
        }

        if (_inputController.TargetPosition != _cachedTargetPosition)
        {
            _cachedTargetPosition = _inputController.TargetPosition;
            _agentCharacter.SetDestination(_cachedTargetPosition);
        }
    }
}