// using UnityEngine;  // Unityの基本機能を使用するための宣言
// using UnityEngine.UI;  // UI関連の機能を使用するための宣言
// using System.Collections;
// using System.Collections.Generic;  // リストなどのコレクションを使用するための宣言
// using System.Linq;  // Linqを使用するための宣言
// using TMPro;  //TextMeshProを使用するための追加

// public class DiceController : MonoBehaviour  // DiceControllerというクラスの宣言。MonoBehaviourを継承しているので、Unityのコンポーネントとして機能する
// {
//     public List<GameObject> diceObjects; // シーンに配置しているサイコロのリスト
//     public Transform bowlTransform; // 皿のTransform
//     public Button rollButton; // サイコロを転がすボタン
//     public TextMeshProUGUI scoreText; // スコアを表示するTextMeshProのUI
//     public AudioClip rollSE; // サイコロを転がすときのサウンドエフェクト
//     public AudioClip bgm; // バックグラウンドミュージック

//     private AudioSource audioSource; // AudioSourceコンポーネント
//     private int playerScore = 0; // プレイヤーのスコア
//     private int cpuScore = 0; // CPUのスコア
//     private int currentRound = 1; // 現在のラウンド
//     private int totalRounds = 5; // 全ラウンド数
//     private bool isPlayerTurn = true; // プレイヤーのターンかどうか
//     private bool isRolling = false; // サイコロが転がっているかどうか

//     void Start()  // ゲーム開始時に実行される関数
//     {
//         // BGMを再生
//         audioSource = gameObject.AddComponent<AudioSource>();  // AudioSourceコンポーネントを追加
//         audioSource.clip = bgm; // AudioSourceにBGMを設定
//         audioSource.loop = true;  // ループ再生を有効にする
//         audioSource.Play();  // BGMを再生

//         // ボタンにクリックイベントを追加
//         rollButton.onClick.AddListener(RollDices);  // ボタンがクリックされたときにRollDices関数を呼び出す
//         scoreText.text = "Player: 0 | CPU: 0"; // 初期スコアの表示
//     }

//     void Update()  // 毎フレーム実行される関数
//     {
//         if (isRolling)  // サイコロが転がっている場合
//         {
//             CheckDiceStopped();  // サイコロが止まったかどうかをチェックする
//         }
//     }

//     void RollDices()  // サイコロを転がす関数
//     {
//         if (isRolling) return; // すでにサイコロが転がっている場合は処理しない
//         isRolling = true;

//         // サイコロを転がすサウンドエフェクトを再生
//         if (rollSE != null)
//         {
//             audioSource.PlayOneShot(rollSE);  // サウンドエフェクトを再生
//         }
//         else
//         {
//             Debug.LogWarning("Roll SE clip is not set.");  // サウンドエフェクトが設定されていない場合、警告を表示
//         }

//         foreach (GameObject dice in diceObjects)  // 全てのサイコロに対して処理を行う
//         {
//             // サイコロを皿の真上に配置
//             Vector3 spawnPosition = bowlTransform.position + new Vector3(Random.Range(-0.5f, 0.5f), 5, Random.Range(-0.5f, 0.5f));  // ランダムな位置にサイコロを配置
//             dice.transform.position = spawnPosition;  // サイコロの位置を設定

//             dice.transform.rotation = Quaternion.identity;  // サイコロの回転をリセット

//             // サイコロの速度と回転をリセット
//             Rigidbody rb = dice.GetComponent<Rigidbody>();  // サイコロのRigidbodyコンポーネントを取得
//             rb.velocity = Vector3.zero;  // 速度をリセット
//             rb.angularVelocity = Vector3.zero;  // 回転をリセット

//             // サイコロにランダムな力と回転を加える
//             Vector3 force = new Vector3(
//                 Random.Range(-0.5f, 0.5f),
//                 0,
//                 Random.Range(-0.5f, 0.5f)
//             ).normalized * 5.0f; // 力の大きさを小さく調整
//             Vector3 torque = new Vector3(
//                 Random.Range(-0.5f, 0.5f),
//                 Random.Range(-0.5f, 0.5f),
//                 Random.Range(-0.5f, 0.5f)
//             ).normalized * 5.0f; // 回転の大きさを小さく調整

//             rb.AddForce(force, ForceMode.Impulse);  // サイコロに力を加える
//             rb.AddTorque(torque, ForceMode.Impulse);  // サイコロに回転を加える
//         }
//     }

//     void CheckDiceStopped()  // サイコロが止まったかどうかをチェックする関数
//     {
//         // 全てのサイコロが停止しているかチェック
//         if (diceObjects.All(d => d.GetComponent<Rigidbody>().velocity.magnitude < 0.1f && d.GetComponent<Rigidbody>().angularVelocity.magnitude < 0.1f))
//         {
//             isRolling = false;  // サイコロが止まったことを示す
//             CalculateScore();  // スコアを計算する
//         }
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

//         // 次のターンへ
//         isPlayerTurn = !isPlayerTurn;
//         if (isPlayerTurn)
//         {
//             currentRound++;
//         }

//         if (currentRound <= totalRounds)
//         {
//             if (!isPlayerTurn)
//             {
//                 StartCoroutine(CPURoll());
//             }
//         }
//         else
//         {
//             DetermineWinner();
//         }
//     }

//     IEnumerator CPURoll()  //コルーチンを定義
//     {
//         yield return new WaitForSeconds(1.5f); // CPUがサイコロを振るまでの待ち時間
//         RollDices();  //サイコロを振る関数を呼び出す
//     }

//     void DetermineWinner()  //勝者を決定する関数
//     {
//         string winner = playerScore > cpuScore ? "Player" : "CPU";  //プレイヤーとCPUのスコアを比較し、スコアが高い方を勝者として設定
//         if (playerScore == cpuScore)  //プレイヤーとCPUのスコアが同じ場合、以下の処理を行う
//         {
//             winner = "Draw";   //引き分け
//         }
//         scoreText.text = "Game Over! Winner: " + winner + "\nPlayer: " + playerScore + " | CPU: " + cpuScore;  //ゲーム終了のメッセージと最終スコアを表示
//     }

//     int GetDiceTopFace(GameObject dice)  // サイコロの上面の出目を取得する関数
//     {
//         Rigidbody rb = dice.GetComponent<Rigidbody>();  // サイコロのRigidbodyコンポーネントを取得

//         if (rb == null || rb.transform.position.y < bowlTransform.position.y - 2)  // サイコロが皿の外に出た場合
//         {
//             return -1;
//         }

//         float maxDot = -1.0f;  // 最大のドット積
//         int topFace = -1;  // 上面の出目
//         Vector3[] faceDirections = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };  // サイコロの各面の方向
//         int[] faceValues = { 1, 6, 4, 3, 2, 5 };  // 各面の値

//         for (int i = 0; i < faceDirections.Length; i++)  // 各面に対して処理を行う
//         {
//             float dot = Vector3.Dot(dice.transform.up, faceDirections[i]);  // ドット積を計算
//             if (dot > maxDot) // 最大のドット積を更新
//                 if (dot > maxDot)
//                 {
//                     maxDot = dot;
//                     topFace = faceValues[i];
//                 }
//         }

//         return topFace;  // 上面の出目を返す
//     }
// }
