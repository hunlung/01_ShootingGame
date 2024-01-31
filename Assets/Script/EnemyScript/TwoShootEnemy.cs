using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoShootEnemy : Enemy
{

    bool IsUp = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        
        while (true)
        {

            yield return new WaitForSeconds(2.0f);
            Factory.Instance.GetEBullet(transform.GetChild(0).position);
            Factory.Instance.GetEBullet(transform.GetChild(1).position);


        }
    }


    protected override void OnMoveUpdate(float deltaTime)
    {
        if (IsUp == false)
        {
            base.OnMoveUpdate(deltaTime);
            if (transform.position.y < 1)
            {
                IsUp = true;
            }
        }
        if (IsUp == true)
        {
            transform.Translate(deltaTime * Speed * 0.3f * transform.up, Space.World); // 기본동작
            if (transform.position.y > 4)
            {
                IsUp = false;
            }
        }
    }
}
