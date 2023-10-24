using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Title, Playing, Paused, GameOver
}

public enum Difficulty
{
    easy, normal, hard
}
       
public class GameManager : MonoBehaviour
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

    
}
