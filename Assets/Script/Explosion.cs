using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : RecycleObject
{
    Animator animator;
    float animLength = 0.3f;
    private void Awake()
    {
        animator = GetComponent<Animator>();//�ִϸ����� �ְ�
        animLength = animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; // �ִϸ��̼��� ã�� �ִϸ��̼��� ���� �Ҵ�
       
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine(LifeOver(animLength)); // �ִϸ��̼� ������ ����
    }

}
