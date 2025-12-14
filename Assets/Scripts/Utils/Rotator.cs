using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 30f;

    private void Awake()
    {
        float randomAngle = Random.Range(0f, 360f);
        transform.Rotate(Vector3.up * randomAngle);
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}
