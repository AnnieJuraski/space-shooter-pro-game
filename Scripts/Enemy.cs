using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speedEnemy = 6.0f;    
    [SerializeField]
    private GameObject _enemy_explosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;

    private float _FireRate = 3.0f;
    private float _canFire = -1f;  
    private UIManager _uiManager;
    private GameManager _gameManager;
   

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_uiManager == null)
        {
            Debug.LogError("Can't find UIManager");
        }
    }

    
    void Update()
    {
        Movement();

        if (Time.time > _canFire)
        {
            _FireRate = Random.Range(2f, 5f);
            _canFire = Time.time + _FireRate;
            GameObject enemylaser = Instantiate(_laserPrefab, transform.position + new Vector3 (0, -0.52f, 0), Quaternion.identity);
            Laser[] lasers = enemylaser.GetComponentsInChildren<Laser>();

            for (int i =0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemy();
            }
            
        }

       
    }

    void Movement()
    {

        transform.Translate(Vector3.down * _speedEnemy * Time.deltaTime);

        if (transform.position.y < -6.5f)
        {
            float RandomX = Random.Range(-7.4f, 7.4f);
            transform.position = new Vector3(RandomX, 6.6f, 0);
        }

        
    }

   


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                _uiManager.PenaltyScore();
                player.Damage();
            }
            Instantiate(_enemy_explosionPrefab, transform.position + new Vector3 (0, -0.51f, 0), Quaternion.identity);  
            Destroy(this.gameObject);
        }

        if (other.tag == "Laser" && transform.position.y < 6.2f)
        {                        
            Laser laser = other.GetComponent<Laser>();
            if (laser.IsEnemyLaser == false)
            {
                Destroy(other.gameObject);
                _uiManager.UpdateScore();
                Instantiate(_enemy_explosionPrefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            
            
            
        }
    }


    public void GameOverDestruction()
    {        
        Destroy(this.gameObject);
    }
}
