using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoomCanvas : MonoBehaviour
{
    Image[] boomImages;

    private void Awake()
    {
        boomImages = new Image[transform.childCount]; //이미지 수 카운트
        for (int i = 0; i < transform.childCount; i++) // for로 집어넣기
        {
            Transform child = transform.GetChild(i);
            boomImages[i] = child.GetComponent<Image>();

        }


    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        if (player != null)
        {

            player.onBoom += OnBoomChange;

        }
        OnBoomChange(2);
    }
    private void OnBoomChange(int boom)
    {
        for (int i = 0; i < boom; i++)
        {
            boomImages[i].color = Color.white;

        }


        for (int i = boom; i < boomImages.Length; i++)
        {
            boomImages[i].color = new Color(1, 1, 1, 0);
        }

    }





}
