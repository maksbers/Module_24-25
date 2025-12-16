using UnityEngine;

public class BombView : MonoBehaviour
{
    private readonly int IsActivatedKey = Animator.StringToHash("IsActivated");

    [SerializeField] private ParticleSystem _explosionEffect;

    private Animator _animator;

    private AudioManager _audioManager;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioManager = AudioManager.Instance;
    }

    public void PlayActivateAnimation() => _animator.SetTrigger(IsActivatedKey);

    public void PlayEffect()
    {
        ParticleSystem instantiatedEffect = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        instantiatedEffect.Play();
        _audioManager.PlayExplosion();
    }
}
