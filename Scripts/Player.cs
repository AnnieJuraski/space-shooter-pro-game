using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _fireRate = 0.35f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShot;
    [SerializeField]
    private GameObject _shieldVisualizer;    
    [SerializeField]
    private bool _canTripleShot = false;
    [SerializeField]
    private bool _speedBoostActive = false;
    [SerializeField]
    private bool _isShieldsActive = false;
    [SerializeField]
    private GameObject _rightEngine, _leftEngine;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private AudioClip _laserSoundClip;
    
    public bool _PlayerOne = false;
    
    public bool _PlayerTwo = false;    
       

    Coroutine _tripleShotPowerDown;
    Coroutine _speedBoostPowerDown;

    private AudioSource _audioSource;
    private UIManager _uiManager;
    private SpawnManager _spawnManager;
    private GameManager _gameManager;
    private float _newFire = 0.0f;
  
  

    void Start()
    {
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        
        if (_gameManager.isCoopMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }
        
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();      
        _audioSource = GetComponent<AudioSource>();
        

        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is NULL");
        }

        if (_uiManager == null)
        {
            Debug.Log("The UI Manager is NULL");
        }

        if (_audioSource == null)
        {
            Debug.Log("Audio Source in NULL");
        }

        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    void Update()
    {
        if (Time.timeScale == 0)
        
            return;
        
        
        CalculateMovement();
                          

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && Time.time > _newFire && _PlayerTwo == false)
        
            ShootLaser();
        


        else if (Input.GetKeyDown(KeyCode.Space) && Time.time > _newFire && _PlayerOne == true)
        
            ShootLaser();
        

        else if (Input.GetMouseButtonDown(0) && Time.time > _newFire && _PlayerTwo == true)
        
            ShootLaser();
        

    }




    void CalculateMovement()
    {
        float horizontalInput;
        float verticalInput;
        

        if (!_PlayerTwo)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
        }
        else
        {
            
            horizontalInput = Input.GetAxisRaw("P2Horizontal");
            verticalInput = Input.GetAxisRaw("P2Vertical");
        }

        Vector3 direction = new(horizontalInput, verticalInput, 0);

        float currentSpeedBoost = _speedBoostActive ? 1.5f : 1;

        transform.Translate(_speed * currentSpeedBoost * Time.deltaTime * direction);


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 0), 0);


        if (transform.position.x > 9.6f)
        
            transform.position = new Vector3(-9.6f, transform.position.y, 0);
        
        else if (transform.position.x < -9.6f)
        
            transform.position = new Vector3(9.6f, transform.position.y, 0);
        

    }

            

  
    void ShootLaser()
    {
        _newFire = Time.time + _fireRate;

        var ShotType = _canTripleShot ? _tripleShot : _laserPrefab;

        Instantiate(ShotType, transform.position + new Vector3(0, 0.44f, 0), Quaternion.identity);              

        _audioSource.Play();
    }
    
   


    public void Damage()
    {
        if (_isShieldsActive == true)
        {
            _isShieldsActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }

        else if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        


        if (_lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _spawnManager.OnPlayerDeath();
            _gameManager.GameOver();            
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {        
        _canTripleShot = true;
        if (_tripleShotPowerDown != null)
        {
            StopCoroutine(_tripleShotPowerDown);
        }
        _tripleShotPowerDown = StartCoroutine(TripleShotPowerDownRoutine());
        return;

    }

    public void SpeedBoostPowerupActive()
    {        
        _speedBoostActive = true;
        if (_speedBoostPowerDown != null)
        {
            StopCoroutine(_speedBoostPowerDown);
        }
        _speedBoostPowerDown = StartCoroutine(SpeedBoostPowerDownRoutine());
        return;
    }

    public void ShieldsActive()
    {
        _isShieldsActive = true;
        _shieldVisualizer.SetActive(true);
    }


    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _canTripleShot = false;
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false;
    }

    public void GameOverDestruction ()
    {
        Destroy(this.gameObject);       

    }

    
}
