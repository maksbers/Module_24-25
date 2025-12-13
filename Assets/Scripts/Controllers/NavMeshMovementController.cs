using UnityEngine;
using UnityEngine.AI;

public abstract class NavMeshMovementController : Controller
{
    private const int MinCornersInPathToMove = 2;
    private const float StoppingDistance = 0.2f;

    private const int StartCornerIndex = 0;
    private const int TargetCornerIndex = 1;

    protected readonly IDirectionalMovable Movable;
    protected readonly NavMeshQueryFilter QueryFilter;

    private readonly NavMeshPath _pathToTarget;
    private Vector3 _currentTarget;

    private bool _isMoving;

    protected NavMeshMovementController(IDirectionalMovable movable, NavMeshQueryFilter queryFilter)
    {
        Movable = movable;
        QueryFilter = queryFilter;
        _pathToTarget = new NavMeshPath();
    }

    protected override void UpdateLogic(float deltaTime)
    {
        if (_isMoving == false)
            return;

        if (IsValidPath() == false)
            return;

        UpdateMoveDirection();
        
        if (IsTargetReached())
            StopMoving();
    }

    private void UpdateMoveDirection()
    {
        Vector3 direction;

        if (EnoughCornersInPath(_pathToTarget))
            direction = (_pathToTarget.corners[TargetCornerIndex] - _pathToTarget.corners[StartCornerIndex]).normalized;

        else if (IsPathComplete(_pathToTarget))
            direction = (_currentTarget - Movable.Position).normalized;

        else
            direction = Vector3.zero;

        Movable.SetMoveDirection(direction);
    }

    private bool EnoughCornersInPath(NavMeshPath pathToTarget) => pathToTarget.corners.Length >= MinCornersInPathToMove;

    private bool IsPathComplete(NavMeshPath pathToTarget) => pathToTarget.status == NavMeshPathStatus.PathComplete;

    private bool IsTargetReached()
    {
        float distance = CalculateDistanceToTarget();

        return distance <= StoppingDistance;
    }

    private float CalculateDistanceToTarget()
    {
        if (_pathToTarget.corners.Length < MinCornersInPathToMove)
        {
            return Vector3.Distance(Movable.Position, _currentTarget);
        }

        return NavMeshUtils.GetPathLength(_pathToTarget);
    }

    private bool IsValidPath()
    {
        return NavMeshUtils.TryGetPath(Movable.Position, _currentTarget, QueryFilter, _pathToTarget);
    }

    protected void SetTargetPosition(Vector3 target)
    {
        _currentTarget = target;
        _isMoving = true;
    }

    protected void StopMoving()
    {
        _isMoving = false;
        Movable.SetMoveDirection(Vector3.zero);
    }
}
