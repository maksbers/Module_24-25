using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private GameObject _healVfxPrefab;
    [SerializeField] private GameObject _pointerPrefab;

    [SerializeField] private float _destroyTime = 1f;

    public GameObject HealVfxPrefab => _healVfxPrefab;
    public GameObject PointerPrefab => _pointerPrefab;

    public void SpawnPrefab(GameObject prefab, Vector3 position)
    {
        GameObject pointer = Instantiate(prefab, position, Quaternion.identity);
        Destroy(pointer, _destroyTime);
    }
}
