using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : RecycleObject
{
    

    /// <summary>
    /// 적의 속도
    /// </summary>
    public float Speed =3.0f;
    /// <summary>
    /// HP
    /// </summary>
    private int hp = 3;
    
    /// <summary>
    /// 적 체력 파라메터
    /// </summary>
    private int HP
    {
        get => hp;
        set
        {
            hp = value;
            if(hp <=0)
            {
                hp = 0;
                OnDie();
            }
        }
    }

    Vector2 EnenmytVec = Vector2.zero;

    /// <summary>
    /// 적의 (최대)체력
    /// </summary>
    public int maxHP = 3;
    /// <summary>
    /// 적이 주는 점수
    /// </summary>
    public int score = 100;

    Action onDie;

     protected Player player;


    protected override void OnEnable()
    {
        base.OnEnable();
        OnInitialize();
    }

    protected override void OnDisable()
    {
        if(player != null)// 죽으면 순차적으로 종료하기
        {
            onDie -= PlayerAddScore;
            onDie = null;
            player = null;
        }
        base.OnDisable();
    }
    private void Update()
    {
        OnMoveUpdate(Time.deltaTime);
    }

    /// <summary>
    /// 업데이트 중 이동 처리하는 함수
    /// </summary>
    /// <param name="deltaTime">프레임간의 간격</param>
    protected virtual void OnMoveUpdate(float deltaTime)
    {
        transform.Translate(deltaTime * Speed * -transform.up, Space.World); // 기본동작
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            HP--;
        }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boom"))
        {
            HP -= 5;
        }
    }

    protected virtual void OnInitialize()
    {
        if(player == null)
        {
            player = GameManager.Instance.Player;
        }
        if(player != null)
        {
            onDie += PlayerAddScore;
        }

        HP = maxHP; //체력 초기화
    }

    protected virtual void OnDie()//죽을때 폭발하면서 죽음알리고 끄기
    {
        Factory.Instance.GetExplosionEffect(transform.position);
        onDie?.Invoke();
        gameObject.SetActive(false);
    }

    

    void PlayerAddScore()
    {
        player.AddScore(score);
    }

}
