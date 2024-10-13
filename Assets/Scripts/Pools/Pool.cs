using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pool<T> : MonoBehaviour where T : PoolableObjects<T> 
{
    [SerializeField] private T _item;
    [SerializeField] private Transform _startPosition;

    private Queue<T> _pool = new Queue<T>();
    public float CounterInstantinate { get; private set; } = 0;

    public T Get(Vector3 start)
    {
        if (_pool.Count == 0)
            Create(start);

        T item = _pool.Dequeue();
        item.gameObject.SetActive(true);
        item.transform.position = start;

        return item;
    }

    public void Return(T item)
    {
        item.gameObject.SetActive(false);
        _pool.Enqueue(item);
    }

    private void Create(Vector3 position)
    {
        T item = Instantiate(_item, position, Quaternion.identity);
        _pool.Enqueue(item);
        CounterInstantinate ++;
    }
}
