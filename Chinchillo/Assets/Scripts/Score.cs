using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  //TextMeshProを使用するための追加


public class Score : MonoBehaviour
{
    [SerializeField] private DiceEvaluation diceEvaluation;  //DiceEvaluationの参照
    private int playerScore = 0;  //プレイヤースコアの初期値
    private int cpuScore = 0;  //CPUスコアの初期値

    public bool isPlayerTurn { get; private set; } = true; // プレイヤーのターンかどうか
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
        List<int> diceNumbers = diceEvaluation.DiceNumbers;  // DiceNumberプロパティからサイコロの出目をリスト形式で取得
        int roundScore = 0;  // 今回のラウンドのスコア

        // サイコロが不足している場合
        if (diceNumbers.Count != 3)
        {
            roundScore -= 20;  // 皿から落ちたとみなして
        }

        // スコア計算ロジック
        bool allSame = diceNumbers[0] == diceNumbers[1] && diceNumbers[1] == diceNumbers[2];  // 全ての出目が同じ
        bool twoSame = diceNumbers[0] == diceNumbers[1] || diceNumbers[1] == diceNumbers[2] || diceNumbers[0] == diceNumbers[2];  // 2つの出目が同じ
        bool is123 = diceNumbers.Contains(1) && diceNumbers.Contains(2) && diceNumbers.Contains(3);  // 1、2、3の場合
        bool is456 = diceNumbers.Contains(4) && diceNumbers.Contains(5) && diceNumbers.Contains(6);  // 4、5、6の場合

        if (allSame && diceNumbers[0] == 1)
        {
            // ピンゾロ
            roundScore += 50;
        }
        if (allSame)
        {
            // ゾロ目
            roundScore += 30;
        }
        if (is456)
        {
            //シゴロ
            roundScore += 20;
        }
        if (is123)
        {
            // ヒフミ
            roundScore -= 40;
        }
        if (twoSame)
        {
            // 通常
            roundScore += 10;
        }
        else
        {
            // 役なし
            roundScore -= 10;
        }

        if (isPlayerTurn)  // プレイヤーのターンかどうか
        {
            playerScore += roundScore;  // プレイヤーのスコアに今回のスコアを加算
        }
        else  // cpuのターンの場合
        {
            cpuScore += roundScore;  // cpuのスコアに今回のスコアを加算
        }
        // 現在のプレイヤーとCPUのスコアを表示するテキストを更新
        scoreText.text = "Player: " + playerScore + " | CPU: " + cpuScore;
    }

}
