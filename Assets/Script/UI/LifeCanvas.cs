using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCanvas : MonoBehaviour
{
    Image[] lifeImages;

    private void Awake()
    {
        lifeImages = new Image[transform.childCount]; //이미지 수 카운트
        for (int i = 0; i < transform.childCount; i++) // for로 집어넣기
        {
            Transform child = transform.GetChild(i);
            lifeImages[i] = child.GetComponent<Image>();

        }
    }

    private void Start()
    {
        Player player = GameManager.Instance.Player;
        if (player != null)
        {
            player.onDie += OnLifeChange;
        }
    }
    private void OnLifeChange(int life)
    {
        for (int i = 0; i < life; i++)
        {
            lifeImages[i].color = Color.white;

        }
        if (life >= 1)
        {

            for (int i = life - 1; i < lifeImages.Length; i++)
            {
                lifeImages[i].color = new Color(1, 1, 1, 0);
            }
        }
    }


}
