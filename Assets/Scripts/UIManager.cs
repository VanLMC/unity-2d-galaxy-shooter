using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Image livesImageDisplay;
    public Image titleScreen;
    public Sprite[] livesSprites;
    public Text scoreText;
    public int score;
    private bool _isGameOver = true;

    [SerializeField]
    private GameObject _player;

    public void Update(){
        if(_isGameOver && Input.GetKey("space")){
            StartGame();
        }
    }

    public void UpdateLives(int currentLives){
        livesImageDisplay.sprite = livesSprites[currentLives];
    }

    public void UpdateScore(){
        score += 10;
        scoreText.text = "Score: " + score;
    }
    //Todo: refactor: put these methods on a game manager
    public void StartGame(){
        titleScreen.enabled = false;
        Instantiate(_player, new Vector3(0f,0f,0f), Quaternion.identity);
        _isGameOver = false;
    }

    public void EndGame(){
       titleScreen.enabled = true;
       scoreText.text = "Score: 0";
       _isGameOver = true;
    }
}
