using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private DiceRoller diceRoller; // サイコロを転がす処理
    [SerializeField] private DiceEvaluator diceEvaluator; // サイコロの上面を判定する処理
    [SerializeField] private TurnManager turnManager; // ターン管理
    [SerializeField] private ScoreManager scoreManager; // スコア管理
    [SerializeField] private GameObject[] diceObjects; // 使用するサイコロ
    [SerializeField] private Transform bowlTransform; // 皿の位置
    [SerializeField] private int totalRounds = 5; // ゲーム全体のラウンド数

    private int currentRound = 1; // 現在のラウンド
    private bool isRolling = false; // サイコロを振っているかどうか

    public void RollDices()
    {
        if (isRolling) return; // サイコロが振られている最中は実行しない

        isRolling = true; // サイコロを振る状態に変更
        foreach (GameObject dice in diceObjects)
        {
            diceRoller.RollDice(dice, bowlTransform.position); // 各サイコロを振る
        }

        Invoke(nameof(EvaluateRoll), 2.0f); // 2秒後にサイコロの出目を評価
    }

    private void EvaluateRoll()
    {
        int roundScore = 0; // このラウンドのスコアを初期化

    foreach (GameObject dice in diceObjects)
    {
        int result = diceEvaluator.EvaluateTopFace(dice, bowlTransform.position);

        // スコアの加算
        roundScore += (result == -1) ? -20 : result; // ペナルティ適用
    }

    // スコアを更新
    scoreManager.AddScore(turnManager.IsPlayerTurn, roundScore);

    // ゲーム進行
    if (currentRound >= totalRounds && turnManager.IsPlayerTurn)
    {
        EndGame(); // ゲーム終了処理
    }
    else
    {
        if (!turnManager.IsPlayerTurn)
        {
            currentRound++; // CPUターン後にラウンドを進める
        }

        turnManager.NextTurn(); // ターン交代
        isRolling = false; // サイコロ振る状態を解除
    }
    }

    private void EndGame()
    {
        string winner = scoreManager.GetWinner(); // 勝者を判定
        scoreManager.scoreText.text += $"\nGame Over! Winner: {winner}"; // 結果を表示
    }
}
