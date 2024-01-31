using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerChargeShoot : RecycleObject
{
    //4초동안 쏘고 setactivefalse되는 스크립트 <<업뎃에서 코루틴으로 바꾸기
    public float lifetime = 8f;

    private float time = 0f;
    private void Update()
    {

        while(lifetime > time)
        {
            time += Time.deltaTime;
            Factory.Instance.GetBullet(transform.position);
            
        }
    }


}
