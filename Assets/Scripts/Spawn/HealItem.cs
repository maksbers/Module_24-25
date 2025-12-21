using UnityEngine;

public class HealItem : MonoBehaviour, ICollectable
{
    [SerializeField] private float _healValue = 30f;

    private VFXManager _vfxManager;
    private AudioManager _audioManager;


    public void Initialize(VFXManager vfxManager)
    {
        _vfxManager = vfxManager;
        _audioManager = AudioManager.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IHealable healable))
            PickUp(other.gameObject);
    }

    public void PickUp(GameObject collector)
    {
        if (collector.TryGetComponent(out IHealable healable))
        {
            healable.TakeHeal(_healValue);

            _vfxManager.SpawnPrefab(_vfxManager.HealVfxPrefab, transform.position);
            _audioManager.PlayHealCollect();

            Destroy(gameObject);
        }
    }
}
