using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject bulletprefab;

    public float Interval = 2.0f;

    private void OnBecameVisible()
    {
        StartCoroutine(BulletCoroutine());
    }
    private void OnBecameInvisible()
    {
        StopAllCoroutines();
    }

    IEnumerator BulletCoroutine()
    {
        float deltatime = 0;
        deltatime += Time.deltaTime;
        if(deltatime > Interval)
        {
            Instantiate(bulletprefab,transform.position, Quaternion.identity);
            yield return null;
        }

        }
    }


