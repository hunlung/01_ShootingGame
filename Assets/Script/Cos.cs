using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class cosEnemy : MonoBehaviour
{

    /// <summary>
    /// 이동 속도
    /// </summary>
    public float speed = 1.0f;
    /// <summary>
    /// 회전 정도
    /// </summary>
    public float amplitude = 3.0f;
    /// <summary>
    /// 회전하는 속도
    /// </summary>
    public float frequency = 2.0f;
    /// <summary>
    /// 
    /// </summary>
    float spawnX = 0.0f;
    /// <summary>
    /// 시간 측정(frequency에 영향받음)
    /// </summary>
    float elapsedTime = 0.0f;










    private void Start()
    {
        spawnX = transform.position.x; // 소환위치 = 이 물체의 위치
        elapsedTime = 0.0f; // 시간 초기화
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime * frequency; // 회전 각도
        transform.position = new Vector3(spawnX + Mathf.Sin(elapsedTime) * amplitude,//회전시키기
            transform.position.y-Time.deltaTime*speed, // 내려오는 속도
            0);
    }


}
