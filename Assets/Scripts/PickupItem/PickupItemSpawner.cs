using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickupItemSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private List<Transform> _spawnPoints = new();
    [SerializeField] private List<PickupItem> _pickupItems = new();

    [Header("Spawn Settings")]
    [SerializeField] private int _maxItems = 2;
    [SerializeField] private float _spawnTime = 20f;

    private int _currentCount = 0;
    private float _currentTimeSpawn = 0;

    private Dictionary<Transform, PickupItem> _spawnedItems = new();

    private void Update()
    {
        if (_currentCount == _maxItems)
        {
            return;
        }

        _currentTimeSpawn += Time.deltaTime;
        if (_currentTimeSpawn >= _spawnTime)
        {
            Spawn();
            _currentTimeSpawn = 0;
        }
    }

    private void Spawn()
    {
        if (!_pickupItems.Any())
        {
            Debug.LogWarning("PickupItemSpawner: No pickup items to spawn.");
            return;
        }

        if (!_spawnPoints.Any())
        {
            Debug.LogWarning("PickupItemSpawner: No spawn points available.");
            return;
        }

        int spawnPointIndex = -1;
        Transform spawnPoint = null;

        while (spawnPointIndex == -1)
        {
            int index = Random.Range(0, _spawnPoints.Count);
            spawnPoint = _spawnPoints[index];

            if (_spawnedItems.ContainsKey(spawnPoint))
            {
                Debug.LogWarning($"PickupItemSpawner: Spawn point {spawnPoint.name} is already occupied.");
                continue;
            }

            spawnPointIndex = index;
        }

        int pickupItemIndex = Random.Range(0, _pickupItems.Count);
        PickupItem pickupItem = _pickupItems[pickupItemIndex];

        PickupItem spawnedItem = Instantiate(pickupItem, spawnPoint.position, Quaternion.identity);
        spawnedItem.PickupItemDestroyed += HandlePickupItemDestoyed;
        _currentCount++;

        _spawnedItems.Add(spawnPoint, spawnedItem);
    }

    private void HandlePickupItemDestoyed(object sender, EventArgs args)
    {
        _currentCount--;

        PickupItem item = (sender as PickupItem);
        item.PickupItemDestroyed -= HandlePickupItemDestoyed;

        if (_spawnedItems.ContainsValue(item))
        {
            Transform spawnPoint = _spawnedItems.FirstOrDefault(x => x.Value == item).Key;
            _spawnedItems.Remove(spawnPoint);
        }
    }
}
