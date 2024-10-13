using System;
using TMPro;
using UnityEngine;

public abstract class Counter<T> : MonoBehaviour, ICounter where T : PoolableObjects<T>
{
    const string SpawnedText = "Заспавленно:";
    const string ActiveSpawnedText = "Активных:";
    const string InitialSpawnedText = "Созданных:";


    private Spawner<T> _spawner;

    [SerializeField] private TextMeshProUGUI _spawnerText;
    [SerializeField] private TextMeshProUGUI _activeObjectText;
    [SerializeField] private TextMeshProUGUI _instantinateText;


    private void Awake()
    {
        _spawner = GetComponent<Spawner<T>>();
    }

    private void OnEnable()
    {
        _spawner.OnSpawn += OnSpawn;
        _spawner.OnActivePool += OnActivePool;
        _spawner.OnInstantinate += OnInstantinate;
    }

    private void OnDisable()
    {
        _spawner.OnSpawn -= OnSpawn;
        _spawner.OnActivePool -= OnActivePool;
        _spawner.OnInstantinate -= OnInstantinate;
    }

    public void OnSpawn(float value)
    {
        _spawnerText.text = $"{SpawnedText} {value}";
    }

    public void OnActivePool(float value)
    {
        _activeObjectText.text = $"{ActiveSpawnedText} {value}";
    }
    public void OnInstantinate(float value)
    {
        _instantinateText.text = $"{InitialSpawnedText} {value}";
    }
}
