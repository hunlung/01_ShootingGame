using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    TextMeshProUGUI score;

    /// <summary>
    /// 목표로 하는 최종 점수
    /// </summary>
    int goalScore = 0;

    /// <summary>
    /// 현재 보여지는 점수
    /// </summary>
    float currentScore = 0.0f;

    /// <summary>
    /// 점수가 올라가는 속도
    /// </summary>
    public float scoreUpSpeed = 50.0f;

    private void Awake()
    {
        score = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // 플레이어 찾기
        Player player = FindAnyObjectByType<Player>();
        //점수 변동시 갱신하기
        player.onScoreChange += RefreshScore;
        //목표 점수
        goalScore = 0;
        //현재 점수
        currentScore = 0.0f;
        // 점수 텍스트
        score.text = "000000";
    }

    private void Update()
    {
        if (currentScore < goalScore)    // 점수가 올라가는 도중일 때
        {
            float speed = Mathf.Max((goalScore - currentScore) * 5.0f, scoreUpSpeed);   // 최소 scoreUpSpeed 보장

            currentScore += Time.deltaTime * speed; //현재 스코어가 느리게 올라가게 만들기
            currentScore = Mathf.Min(currentScore, goalScore);
            
            int temp = (int)currentScore;
            score.text = $"{temp:d6}"; //6자리 수 까지 출력
            //score.text = $"Score : {currentScore:f0}";    // 소수점 출력 안하기
        }
    }

    /// <summary>
    /// 점수가 들어오면 새 점수를 목표 점수로 바꾸기
    /// </summary>
    /// <param name="newScore">갱신된 점수</param>
    private void RefreshScore(int newScore)
    {

        goalScore = newScore;
    }
}