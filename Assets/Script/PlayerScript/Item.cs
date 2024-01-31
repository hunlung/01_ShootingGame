using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : RecycleObject
{

    

    /// <summary>
    /// ���� �ӵ�
    /// </summary>
    public float Speed = 6.0f;
    float move_x_rate;
    float move_y_rate;
    public float lifetime = 6.0f;
    float deltatime = 0;

    private void Start()
    {

        move_x_rate = Random.Range(-1.0f, 1.0f);
        move_y_rate = Random.Range(-1.0f, 1.0f);

        while (Mathf.Abs(move_x_rate) < 0.3f)
        {
            move_x_rate = Random.Range(-1.0f, 1.0f);
        }

        while (Mathf.Abs(move_y_rate) < 0.3f)
        {
            move_y_rate = Random.Range(-1.0f, 1.0f);
        }
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        deltatime = 0;
    }

    void Update()
    {

        transform.Translate(Vector3.right * Time.deltaTime *Speed * move_x_rate, Space.World);
        transform.Translate(Vector3.up * Time.deltaTime * Speed * move_y_rate, Space.World);

        // 카메라를 벗어나지 않도록 범위 제한
        Vector3 position = Camera.main.WorldToViewportPoint(transform.position);
        
        deltatime += Time.deltaTime;
        while (deltatime <= lifetime)
        {
            if (position.x < 0f)
            {
                position.x = 0f;
                move_x_rate = Random.Range(0.3f, 1.0f);
            }
            if (position.y < 0f)
            {
                position.y = 0f;
                move_y_rate = Random.Range(0.3f, 1.0f);
            }
            if (position.x > 1f)
            {
                position.x = 1f;
                move_x_rate = Random.Range(-1.0f, -0.3f);
            }
            if (position.y > 0.8f)
            {
                position.y = 0.8f;
                move_y_rate = Random.Range(-1.0f, -0.3f);
            }
            break;
        }

        transform.position = Camera.main.ViewportToWorldPoint(position);
    }


}
