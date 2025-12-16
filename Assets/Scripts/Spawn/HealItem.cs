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
        if (other.TryGetComponent(out AgentCharacter character))
            PickUp(character);
    }

    public void PickUp(AgentCharacter character)
    {
        character.TakeHeal(_healValue);
        _vfxManager.SpawnPrefab(_vfxManager.HealVfxPrefab, transform.position);
        _audioManager.PlayHealCollect();

        Destroy(gameObject);
    }
}
