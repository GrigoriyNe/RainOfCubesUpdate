using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bomb : PoolableObjects<Bomb>
{
    private Coroutine _coroutine;
    private Renderer _renderer;
    private float _radius = 10f;
    private float _force = 500f;
    private int _valueRandomLifeBomb;
    private int _minValueLife = 2;
    private int _maxValueLife = 5;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _valueRandomLifeBomb = Random.Range(_minValueLife, _maxValueLife + 1);
    }

    private void OnEnable()
    {
        _coroutine = null;
        _coroutine = StartCoroutine(Life());
    }

    private void Explodive()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);

        foreach (Collider collider in hits)
        {
            if (collider.TryGetComponent(out Rigidbody rigidbody) == false)
                continue;

            rigidbody.AddExplosionForce(_force, transform.position, _radius);
        }
    }

    private IEnumerator Life()
    {
        float timer = 0;
        

        Color color = _renderer.material.color;

        while (timer < _valueRandomLifeBomb)
        {
            float correctTime = timer / _valueRandomLifeBomb;
            color.a = Mathf.Lerp(1f, 0f, correctTime);
            _renderer.material.color = color;
            timer += Time.deltaTime;

            yield return null;
        }

        Explodive();
        Disable();
    }
}
