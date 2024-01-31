using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultySpawner : MonoBehaviour
{
    
    [System.Serializable]
    public struct SpawnData
    {
        public SpawnData(PoolObjectType type = PoolObjectType.ShootEnemy, float interval = 0.5f)
        {
            this.spawnType = type;
            this.interval = interval;
        }

        public PoolObjectType spawnType;
        public float interval;
    }

    public SpawnData[] spawnDatas;


    const float Minx = -8.0f;
    const float Maxx = 8.0f;


    private void Start()
    {
        foreach (var data in spawnDatas)
        {
            StartCoroutine(SpawnCoroutine(data));
        }
    }

    private IEnumerator SpawnCoroutine(SpawnData data)
    {
        
        while (true)
        {
            yield return new WaitForSeconds(data.interval);
            float height = Random.Range(Minx, Maxx);
            
            GameObject obj = Factory.Instance.GetObject(data.spawnType, new(height, transform.position.y, 0.0f));
            

        }
        
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;                             // ���� ����
        Vector3 p0 = transform.position + Vector3.right * Maxx;    // ���� ������ ���
        Vector3 p1 = transform.position + Vector3.right * Minx;    // ���� ������ ���
        Gizmos.DrawLine(p0, p1);                                // ���������� ���������� ���� �ߴ´�.

    }

    private void OnDrawGizmosSelected()
    {
        // �� ������Ʈ�� �������� �� �簢�� �׸���(���� �����ϱ�)

        Gizmos.color = Color.red;                             // ���� ����        
        Vector3 p0 = transform.position + Vector3.right * Maxx - Vector3.right * 0.5f;
        Vector3 p1 = transform.position + Vector3.right * Maxx + Vector3.right * 0.5f;
        Vector3 p2 = transform.position + Vector3.right * Minx + Vector3.right * 0.5f;
        Vector3 p3 = transform.position + Vector3.right * Minx - Vector3.right * 0.5f;
        Gizmos.DrawLine(p0, p1);
        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p0);


    }
}