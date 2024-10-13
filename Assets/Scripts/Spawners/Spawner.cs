using System;
using UnityEngine;

[RequireComponent(typeof(ICounter))]
public abstract class Spawner<T> : MonoBehaviour where T : PoolableObjects<T>
{
    [SerializeField] private Pool<T> _pool;

    private float _counterSpawn = 0;
    private float _counterActivePool = 0;

    public event Action <float> OnSpawn;
    public event Action <float> OnActivePool;
    public event Action <float> OnInstantinate;

    public virtual void SpawnObject(Vector3 start)
    {
        T createdObject = _pool.Get(start);
        _counterSpawn++;
        _counterActivePool++;
        createdObject.Disabled += DestroyObject;
        OnSpawn?.Invoke(_counterSpawn);
        OnInstantinate?.Invoke(_counterActivePool);
        OnActivePool?.Invoke(_pool.CounterInstantinate);
    }

    private void DestroyObject(T obj)
    {
        _pool.Return((T)obj);
        obj.Disabled -= DestroyObject;
        OnRealise(obj);
        _counterActivePool--;
        OnActivePool?.Invoke(_counterActivePool);
    }

    protected virtual void OnRealise(T obj) { }

}
