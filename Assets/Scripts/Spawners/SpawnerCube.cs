using System;
using System.Collections;
using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private SpawnerBomb _spawnBomb;

    private Vector3 _startPoint;
    private Coroutine _coroutine;

    public Action<SpawnerBomb> Changed;

    protected void Start()
    {
        _startPoint = transform.position;

        if (_coroutine == null)
            _coroutine = StartCoroutine(SpawnCube());
    }

    private IEnumerator SpawnCube()
    {
        while (enabled)
        {
            SpawnObject(GetRandomStartPosition());
            Changed?.Invoke(_spawnBomb);

            yield return new WaitForSecondsRealtime(1f);
        }
    }

    protected override void OnRealise(Cube obj)
    {
        _spawnBomb.SpawnBombAtPosition(obj.transform.position);
    }

    private Vector3 GetRandomStartPosition()
    {
        int minRandomValue = -5;
        int maxRandomValue = 5;
        int valueRandom = UnityEngine.Random.Range(minRandomValue, maxRandomValue);

        return new Vector3(
            _startPoint.x + valueRandom,
            _startPoint.y,
            _startPoint.z + valueRandom);
    }
}