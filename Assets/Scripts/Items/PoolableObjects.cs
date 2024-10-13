using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PoolableObjects<T> : MonoBehaviour where T : PoolableObjects<T>
{
    public event Action<T> Disabled;

    public void Disable()
    {
        Disabled.Invoke((T)this);
        gameObject.SetActive(false);
    }
}
