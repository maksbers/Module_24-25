using UnityEngine;

public interface ICollectable
{
    void PickUp(GameObject collector);
    void Initialize(VFXManager vfxManager);
}
