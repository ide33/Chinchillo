using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private Score score;  // Scoreスクリプトの参照
    [SerializeField] private DiceEvaluation diceEvaluation;  // DiceEvaluationスクリプトの参照
    private int currentRound = 1;  // 現在のラウンド
    private int totalRound = 5;  // 全5ラウンド制

    private void Updatte()
    {
        
    }

    void TurnChange()
    {
        // // ターンを切り替え
        // score.isPlayerTurn = !score.isPlayerTurn;

        // // プレイヤーターンに戻った場合、ラウンドを進める
        // if(score.isPlayerTurn)
        // {
        //     currentRound++;
        // }
    }

    void GameOver()
    {
        
    }
}
