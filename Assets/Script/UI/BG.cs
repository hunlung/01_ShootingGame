using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float scrollingSpeed = 2.5f;


    /// <summary>
    /// 움직일 대상
    /// </summary>
    Transform[] bgSlots;

    /// <summary>
    /// 위쪽 화면 밖으로 보내는 기준
    /// </summary>
    float baseLineY;

    protected virtual void Awake()
    {
        bgSlots = new Transform[transform.childCount];  // 배열 만들고
        for (int i = 0; i < bgSlots.Length; i++)
        {
            bgSlots[i] = transform.GetChild(i);         // 배열에 자식을 하나씩 넣기
        }

        baseLineY = -25.47f; // 기준이될 Y위치 구하기

    }

    private void Update()
    {
        for (int i = 0; i < bgSlots.Length; i++)
        {
            bgSlots[i].Translate(Time.deltaTime * scrollingSpeed * -transform.up);   // 이동 대상을 계속 아래쪽으로 이동 시키기

            if (bgSlots[i].position.y < baseLineY)  // 기준선을 넘었는지 확인하고
            {
                MoveUp(i);                       // 넘었으면 위쪽 끝으로 보내기
            }
        
        }

    }


    /// <summary>
    /// 위로 이동시키기
    /// </summary>
    /// <param name="index">이동시킬 대상의 인덱스</param>
    protected virtual void MoveUp(int index)
    {
        bgSlots[index].transform.position = new Vector3(0f, 30.0f, 0f);   
    }
}
