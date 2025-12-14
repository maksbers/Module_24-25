using UnityEngine;

public interface ICollectable
{
    void PickUp(AgentCharacter character);
    void Initialize(VFXManager vfxManager);
}
