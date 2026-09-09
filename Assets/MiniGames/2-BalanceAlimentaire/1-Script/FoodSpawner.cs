using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private Food _foodPrefab;
    [SerializeField] private List<FoodData> _availableFoods;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _foodParent;
    [SerializeField] private float _spawnInterval = 2f;
    [SerializeField] private int _defaultPoolCapacity = 10;
    [SerializeField] private int _maximumPoolSize = 30;

    private ObjectPool<Food> _foodPool;
    private float _spawnTimer;
    private bool _isSpawning;

    private void Awake()
    {
        _foodPool = new ObjectPool<Food>(
            CreateFood,
            TakeFoodFromPool,
            ReturnFoodToPool,
            DestroyFood,
            true,
            _defaultPoolCapacity,
            _maximumPoolSize
        );
    }

    private void Update()
    {
        if (!_isSpawning)
            return;

        _spawnTimer -= Time.deltaTime;

        if (_spawnTimer > 0f)
            return;

        SpawnFood();
        _spawnTimer = _spawnInterval;
    }

    public void StartSpawning()
    {
        _spawnTimer = 0f;
        _isSpawning = true;
    }

    public void PauseSpawning()
    {
        _isSpawning = false;
    }

    public void ResumeSpawning()
    {
        _isSpawning = true;
    }

    public void StopSpawning()
    {
        _isSpawning = false;
    }

    public void SpawnFood()
    {
        if (_availableFoods == null || _availableFoods.Count == 0)
            return;

        Food food = _foodPool.Get();

        FoodData data =
            _availableFoods[Random.Range(0, _availableFoods.Count)];

        food.transform.SetPositionAndRotation(
            _spawnPoint.position,
            Quaternion.identity
        );

        food.Initialize(data);
    }

    public void SpawnBurst(int amount)
    {
        for (int i = 0; i < amount; i++)
            SpawnFood();
    }

    public void ReleaseFood(Food food)
    {
        if (food == null)
            return;

        _foodPool.Release(food);
    }

    private Food CreateFood()
    {
        Food food = Instantiate(
            _foodPrefab,
            _spawnPoint.position,
            Quaternion.identity,
            _foodParent
        );

        food.gameObject.SetActive(false);

        return food;
    }

    private void TakeFoodFromPool(Food food)
    {
        food.gameObject.SetActive(true);
    }

    private void ReturnFoodToPool(Food food)
    {
        food.gameObject.SetActive(false);
    }

    private void DestroyFood(Food food)
    {
        if (food != null)
            Destroy(food.gameObject);
    }
}