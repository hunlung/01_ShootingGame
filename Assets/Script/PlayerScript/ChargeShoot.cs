using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeShoot : RecycleObject
{

    protected override void OnEnable()
    {
        StopAllCoroutines();
        base.OnEnable();
        StartCoroutine(FireCoroutine());
        StartCoroutine(LifeOver(6.0f));
    }

    IEnumerator FireCoroutine()
    {
        while (true)
        {
            Factory.Instance.GetChargeBullet(transform.position);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
