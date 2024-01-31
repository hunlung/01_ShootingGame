using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBullet : Bullet
{

    private Transform target; // �� ��ġ
    bool onGuided = false;


    protected override void OnEnable()
    {
        base.OnEnable();
        onGuided = false;
        StartCoroutine(EnemychaserCoroutine());
    }

    private IEnumerator EnemychaserCoroutine()
    {
        while (true)
        {


            if (onGuided == true)
            {
                target = GameObject.FindGameObjectWithTag("Enemy").transform;
                Vector3 dir = target.position - transform.position;
                transform.up = new Vector3(dir.x, dir.y, 0);

            }

            yield return null;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onGuided == false && collision.CompareTag("Enemy"))
        {
            onGuided = true;
        }
    }
}
