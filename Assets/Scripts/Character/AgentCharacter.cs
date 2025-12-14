using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDirectionalRotatable, IDamageable
{
    [SerializeField] private float _maxHealth = 100f;
    [SerializeField] private float _injuredMoveSpeedModifier = 0.3f;
    [SerializeField] private float _injuredThreshold = 0.3f;

    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private float _rotationSpeed = 500f;
    [SerializeField] private float _acceleration = 999f;

    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private LayerMask _groundLayer;

    private NavMeshAgent _agent;

    private AgentMover _mover;
    private DirectionalRotator _rotator;
    private AgentJumper _jumper;

    private HealthSystem _healthSystem;
    private bool _isDamageTaken;

    public LayerMask GroundLayer => _groundLayer;
    public Vector3 Position => transform.position;
    public Vector3 CurrentVelocity => _agent.desiredVelocity;
    public Quaternion CurrentRotation => _rotator.CurrentRotation;
    public bool InJumpProcess => _jumper.InProcess;

    public float Health => _healthSystem.CurrentHealth;
    public float MaxHealth => _healthSystem.MaxHealth;
    public float InjuredThreshold => _injuredThreshold;
    public bool IsDead => _healthSystem.IsDead;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _moveSpeed, _acceleration);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
        _jumper = new AgentJumper(_jumpSpeed, _agent, this, _jumpCurve);

        _healthSystem = new HealthSystem(_maxHealth);
    }

    private void Update()
    {
        if (IsDead)
        {
            _agent.velocity = Vector3.zero;
            _agent.isStopped = true;
            return;
        }

        if (InJumpProcess)
            return;

        UpdateSpeed();

        _rotator.Update(Time.deltaTime);
    }

    private void UpdateSpeed()
    {
        float speedModifier = CalculateSpeedModifier();
        _mover.SetSpeed(_moveSpeed * speedModifier);
    }

    private float CalculateSpeedModifier()
    {
        if (Health <= MaxHealth * _injuredThreshold)
            return _injuredMoveSpeedModifier;

        return 1f;
    }

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);
    public void StopMove() => _mover.Stop();
    public void ResumeMove() => _mover.Resume();
    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget)
    {
        return NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);
    }

    public float TakeDamage(float damageAmount)
    {
        _isDamageTaken = true;
        return _healthSystem.TakeDamage(damageAmount);
    }

    public float TakeHeal(float healAmount) => _healthSystem.TakeHeal(healAmount);

    public bool TryGetDamageTaken()
    {
        if (_isDamageTaken == false)
            return false;

        _isDamageTaken = false;
        return true;
    }

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default(OffMeshLinkData);
        return false;
    }

    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);
}
