using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    public float rollForce = 10.0f;  // サイコロに加える力の大きさ
    public float rollTorque = 10.0f; // サイコロに加える回転力の大きさ

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // スペースキーを押すとサイコロを転がす
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RollDice();
        }
    }

    void RollDice()
    {
        // サイコロの位置と回転をリセット
        transform.position = new Vector3(0, 2, 0); // 好きな位置に調整
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        // ランダムな方向に力を加える
        Vector3 force = new Vector3(
            Random.Range(-1.0f, 1.0f),
            1.0f,
            Random.Range(-1.0f, 1.0f)
        ).normalized * rollForce;

        Vector3 torque = new Vector3(
            Random.Range(-1.0f, 1.0f),
            Random.Range(-1.0f, 1.0f),
            Random.Range(-1.0f, 1.0f)
        ).normalized * rollTorque;

        rb.AddForce(force, ForceMode.Impulse);
        rb.AddTorque(torque, ForceMode.Impulse);
    }
}
