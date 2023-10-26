using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum GameState
{
    Title, Playing, Paused, GameOver
}

public enum Difficulty
{
    easy, normal, hard
}
       
public class GameManager : Singleton <GameManager>
{
    public GameState gameState;
    public Difficulty difficulty;
    public int score = 0;
    int scoreMultiplier = 1;
   


   
    void Start()
    {
        switch(difficulty)
        {
            case Difficulty.easy:
                scoreMultiplier = 1;
                break;
                case Difficulty.normal:
                scoreMultiplier = 2;
                break;
            case Difficulty.hard:
                scoreMultiplier = 3;
                break;
        }
    }

    public void AddScore(int _points)
    {
        score += _points * scoreMultiplier;
    }

    private void OnEnemyHit(GameObject _enemy)
    {
        int _score = _enemy.GetComponent<Enemy>().myScore;
        AddScore(score);
    }

    private void OnEnable()
    {
        Enemy.OnEnemyHit += OnEnemyHit;
        Enemy.OnEnemyDie += OnEnemyHit;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyHit -= OnEnemyHit;
        Enemy.OnEnemyDie -= OnEnemyHit;
    }
}
    