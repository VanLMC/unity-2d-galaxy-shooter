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

    public void UpdateLives(int currentLives){
        livesImageDisplay.sprite = livesSprites[currentLives];
    }

    public void UpdateScore(){
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void ResetScore(){
        scoreText.text = "Score: 0";
    }

    public void ShowTitleScreen(){
        titleScreen.enabled = true;
    }

    public void HideTitleScreen(){
        titleScreen.enabled = false;
    }

}
