using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    public GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;
    [SerializeField]
    private GameObject _player;

    private bool _stopSpawning = false;

    private Enemy _enemy;



    void Start()
    {
        _enemy = GetComponentInChildren<Enemy>();       

       
    }

    private void Update()
    {
        if (_stopSpawning)
        {
            List<Enemy> enemies = new List<Enemy>(_enemyContainer.GetComponentsInChildren<Enemy>());
            foreach (Enemy enemy in enemies)
            {
                enemy.GameOverDestruction();
            }
        }
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerUPSpawnRoutine());

    }

    
   IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(2.0f);
            float RandomX = Random.Range(-7.4f, 7.4f);
            Vector3 posToSpawn = new Vector3(RandomX, 6.6f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator PowerUPSpawnRoutine()
    {
        while (_stopSpawning == false)
        {
            float RandomX = Random.Range(-7.4f, 7.4f);
            Vector3 posToSpawn = new Vector3(RandomX, 7.2f, 0);
            int RandomPowerUp = Random.Range(0, 3);
            Instantiate(_powerups[RandomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
