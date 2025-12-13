using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AgentJumper
{
    private float _rotationSpeedMultiplyer = 4f;

    private float _speed;
    private NavMeshAgent _agent;
    private AnimationCurve _yOffsetCurve;

    private MonoBehaviour _coroutineRunner;

    private Coroutine _jumpProcess;

    public AgentJumper(
        float speed,
        NavMeshAgent agent,
        MonoBehaviour coroutineRunner,
        AnimationCurve yOffsetCurve)
    {
        _speed = speed;
        _agent = agent;
        _coroutineRunner = coroutineRunner;
        _yOffsetCurve = yOffsetCurve;
    }

    public bool InProcess => _jumpProcess != null;

    public void Jump(OffMeshLinkData offMeshLinkData)
    {
        if (InProcess)
            return;

        _jumpProcess = _coroutineRunner.StartCoroutine(JumpProcessCoroutine(offMeshLinkData));
    }

    private IEnumerator JumpProcessCoroutine(OffMeshLinkData offMeshLinkData)
    {
        Vector3 startPos = _agent.transform.position;
        Vector3 endPos = offMeshLinkData.endPos;

        float duration = Vector3.Distance(startPos, endPos) / _speed;

        float progress = 0f;

        Quaternion startRotation = _agent.transform.rotation;
        Vector3 directionToEnd = endPos - startPos;
        directionToEnd.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(directionToEnd);

        while (progress < duration)
        {
            float normalizedProgress = progress / duration;
            float yOffset = _yOffsetCurve.Evaluate(normalizedProgress);

            Vector3 currentPos = Vector3.Lerp(startPos, endPos, normalizedProgress);
            currentPos.y += yOffset;
            _agent.transform.position = currentPos;

            _agent.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, normalizedProgress * _rotationSpeedMultiplyer);
            progress += Time.deltaTime;

            yield return null;
        }

        _agent.CompleteOffMeshLink();
        _jumpProcess = null;
    }
}
