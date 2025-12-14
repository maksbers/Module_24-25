using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] private float _amplitude = 0.5f; 
    [SerializeField] private float _frequency = 1f;

    private Vector3 _startPos;

    private void Awake()
    {
        _startPos = transform.position;
    }
    private void Update()
    {
        float newY = _startPos.y + Mathf.Sin(Time.time * _frequency) * _amplitude;

        transform.position = new Vector3(_startPos.x, newY, _startPos.z);
    }
}
