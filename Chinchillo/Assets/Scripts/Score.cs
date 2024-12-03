using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  //TextMeshProを使用するための追加


public class Score : MonoBehaviour
{
    [SerializeField] private DiceEvaluation diceEvaluation;  //DiceEvaluationの参照
    private int playerScore = 0;  //プレイヤースコアの初期値
    private int cpuScore = 0;  //CPUスコアの初期値

    public TextMeshProUGUI scoreText; // スコアを表示するTextMeshProのUI

    void Start()
    {
        scoreText.text = "Player: 0 | CPU: 0"; // 初期スコアの表示
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // テスト用：スペースキーでスコア計算
        {
            CalculateScore();
        }
    }

    void CalculateScore()
    {
        
    }
}
