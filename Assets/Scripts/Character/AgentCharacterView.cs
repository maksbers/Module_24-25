using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private float _runningThreshold = 0.1f;
    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int DieKey = Animator.StringToHash("Die");
    private readonly int IsAttackedKey = Animator.StringToHash("IsAttacked");
    private readonly int InJumpProcessKey = Animator.StringToHash("InJumpProcess");

    private const string InjuredLayerName = "InjuredLayer";
    private int _injuredLayerIndex;

    [SerializeField] private float _injuredRunPitchAmount = 0.4f;

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;

    private AudioManager _audioManager;

    private bool _wasJumping;
    private bool _isDead;

    private void Awake()
    {
        _audioManager = AudioManager.Instance;
        _injuredLayerIndex = _animator.GetLayerIndex(InjuredLayerName);
    }

    private void Update()
    {
        UpdateHealth();

        if (_character.IsDead)
        {
            if (_isDead == false)
            {
                _isDead = true;
                _animator.SetTrigger(DieKey);
                StopRunning();
            }
            return;
        }

        if (_character.TryGetDamageTaken())
            _animator.SetTrigger(IsAttackedKey);


        bool isJumping = _character.InJumpProcess;
        _animator.SetBool(InJumpProcessKey, isJumping);

        if (isJumping && _wasJumping != true)
            _audioManager.PlayJump();

        _wasJumping = isJumping;

        if (_character.CurrentVelocity.magnitude > _runningThreshold && isJumping != true)
            StartRunning();
        else
            StopRunning();
    }

    private void UpdateHealth()
    {
        float injuredWeight = 0f;

        if (_character.Health <= _character.MaxHealth * _character.InjuredThreshold)
            injuredWeight = 1f;

        _animator.SetLayerWeight(_injuredLayerIndex, injuredWeight);
    }

    private void StartRunning()
    {
        _animator.SetBool(IsRunningKey, true);

        float currentPitch = 1.0f;

        if (_character.Health <= _character.MaxHealth * _character.InjuredThreshold)
            currentPitch = _injuredRunPitchAmount;

        _audioManager.ToggleRunSFX(true, currentPitch);
    }    

    private void StopRunning()
    {
        _animator.SetBool(IsRunningKey, false);

        _audioManager.ToggleRunSFX(false);
    }
}
