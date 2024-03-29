using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    /// <summary>
    /// 재활용 오브젝트가 비활성화 될 때 실행되는 델리게이트
    /// </summary>
    public Action onDisable;
    protected virtual void OnEnable()
    {
        transform.localPosition = Vector3.zero; // 부모의 위치
        transform.localRotation = Quaternion.identity; // 부모의 회전과 같게
        StopAllCoroutines(); // 라이프타임 초기화
    }
    protected virtual void OnDisable()
    {
        onDisable?.Invoke(); //비활성화 알림(풀만들때 할 일이추가되야함)
    }
    /// <summary>
    /// 일정시간 후에 오브젝트 비활성화 시키는 코루틴
    /// </summary>
    /// <param name="delay">비활성화에 걸리는 시간</param>
    /// <returns></returns>
    protected IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay); //기다리고
        gameObject.SetActive(false); // 비활성화
    }
}
