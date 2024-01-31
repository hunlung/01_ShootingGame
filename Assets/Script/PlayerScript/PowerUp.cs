using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public int Power = 0;

    public GameObject powerupprefab;

    Transform PowerTransform;
    // Start is called before the first frame update
    private void Start()
    {
        PowerTransform = transform.GetChild(0);
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PowerItem"))
        {
            Destroy(collision.gameObject);
            switch (Power)
            {
                case 0:
                    Power++;
                    Instantiate(powerupprefab, PowerTransform.position, Quaternion.identity);
                    PowerTransform = transform.GetChild(1);
                    break;
                case 1:
                    Power++;
                    Instantiate(powerupprefab, PowerTransform.position, Quaternion.identity);
                    PowerTransform = transform.GetChild(2);
                    break;
                case 2:
                    Power++;
                    Instantiate(powerupprefab, PowerTransform.position, Quaternion.identity);
                    PowerTransform = transform.GetChild(3);
                    break;
                default:
                    break;

            }

        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Power = 0;
        }
    }
}
