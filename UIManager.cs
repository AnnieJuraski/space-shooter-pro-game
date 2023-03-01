using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Sprite[] _livesSprites;
    [SerializeField]
    private Image _livesIMG;
    [SerializeField]
    private Text _textGameOver;

    private GameManager _gameManager;
    private Enemy _enemy;
    private int _score;

    private SpawnManager _spawnManager;

    private void Start()
    {
        _textGameOver.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        



        if (_gameManager == null)
        {
            Debug.Log("GameManager is null");
        }
    }

    
        

    public void UpdateScore()
    {
        _score += 10;
        _scoreText.text = "Score: " + _score;
    }

    public void PenaltyScore()
    {
        if (_score >= 5)
        {
            _score -= 5;
            _scoreText.text = "Score: " + _score;
        }
    }


    public void UpdateLives(int currentLives)
    {
        _livesIMG.sprite = _livesSprites[currentLives];

        if (currentLives == 0)
        {
            _gameManager.GameOver();
            StartCoroutine(GameOverFlickerRoutine());
        }
    }
       

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _textGameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _textGameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

    }




}
