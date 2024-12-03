// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class DiceRoller : MonoBehaviour
// {
//     private const float SpawnHeight = 5.0f; // サイコロの初期位置の高さ
//     private const float ForceMagnitude = 5.0f; // サイコロに加える力の大きさ

//     public void RollDice(GameObject dice, Vector3 bowlPosition)
//     {
//         // サイコロにアタッチされた Rigidbody コンポーネントを取得
//         Rigidbody rb = dice.GetComponent<Rigidbody>();
//         if (rb == null) return; // Rigidbody がない場合は何もしない

//         // サイコロの初期位置をランダムに設定
//         Vector3 spawnPosition = bowlPosition + new Vector3(
//             Random.Range(-0.5f, 0.5f), // 水平方向にランダムな位置
//             SpawnHeight, // 指定した高さ
//             Random.Range(-0.5f, 0.5f)  // 水平方向にランダムな位置
//         );

//         dice.transform.position = spawnPosition; // サイコロの位置を設定
//         dice.transform.rotation = Random.rotation; // サイコロの回転をランダムに設定

//         // サイコロの速度と回転速度をリセット
//         rb.velocity = Vector3.zero;
//         rb.angularVelocity = Vector3.zero;

//         // サイコロにランダムな力を加える
//         Vector3 randomForce = new Vector3(
//             Random.Range(-0.5f, 0.5f), // x方向のランダムな力
//             0, // y方向の力（0）
//             Random.Range(-0.5f, 0.5f)  // z方向のランダムな力
//         ).normalized * ForceMagnitude; // 力の大きさを設定

//         // サイコロにランダムな回転を加える
//         Vector3 randomTorque = new Vector3(
//             Random.Range(-0.5f, 0.5f), // x方向のランダムな回転
//             Random.Range(-0.5f, 0.5f), // y方向のランダムな回転
//             Random.Range(-0.5f, 0.5f)  // z方向のランダムな回転
//         ).normalized * ForceMagnitude; // 回転の大きさを設定

//         rb.AddForce(randomForce, ForceMode.Impulse); // サイコロに力を加える
//         rb.AddTorque(randomTorque, ForceMode.Impulse); // サイコロに回転を加える
//     }
// }
