using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;  //TextMeshProを使用するための追加

public class DiceController : MonoBehaviour
{
    public List<GameObject> diceObjects; // シーンに配置しているサイコロのリスト
    public Transform bowlTransform; // 皿のTransform
    public Button rollButton; // サイコロを転がすボタン
    public TextMeshProUGUI scoreText; // スコアを表示するTextMeshProのUI

    private int score; // 現在のスコア
    private bool isRolling = false; // サイコロが転がっているかどうか

    void Start()
    {
        // ボタンにクリックイベントを追加
        rollButton.onClick.AddListener(RollDices);
        scoreText.text = "Score: 0"; // 初期スコアの表示
    }

    void Update()
    {
        if (isRolling)
        {
            CheckDiceStopped();
        }
    }

    void RollDices()
    {
        isRolling = true;
        score = 0;

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
                Random.Range(-0.5f, 0.5f),
                0,
                Random.Range(-0.5f, 0.5f)
            ).normalized * 5.0f; // 力の大きさを小さく調整
            Vector3 torque = new Vector3(
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f),
                Random.Range(-0.5f, 0.5f)
            ).normalized * 5.0f; // 回転の大きさを小さく調整

            rb.AddForce(force, ForceMode.Impulse);
            rb.AddTorque(torque, ForceMode.Impulse);
        }
    }

    void CheckDiceStopped()
    {
        if (diceObjects.All(d => d.GetComponent<Rigidbody>().velocity.magnitude < 0.1f && d.GetComponent<Rigidbody>().angularVelocity.magnitude < 0.1f))
        {
            isRolling = false;
            CalculateScore();
        }
    }

    void CalculateScore()
    {
        List<int> diceResults = new List<int>();

        foreach (GameObject dice in diceObjects)
        {
            diceResults.Add(Random.Range(1, 7));  // ランダムにサイコロの出目を生成
        }

        if (diceResults.Contains(-1))
        {
            score -= 20; // サイコロが皿の外に出た場合
        }
        else
        {
            var groupedResults = diceResults.GroupBy(x => x).ToList();

            if (groupedResults.Count == 1)
            {
                if (groupedResults[0].Key == 1)
                {
                    score += 50; // ピンゾロ
                }
                else
                {
                    score += 30; // ゾロ目
                }
            }
            else if (diceResults.Contains(4) && diceResults.Contains(5) && diceResults.Contains(6))
            {
                score += 20; // シゴロ
            }
            else if (groupedResults.Any(g => g.Count() == 2))
            {
                score += 10; // 通常
            }
            else if (diceResults.Contains(1) && diceResults.Contains(2) && diceResults.Contains(3))
            {
                score -= 40; // ヒフミ
            }
            else
            {
                score -= 10; // 役なし
            }
        }

        scoreText.text = "Score: " + score;
    }

    int GetDiceTopFace(GameObject dice)
    {
        Rigidbody rb = dice.GetComponent<Rigidbody>();

        if (rb == null || rb.transform.position.y < bowlTransform.position.y - 2)
        {
            return -1; // サイコロが皿の外に出た場合
        }

        float maxDot = -1.0f;
        int topFace = -1;
        Vector3[] faceDirections = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };
        int[] faceValues = { 1, 6, 4, 3, 2, 5 };

        for (int i = 0; i < faceDirections.Length; i++)
        {
            float dot = Vector3.Dot(dice.transform.up, faceDirections[i]);
            if (dot > maxDot)
            {
                maxDot = dot;
                topFace = faceValues[i];
            }
        }

        return topFace;
    }
}
