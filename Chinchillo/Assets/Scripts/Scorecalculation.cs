// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;  // Linqを使用するための宣言
// using TMPro;  //TextMeshProを使用するための追加

// public class Scorecalculation : MonoBehaviour
// {
//     public List<GameObject> diceObjects; // シーンに配置しているサイコロのリスト
//     public TextMeshProUGUI scoreText; // スコアを表示するTextMeshProのUI

//     private int playerScore = 0; // プレイヤーのスコア
//     private int cpuScore = 0; // CPUのスコア
//     private int currentRound = 1; // 現在のラウンド
//     private int totalRounds = 5; // 全ラウンド数
//     private bool isPlayerTurn = true; // プレイヤーのターンかどうか

//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }

//     void CalculateScore()  // スコアを計算する関数
//     {
//         int roundScore = 0; // 今回のラウンドのスコア
//         List<int> diceResults = new List<int>();  // サイコロの出目のリスト

//         foreach (GameObject dice in diceObjects)  // 全てのサイコロに対して処理を行う
//         {
//             diceResults.Add(Random.Range(1, 7));  // ランダムにサイコロの出目を生成
//         }

//         if (diceResults.Contains(-1))  // サイコロが皿の外に出た場合
//         {
//             // スコアを減らす
//             roundScore -= 20; // サイコロが皿の外に出た場合
//         }
//         else
//         {
//             var groupedResults = diceResults.GroupBy(x => x).ToList();  // 出目をグループ化

//             if (groupedResults.Count == 1)  // 全てのサイコロが同じ出目の場合
//             {
//                 if (groupedResults[0].Key == 1)
//                 {
//                     roundScore += 50; // ピンゾロ
//                 }
//                 else
//                 {
//                     roundScore += 30; // ゾロ目
//                 }
//             }
//             else if (diceResults.Contains(4) && diceResults.Contains(5) && diceResults.Contains(6))
//             {
//                 roundScore += 20; // シゴロ
//             }
//             else if (groupedResults.Any(g => g.Count() == 2))
//             {
//                 roundScore += 10; // 通常
//             }
//             else if (diceResults.Contains(1) && diceResults.Contains(2) && diceResults.Contains(3))
//             {
//                 roundScore -= 40; // ヒフミ
//             }
//             else
//             {
//                 roundScore -= 10; // 役なし
//             }
//         }

//         if (isPlayerTurn)  //プレイヤーのターンかどうか
//         {
//             //プレイヤーのターンの場合
//             playerScore += roundScore;  //プレイヤーのスコアに今回のラウンドのスコアを加算
//         }
//         else  //CPUのターンの場合
//         {
//             cpuScore += roundScore;  //CPUのスコアに今回のラウンドのスコアを加算
//         }
//         scoreText.text = "Player: " + playerScore + " | CPU: " + cpuScore;  //現在のプレイヤーとCPUのスコアを表示するテキストを更新

        // // 次のターンへ
        // isPlayerTurn = !isPlayerTurn;
        // if (isPlayerTurn)
        // {
        //     currentRound++;
        // }

        // if (currentRound <= totalRounds)
        // {
        //     if (!isPlayerTurn)
        //     {
        //         StartCoroutine(CPURoll());
        //     }
        // }
//         else
//         {
//             DetermineWinner();
//         }
//     }
// }
