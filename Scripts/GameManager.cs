using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _gameOver = false;
    private SpawnManager _spawnManager;
    public bool isCoopMode = false;
    int IndexScene;
    private Animator _pauseAnim;
    [SerializeField]    
    private GameObject _pauseMenuPanel;
     
  
    

    private void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        IndexScene = SceneManager.GetActiveScene().buildIndex;
        _pauseAnim = _pauseMenuPanel.GetComponent<Animator>();
        _pauseAnim.updateMode = AnimatorUpdateMode.UnscaledTime;        

        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _gameOver == true)
        {
            SceneManager.LoadScene(IndexScene);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_pauseMenuPanel.activeInHierarchy)
            {                
                PauseGame();             
            }

            else
            {                
                ResumeGame();
            }                           
        }               
    }

    public void GameOver()
    {
        _gameOver = true;
        if (isCoopMode)
        {
            List<Player> players = new List<Player>(GameObject.Find("Player_Container").GetComponentsInChildren<Player>());
            foreach (Player player in players)
            {
                player.GameOverDestruction();
            }
        }
        

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;       
        AudioListener.pause = false;

    }

    private void PauseGame()
    {
        _pauseMenuPanel.SetActive(true);
        _pauseAnim.SetBool("PauseTrue", true);
        Time.timeScale = 0;      
        AudioListener.pause = true;
    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale= 1.0f;        
        AudioListener.pause = false;
    }




    
}
