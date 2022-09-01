using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = true;
    private UIManager _uiManager;

    [SerializeField]
    private GameObject _player;

    void Start(){
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    void Update()
    {
        if(isGameOver && Input.GetKey("space")){
            StartGame();
        }
    }

    public void StartGame(){
        Instantiate(_player, new Vector3(0f,0f,0f), Quaternion.identity);
        isGameOver = false;
        _uiManager.HideTitleScreen();
    }

    public void EndGame(){
       isGameOver = true;
       _uiManager.ShowTitleScreen();
       _uiManager.ResetScore();
    }
}
