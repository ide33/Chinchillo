// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;  // UI関連の機能を使用するための宣言
// using System.Linq;  // Linqを使用するための宣言

// public class Roll : MonoBehaviour
// {
//     public List<GameObject> diceObjects; // シーンに配置しているサイコロのリスト
//     public Transform bowlTransform; // 皿のTransform
//     public Button rollButton; // サイコロを転がすボタン
//     private bool isRolling = false; // サイコロが転がっているかどうか

//     void Start()
//     {
//         // ボタンにクリックイベントを追加
//         rollButton.onClick.AddListener(RollDices);  // ボタンがクリックされたときにRollDices関数を呼び出す
//     }

//     void Update()
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
//         // if (rollSE != null)
//         // {
//         //     audioSource.PlayOneShot(rollSE);  // サウンドエフェクトを再生
//         // }
//         // else
//         // {
//         //     Debug.LogWarning("Roll SE clip is not set.");  // サウンドエフェクトが設定されていない場合、警告を表示
//         // }

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
// }
