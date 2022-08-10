using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameStatus
    {
        Play,
        Pause,
        Stop,
    };
    
    public float playTime = 0;
    public int score = 0;
    public float heathPoint = 100.0f;
    public int coin = 0;

    private GameStatus _gameStatus = GameStatus.Play;

    public GameStatus Status {
        get
        {
            return _gameStatus;
        }
    }

    private void FixedUpdate()
    {
        if (heathPoint <= 0)
        {
            print("Game Over!");
            _gameStatus = GameStatus.Stop;
        }
        
        if (_gameStatus == GameStatus.Play)
        {
            playTime += Time.deltaTime;
        }
    }
}
