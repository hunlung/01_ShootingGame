using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerChargeShoot : RecycleObject
{
    //4�ʵ��� ��� setactivefalse�Ǵ� ��ũ��Ʈ <<�������� �ڷ�ƾ���� �ٲٱ�
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
