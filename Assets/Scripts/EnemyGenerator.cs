using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public struct EnemySpawn
{
    public GameObject enemy;
    public int count;
    public float spawnDelay;
}

[Serializable]
public struct GameRound
{
    public GameRound(float roundDelay)
    {
        this.roundDelay = roundDelay;
        enemies = new List<EnemySpawn>();
    }
    
    public float roundDelay;
    public List<EnemySpawn> enemies;
}

public class EnemyGenerator : MonoBehaviour
{
    // Public member variables
    public GameManager gameManager;
    public Route route;
    public List<GameRound> gameRounds = new List<GameRound>();
    
    // Round
    private int _currentCount = 0;
    private int _currentRound = 0;
    private int _currentEnemy = 0;
    private float _timestampEnemy = 0;
    private float _timestampRound = 0;

    private void Awake()
    {
        route = GetComponent<Route>();
        _timestampEnemy = gameManager.playTime;
        _timestampRound = gameManager.playTime;
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (_currentRound < gameRounds.Count && 
            gameManager.playTime - _timestampRound >= gameRounds[_currentRound].roundDelay)
        {
            EnemySpawn enemy = gameRounds[_currentRound].enemies[_currentEnemy];
            
            if (gameManager.playTime - _timestampEnemy < enemy.spawnDelay) return;
            
            GameObject enemyObject = enemy.enemy;
            GameObject enemyClone = Instantiate(enemyObject, transform.position, enemyObject.transform.rotation);
            enemyClone.GetComponent<NPC>().path = route.Path;

            _timestampEnemy = gameManager.playTime;
            _currentCount ++;

            if (_currentCount >= enemy.count)
            {
                _currentCount = 0;
                _currentEnemy ++;
            }

            if (_currentEnemy >= gameRounds[_currentRound].enemies.Count)
            {
                _currentEnemy = 0;
                _currentRound ++;
                _timestampRound = gameManager.playTime;
            }
        }
    }
}
