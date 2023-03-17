using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _RotateSpeed = -20.0f;
    [SerializeField]
    private GameObject _explosionPrefab;

    private int _lifeAsteroid = 3;
    private SpawnManager _spawnManager;



    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
    }
    void Update()
    {
        transform.Rotate(Vector3.forward * _RotateSpeed * Time.deltaTime);  
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _lifeAsteroid--;
            Destroy(other.gameObject);

            if (_lifeAsteroid < 1)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity); 
                _spawnManager.StartSpawning();
                Destroy(this.gameObject);
            }
        }
    }
}
