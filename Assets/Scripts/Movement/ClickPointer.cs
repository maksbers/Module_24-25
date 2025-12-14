using UnityEngine;

public class ClickPointer : MonoBehaviour
{
    [SerializeField] private VFXManager _vfxManager;
    private InputController _inputController;

    public void Initialize(InputController inputController)
    {
        _inputController = inputController;
    }

    private void Update()
    {
        if (_inputController == null)
            return;

        if (_inputController.HasNewTarget)
            _vfxManager.SpawnPrefab(_vfxManager.PointerPrefab, _inputController.TakeTarget());
    }
}
