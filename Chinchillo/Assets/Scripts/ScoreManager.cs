// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class ScoreManager : MonoBehaviour
// {
//     public TextMeshProUGUI scoreText; // スコアを表示するUI
//     private int playerScore = 0; // プレイヤーのスコア
//     private int cpuScore = 0; // CPUのスコア
//     public int score;

//     public void AddScore(bool isPlayerTurn, int roundScore)
//     {
//         // 現在のターンに応じてスコアを加算
//         if (isPlayerTurn)
//         {
//             playerScore += roundScore;
//         }
//         else
//         {
//             cpuScore += roundScore;
//         }
//         UpdateScoreText(); // スコア表示を更新
//     }

//     public string GetWinner()
//     {
//         // プレイヤーとCPUのスコアを比較して勝者を判定
//         if (playerScore == cpuScore)
//         {
//             return "Draw"; // 引き分け
//         }
//         else if (playerScore > cpuScore)
//         {
//             return "Player"; // プレイヤーの勝ち
//         }
//         else
//         {
//             return "CPU"; // CPUの勝ち
//         }
//     }

//     private void UpdateScoreText()
//     {
//         // スコアをテキストに反映
//         scoreText.text = $"Player: {playerScore} | CPU: {cpuScore}";
//     }
// }
