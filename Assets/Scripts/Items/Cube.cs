using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : PoolableObjects<Cube>
{
    private Renderer _renderer;
    private bool _isDestroed;
    private Color _deafult = Color.yellow;
    private Coroutine _coroutine;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        _isDestroed = false;
        _coroutine = null;
        _renderer.material.color = _deafult;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_isDestroed)
            return;

        if (collision.gameObject.TryGetComponent(out Platform _) == false)
            return;

        _renderer.material.color = Random.ColorHSV();
        _isDestroed = true;

        if (_coroutine == null)
            _coroutine =  StartCoroutine(DeleteCubeWhithDelay());
    }

    private IEnumerator DeleteCubeWhithDelay()
    {
        int minRandomValue = 2;
        int maxRandomValue = 5;
        int valueRandom = Random.Range(minRandomValue, maxRandomValue + 1);

        yield return new WaitForSeconds(valueRandom);

        Disable();
    }
}