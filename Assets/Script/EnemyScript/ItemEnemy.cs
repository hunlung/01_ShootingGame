using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEnemy : Enemy
{


    protected override void OnInitialize()
    {
        base.OnInitialize();
    }


    protected override void OnDie()
    {
        player = GameManager.Instance.Player;

        if (player.power< 4)
        {
        Factory.Instance.GetPowerItem(transform.position);
        }
        else
        {
        Factory.Instance.GetBoomItem(transform.position);
        }

        base.OnDie();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 체력0, 충돌이 총알이면 파워생성하기
    }










}

