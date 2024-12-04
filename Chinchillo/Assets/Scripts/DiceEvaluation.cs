using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceEvaluation : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> diceRigidbodies;  // サイコロのRigidbodyリスト

    private Dictionary<GameObject, int> diceNumbers = new Dictionary<GameObject, int>();  // 各サイコロの出目を管理するリスト

    public List<int> DiceNumbers
    {
        get
        {
            return new List<int>(diceNumbers.Values);  // 出目リストを公開
        }
    }

    void Start()
    {
        foreach (var dice in diceRigidbodies)
        {
            diceNumbers[dice.gameObject] = 0;  // 全てのサイコロの出目を初期化
        }
    }


    // コライダーがトリガーゾーンに留まっている間に呼び出される
    private void OnTriggerStay(Collider col)
    {
        foreach (var rb in diceRigidbodies)
        {
            if (rb.IsSleeping())  // サイコロが完全に静止したとき
            {
                switch (col.gameObject.name)  // 接触しているオブジェクトを判定
                {
                    case "1":
                        diceNumbers[rb.gameObject] = 6;
                        break;
                    case "2":
                        diceNumbers[rb.gameObject] = 5;
                        break;
                    case "3":
                        diceNumbers[rb.gameObject] = 4;
                        break;
                    case "4":
                        diceNumbers[rb.gameObject] = 3;
                        break;
                    case "5":
                        diceNumbers[rb.gameObject] = 2;
                        break;
                    case "6":
                        diceNumbers[rb.gameObject] = 1;
                        break;
                }
                Debug.Log($"サイコロ: {rb.gameObject.name},出目: {diceNumbers[rb.gameObject]}"); // 確認用
            }
        }
    }
}
