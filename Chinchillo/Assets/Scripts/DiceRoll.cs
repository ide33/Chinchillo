using System.Collections;  // コルーチン（非同期処理）を使用するための名前空間
using System.Collections.Generic;  // リストなどのコレクションを使用するための名前空間
using UnityEngine;  // Unityの基本機能を使用するための名前空間
using DG.Tweening; // DoTweenの名前空間
using UnityEngine.UI;  // UI関連の機能を使用するための名前空間
using TMPro; // UIを使用する場合

public class DiceRoll : MonoBehaviour  // DiceRollというクラスの宣言。MonoBehaviourを継承しているので、Unityのコンポーネントとして機能する
{
    public float rollDuration = 1.5f; // 転がる時間
    public Vector3 rollRotation = new Vector3(360, 360, 360); // 転がる回転量
    public TextMeshProUGUI scoreText; // スコアを表示するためのUI Text

    private bool isStopped = false;  // サイコロが止まっているかどうかを示すフラグ

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();  // サイコロのRigidbodyコンポーネントを取得
        if (rb != null)
        {
            rb.isKinematic = true; // 物理シミュレーションを無効化
        }

        // サイコロの初期位置と回転をリセット
        transform.position = Vector3.zero;  // サイコロの位置を原点に設定
        transform.rotation = Quaternion.identity;  // サイコロの回転をリセット

        RollDice();  // サイコロを転がす関数を呼び出す
    }

    public void RollDice()  // サイコロを転がす関数
    {
        // サイコロの回転をアニメーションする
        transform.DORotate(rollRotation, rollDuration, RotateMode.LocalAxisAdd)
            .OnComplete(() =>  // アニメーションが完了したときに実行する処理
            {
                isStopped = true;  // サイコロが止まったことを示す
                DetermineTopFace();  // サイコロの上面の出目を判定する関数を呼び出す
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

    void DetermineTopFace()  // サイコロの上面の出目を判定する関数
    {
        Vector3 up = transform.up;  // サイコロの上方向のベクトルを取得
        float maxDot = float.MinValue;  // 最大のドット積の初期値を設定
        int topFaceIndex = -1;  // 上面のインデックスを初期化

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
            float dot = Vector3.Dot(up, faces[i]);  // 各面の方向とサイコロの上方向とのドット積を計算
            if (dot > maxDot)  // 最大のドット積を更新
            {
                maxDot = dot;
                topFaceIndex = i;
            }
        }

        // 出目を表示
        DisplayScore(topFaceIndex);  // スコアを表示する関数を呼び出す
    }

    void DisplayScore(int faceIndex)  // スコアを表示する関数
    {
        // スコアを表示する処理（例として面のインデックスを表示）
        if (scoreText != null)
        {
            scoreText.text = "Score: " + (faceIndex + 1);  // スコアを更新
        }
    }
}
