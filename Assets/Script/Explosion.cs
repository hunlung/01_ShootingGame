using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : RecycleObject
{
    Animator animator;
    float animLength = 0.3f;
    private void Awake()
    {
        animator = GetComponent<Animator>();//애니메이터 넣고
        animLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; // 애니메이션을 찾고 애니메이션의 길이 할당
       
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(LifeOver(animLength)); // 애니메이션 끝나면 끄기
    }

}
