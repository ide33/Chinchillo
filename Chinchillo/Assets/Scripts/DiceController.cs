using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceController : MonoBehaviour
{
   public List<GameObject> diceObjects; // シーンに配置しているサイコロのリスト
    public Transform bowlTransform; // 皿のTransform
    public Button rollButton; // サイコロを転がすボタン

    void Start()
    {
        // ボタンにクリックイベントを追加
        rollButton.onClick.AddListener(RollDices);
    }

    void RollDices()
    {
        foreach (GameObject dice in diceObjects)
        {
            // サイコロを皿の真上に配置
            Vector3 spawnPosition = bowlTransform.position + new Vector3(Random.Range(-0.5f, 0.5f), 5, Random.Range(-0.5f, 0.5f));
            dice.transform.position = spawnPosition;
            dice.transform.rotation = Quaternion.identity;

            // サイコロの速度と回転をリセット
            Rigidbody rb = dice.GetComponent<Rigidbody>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            // サイコロにランダムな力と回転を加える
            Vector3 force = new Vector3(
                Random.Range(-1.0f, 1.0f),
                0,
                Random.Range(-1.0f, 1.0f)
            ).normalized * 10.0f; // 力の大きさを調整
            Vector3 torque = new Vector3(
                Random.Range(-1.0f, 1.0f),
                Random.Range(-1.0f, 1.0f),
                Random.Range(-1.0f, 1.0f)
            ).normalized * 10.0f; // 回転の大きさを調整

            rb.AddForce(force, ForceMode.Impulse);
            rb.AddTorque(torque, ForceMode.Impulse);
        }
    }
}
