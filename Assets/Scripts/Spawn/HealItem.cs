using UnityEngine;

public class HealItem : MonoBehaviour, ICollectable
{
    [SerializeField] private float _healValue = 30f;
    
    private VFXManager _vfxManager;

    public void Initialize(VFXManager vfxManager)
    {
        _vfxManager = vfxManager;
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

        Destroy(gameObject);
    }
}
