using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool IsPlayerTurn {get; private set;} = true;

    //次のターンに移行
    public void NextTurn()
    {
        IsPlayerTurn = !IsPlayerTurn;
    }
}
