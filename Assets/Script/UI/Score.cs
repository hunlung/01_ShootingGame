using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    TextMeshProUGUI score;

    /// <summary>
    /// ��ǥ�� �ϴ� ���� ����
    /// </summary>
    int goalScore = 0;

    /// <summary>
    /// ���� �������� ����
    /// </summary>
    float currentScore = 0.0f;

    /// <summary>
    /// ������ �ö󰡴� �ӵ�
    /// </summary>
    public float scoreUpSpeed = 50.0f;

    private void Awake()
    {
        score = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        // �÷��̾� ã��
        Player player = FindAnyObjectByType<Player>();
        //���� ������ �����ϱ�
        player.onScoreChange += RefreshScore;
        //��ǥ ����
        goalScore = 0;
        //���� ����
        currentScore = 0.0f;
        // ���� �ؽ�Ʈ
        score.text = "000000";
    }

    private void Update()
    {
        if (currentScore < goalScore)    // ������ �ö󰡴� ������ ��
        {
            float speed = Mathf.Max((goalScore - currentScore) * 5.0f, scoreUpSpeed);   // �ּ� scoreUpSpeed ����

            currentScore += Time.deltaTime * speed; //���� ���ھ ������ �ö󰡰� �����
            currentScore = Mathf.Min(currentScore, goalScore);
            
            int temp = (int)currentScore;
            score.text = $"{temp:d6}"; //6�ڸ� �� ���� ���
            //score.text = $"Score : {currentScore:f0}";    // �Ҽ��� ��� ���ϱ�
        }
    }

    /// <summary>
    /// ������ ������ �� ������ ��ǥ ������ �ٲٱ�
    /// </summary>
    /// <param name="newScore">���ŵ� ����</param>
    private void RefreshScore(int newScore)
    {

        goalScore = newScore;
    }
}