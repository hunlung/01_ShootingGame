using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GameManager.Instance.Player;
        player.OnEnd += GameOver;
    }

    void GameOver()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }




}
