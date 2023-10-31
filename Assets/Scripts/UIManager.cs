using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public TMP_Text scoreText;
    public TMP_Text TimeText;
    public TMP_Text enemyCountText;


    private void Start()
    {
        UpdateScore(0);
        UpdateTime(0);
        EnemyCount(0);
    }
    public void UpdateScore(int _score)
    {
        scoreText.text = "score: " + _score;
    }

    public void UpdateTime(float _time)
    {
        TimeText.text = _time.ToString("F2");
    }

    public void EnemyCount(int _count)
    {
        enemyCountText.text = "Enemy Count: " + _count;
    }
}
