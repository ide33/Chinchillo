using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roll : MonoBehaviour
{
    private Rigidbody rb;  //Rigidbodyコンポーネント
    void Start()
    {
        rb = GetComponent<Rigidbody>();  //Rigidbodyコンポーネントを取得
        Vector3 rota = new Vector3(Random.Range(0, 100), Random.Range(0, 100), Random.Range(0, 100));  //ランダムな回転量を設定
        transform.Rotate(rota);  //サイコロのtranformを操作して、rotaに基づいた回転を適用
    }

    void Update()
    {

    }
}
