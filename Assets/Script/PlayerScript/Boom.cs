using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : RecycleObject
{
    public float boomTimer = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void OnEnable()
    {
        StopAllCoroutines();

        base.OnEnable();

        StartCoroutine(BoomCoroutine());

    }
    IEnumerator BoomCoroutine()
    {
        float deltatime = 0;
        while (deltatime< boomTimer)
        {
        deltatime += Time.deltaTime;

            yield return null;
        }
        gameObject.SetActive(false);

    }
}
