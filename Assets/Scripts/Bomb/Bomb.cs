using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float _damageRadius = 5f;
    [SerializeField] private float _damageAmount = 30f;
    [SerializeField] private float _delay = 2f;

    private BombView _view;

    private bool _isActivated = false;

    private void Awake()
    {
        _view = GetComponent<BombView>();
    }

    private IEnumerator CoroutineExplosion()
    {
        Debug.Log("Start coroutine");
        yield return new WaitForSeconds(_delay);

        Explode();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isActivated)
            return;

        if (other.GetComponent<IDamageable>() != null)
            Activate();
    }

    private void Activate()
    {
        _isActivated = true;

        _view.PlayActivateAnimation();

        StartCoroutine(CoroutineExplosion());
    }

    private void Explode()
    {
        _view.PlayEffect();

        Collider[] hits = Physics.OverlapSphere(transform.position, _damageRadius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<IDamageable>(out IDamageable damageable))
                damageable.TakeDamage(_damageAmount);
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _damageRadius);
    }
}
