using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : RecycleObject
{
    /// <summary>
    /// �Ѿ� �ӵ�
    /// </summary>
    public float bullet_Speed = 7.0f;



    private void Update()
    {
        transform.Translate(Time.deltaTime * bullet_Speed * Vector2.up);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Factory.Instance.GetHitEffect(transform.position);
        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }



}
