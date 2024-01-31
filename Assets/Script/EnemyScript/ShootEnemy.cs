using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : Enemy
{

    float rand;
    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(FireCoroutine());
    }

    IEnumerator FireCoroutine()
    {
        int count = 0;
        rand = Random.Range(0.5f, 2f);
        while (count < 2)
        {

            yield return new WaitForSeconds(rand);
            Factory.Instance.GetEBullet(transform.position);
            count++;
        }
    }




}
