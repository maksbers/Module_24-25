using System.Collections;
using UnityEngine.AI;
using UnityEngine;

public class HealSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnPrefab;
    [SerializeField] private Transform _spawnTarget;
    [SerializeField] private VFXManager _vfxManager;

    [SerializeField] private float _spawnRange = 5.0f;
    [SerializeField] private float _spawnRate = 3f;

    public bool IsEnableHealSpawn { get; private set; }


    private void Awake()
    {
        SetHealSpawnEnable();
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnRate);

            if (IsEnableHealSpawn)
                SpawnWithOffset();
        }
    }

    private void SpawnWithOffset()
    {
        if (_spawnTarget == null) return;

        Vector3 randOffset = GenerateRandomOffset(_spawnRange);
        Vector3 targetPos = _spawnTarget.position + randOffset;

        if (NavMesh.SamplePosition(targetPos, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            GameObject obj = Instantiate(_spawnPrefab, hit.position, Quaternion.identity);

            if (obj.TryGetComponent(out ICollectable collectable))
                collectable.Initialize(_vfxManager);
        }
    }

    private Vector3 GenerateRandomOffset(float range)
    {
        return new Vector3(Random.Range(-range, range), 0f, Random.Range(-range, range));
    }

    public void SetHealSpawnEnable() => IsEnableHealSpawn = true;
    public void SetHealSpawnDisable() => IsEnableHealSpawn = false;
}
