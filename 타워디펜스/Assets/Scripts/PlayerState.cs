using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    //이 클래스는 골드, 라이프의 데이터만 관리함. 실제 보여주는 골드, 라이프는 하이어라키 게임매니저에 플레이어스테이트 창에서 컨트롤하기

    public static int Money;
    public int startMoney = 200;

    public static int Life;
    public int startLife = 10;
    public static int round;

    void Awake()
    {
        GameManager.gameOver = false;
        GameManager.pause = false;
        
        Money = startMoney;
        Life = startLife;

        round = 0;
    }
}
