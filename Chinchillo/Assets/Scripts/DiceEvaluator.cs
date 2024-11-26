using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DiceEvaluator : MonoBehaviour
{
    // サイコロの各面の方向を表すベクトル（上下左右前後）
    private static readonly Vector3[] FaceDirections = {
        Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back
    };
    // 各面に対応するサイコロの出目
    private static readonly int[] FaceValues = { 1, 6, 4, 3, 2, 5 };

    public int EvaluateTopFace(GameObject dice, Vector3 bowlPosition)
    {
        // サイコロにアタッチされた Rigidbody コンポーネントを取得
        Rigidbody rb = dice.GetComponent<Rigidbody>();
        if (rb == null || dice.transform.position.y < bowlPosition.y - 2)
        {
            return -1; // Rigidbody がないか、皿から外れている場合は -1 を返す
        }

        // 出目を格納するリスト
        List<int> rollResults = new List<int>();

        // サイコロの各面の方向をチェック
        for (int i = 0; i < FaceDirections.Length; i++)
        {
            // 面の方向と現在の上向きベクトルとの内積を計算
            float dot = Vector3.Dot(dice.transform.up, FaceDirections[i]);
            if (dot > 0) // 上向きの面を判定
            {
                rollResults.Add(FaceValues[i]); // 上向きの面の出目をリストに追加
            }
        }

        // スコア計算
        return CalculateScore(rollResults);
    }

    // スコアを計算するメソッド
    private int CalculateScore(List<int> rollResults)
    {
        int roundScore = 0;

        // 皿から外れた場合
        if (rollResults.Count == 0)
        {
            return -20; // 皿から外れたら-20点
        }

        // 出目が全部1なら50点
        if (rollResults.Count == 1 && rollResults[0] == 1)
        {
            roundScore += 50;
        }
        // 1以外のゾロ目なら30点
        else if (rollResults.Distinct().Count() == 1 && rollResults[0] != 1)
        {
            roundScore += 30;
        }
        // 4, 5, 6なら20点
        else if (rollResults.All(r => r == 4 || r == 5 || r == 6))
        {
            roundScore += 20;
        }
        // 同じ出目が2つあれば10点
        else if (rollResults.GroupBy(r => r).Any(g => g.Count() == 2))
        {
            roundScore += 10;
        }
        // 出目が1, 2, 3なら-40点
        else if (rollResults.Contains(1) && rollResults.Contains(2) && rollResults.Contains(3))
        {
            roundScore -= 40;
        }
        // それ以外の出目なら-10点
        else
        {
            roundScore -= 10;
        }

        return roundScore;
    }
}
