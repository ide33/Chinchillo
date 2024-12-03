using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceEvaluation : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;  // サイコロのRigidbody

    [SerializeField] private List<GameObject> diceObjects; // シーンに配置しているサイコロのリスト
    public int DiceNumber { get; private set; }  // 出目を公開するプロパティ

    // コライダーがトリガーゾーンに留まっている間に呼び出される
    private void OnTriggerStay(Collider col)
    {
        if (rb.IsSleeping())  // サイコロが完全に静止したとき
        {
            switch (col.gameObject.name)  // 接触しているオブジェクトを判定
            {
                case "1":
                    DiceNumber = 6;
                    break;
                case "2":
                    DiceNumber = 5;
                    break;
                case "3":
                    DiceNumber = 4;
                    break;
                case "4":
                    DiceNumber = 3;
                    break;
                case "5":
                    DiceNumber = 2;
                    break;
                case "6":
                    DiceNumber = 1;
                    break;
            }
            Debug.Log($"出目: {DiceNumber}"); // 確認用
        }
    }
}
