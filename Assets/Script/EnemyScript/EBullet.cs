using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet : RecycleObject
{
    /// <summary>
    /// 총알속도
    /// </summary>
    public float Speed = 3.0f;

    /// <summary>
    /// 타겟(플레이어)
    /// </summary>
    Transform target;
    /// <summary>
    /// 플레이어 방향
    /// </summary>
    Vector3 dir;

    bool ischase = false; // 플레이어 위치 한번만찾기

    protected override void OnEnable()
    {
        base.OnEnable();
        target = GameManager.Instance.Player.transform; // 플레이어 위치 찾기
        ischase = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (ischase == false)
        {
            dir = target.position - transform.position;
            ischase = true;
        }
        transform.up = new Vector2(dir.x, dir.y);
        transform.Translate(Time.deltaTime * Speed * Vector2.up); // 플레이어 추격
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Factory.Instance.GetHitEffect(transform.position);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Factory.Instance.GetHitEffect(transform.position);
        gameObject.SetActive(false);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }















}
