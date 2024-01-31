using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.Mathematics;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : RecycleObject
{ //Objectpool<����>, where T : MonoBehaviour = TŸ���� MonoBehaviour�̰ų�
    //Mono�� ��ӹ��� Ŭ������ �����ϴ�.

    /// <summary>
    /// Ǯ���� ������ ������Ʈ�� ������
    /// </summary>
    public GameObject originalprefab;

    /// <summary>
    ///Ǯ�� ũ��, ó���� �����ϴ� ������Ʈ�� ���� 2^n�����θ� ������
    /// </summary>
    public int poolSize = 64;
    /// <summary>
    /// TŸ������ ������ ������Ʈ�� �迭.
    /// </summary>
    T[] pool;
    private void Awake()
    {
        Initialize();
    }
    /// <summary>
    /// ���� ��밡����(��Ȱ��ȭ��) ������Ʈ�� ����
    /// </summary>
    Queue<T> readyQueue;
    public void Initialize()
    {
        if (pool == null)
        {// Ǯ�� �ȸ�������ٸ�

            pool = new T[poolSize]; // �迭 ũ�⸸ŭ new
            readyQueue = new Queue<T>(poolSize); // ����ť ����� capacity�� poolsize�� ������
            GenerateObject(0, poolSize, pool);
        }
        else
        {
            //�̹� ������� �ִ� ���(ex:�� �߰��ε� or �� �ٽ� ����
            foreach (T obj in pool)
            // Ư�� �÷��� �ȿ� �ִ� ��� ��Ҹ� �ѹ��� ó���ؾ� �� �� 
            { // foreach ��
                obj.gameObject.SetActive(false);
            }
        }

    }

    /// <summary>
    /// Ǯ���� ������� ���� ������Ʈ �ϳ� ���� ����, ������ ����
    /// </summary>
    /// <returns>Ǯ���� ���� ������Ʈ(Ȱ��ȭ��)</returns>
    public T GetObject(Vector3 position)
    {
        if (readyQueue.Count > 0) // ����ť�� ������Ʈ Ȯ��
        {
            T comp = readyQueue.Dequeue(); // ť�ϳ� ������, ������ ����
            comp.gameObject.SetActive(true);//Ȱ��ȭ ��Ŵ
            OnGetObject(comp); // ������Ʈ�� �߰�ó��
            return comp; // ����
        }
        else // ť�� ����ų� ���� ������Ʈ�� ����
        {
            ExpandPool(); // Ǯ �ι� Ȯ��
            return GetObject(); // �ٽ� Ȯ��
        }

    }
    //�� ������Ʈ �� Ư��ó���� �ʿ��� �� ���
    protected virtual void OnGetObject(T component)
    {

    }
    /// <summary>
    /// Ǯ���� ������� ���� ������Ʈ ���� ����
    /// </summary>
    /// <param name="position">��ġ�� ��ġ(������ǥ)</param>
    /// <param name="quaternion">��ġ�� ���� ����</param>
    /// <returns>Ǯ���� ���� ������Ʈ(Ȱ��ȭ)</returns>
    public T GetObject(Vector3? position = null, Vector3? eulerAngle = null)
    {
        if (readyQueue.Count > 0) // ����ť�� ������Ʈ Ȯ��
        {
            T comp = readyQueue.Dequeue(); // ť�ϳ� ������, ������ ����
            comp.gameObject.SetActive(true);//Ȱ��ȭ ��Ŵ
            comp.transform.position = position.GetValueOrDefault(); //������ ��ġ �̵�
            comp.transform.Rotate(eulerAngle.GetValueOrDefault());//������ ������ ȸ��
            OnGetObject(comp); // ������Ʈ�� �߰�ó��
            return comp; // ����
        }
        else // ť�� ����ų� ���� ������Ʈ�� ����
        {
            ExpandPool(); // Ǯ �ι� Ȯ��
            return GetObject(); // �ٽ� Ȯ��
        }

    }



    void ExpandPool()
    { // �Ͼ�� �ȵǴ� ���̶� ��� ǥ��
        Debug.LogWarning($"{gameObject.name} Ǯ������ ����. {poolSize} -> {poolSize * 2}");
        int newSize = poolSize * 2; //�� Ǯ ũ�� ����
        T[] newPool = new T[newSize]; // �� Ǯ ����
        for (int i = 0; i < poolSize; i++) // ����Ǯ ���� ����
        {
            newPool[i] = pool[i];
        }// ���� �κп� ������Ʈ �����ؼ� �߰�
        GenerateObject(poolSize, newSize, newPool);

        pool = newPool;
        poolSize = newSize; //�� Ǯ�� Ǯ�� ����
    }
    /// <summary>
    /// Ǯ���� ����� ������Ʈ �����ϴ� �Լ�
    /// </summary>
    /// <param name="start">���� ���� ������ �ε���</param>
    /// <param name="end">���� ������ ������ �ε���</param>
    /// <param name="results">������ ������Ʈ�� �� �迭</param>
    void GenerateObject(int start, int end, T[] results)
    {
        for (int i = start; i < end; ++i)
        {
            //������ �����ؼ�
            GameObject obj = Instantiate(originalprefab, transform);
            //�̸� �ٲٰ�
            obj.name = $"{originalprefab.name}_{i}";

            T comp = obj.GetComponent<T>();
            //��Ȱ��ȭ �� ��Ȱ���ϱ�,false�� �߰���, ���ٿ� ���������� ������
            comp.onDisable += () => readyQueue.Enqueue(comp);
            // readyQueue.Enqueue(comp); // ����ť�� �߰�

            results[i] = comp; // �迭�� �����ϰ�
            obj.SetActive(false); // ��Ȱ��ȭ
        }

    }

}
