using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // DoTweenの名前空間
using UnityEngine.UI;
using TMPro; // UIを使用する場合

public class DiceRoll : MonoBehaviour
{
     public float rollDuration = 1.5f; // 転がる時間
    public Vector3 rollRotation = new Vector3(360, 0, 0); // 転がる回転量
    public TextMeshProUGUI scoreText; // スコアを表示するためのUI Text

    private bool isStopped = false;

    void Start()
    {
        RollDice();
    }

    public void RollDice()
    {
        // サイコロの回転をアニメーションする
        transform.DORotate(rollRotation, rollDuration, RotateMode.LocalAxisAdd)
            .OnComplete(() =>
            {
                isStopped = true;
                DetermineTopFace();
            });
    }

    void Update()
    {
        // サイコロが完全に止まったかどうかをチェックする
        if (isStopped && Mathf.Approximately(Vector3.Dot(transform.up, Vector3.up), 1.0f))
        {
            isStopped = false; // ストップ状態をリセット
        }
    }

    void DetermineTopFace()
    {
        // サイコロの上面の出目を判定する処理
        Vector3 up = transform.up;
        float maxDot = float.MinValue;
        int topFaceIndex = -1;

        // サイコロの面を定義
        Vector3[] faces = new Vector3[]
        {
            Vector3.up, // 上面
            Vector3.down, // 下面
            Vector3.left, // 左面
            Vector3.right, // 右面
            Vector3.forward, // 前面
            Vector3.back // 後面
        };

        // 最も近い面を判定
        for (int i = 0; i < faces.Length; i++)
        {
            float dot = Vector3.Dot(up, faces[i]);
            if (dot > maxDot)
            {
                maxDot = dot;
                topFaceIndex = i;
            }
        }

        // 出目を表示
        DisplayScore(topFaceIndex);
    }

    void DisplayScore(int faceIndex)
    {
        // スコアを表示する処理（例として面のインデックスを表示）
        if (scoreText != null)
        {
            scoreText.text = "Score: " + (faceIndex + 1);
        }
    }
}
