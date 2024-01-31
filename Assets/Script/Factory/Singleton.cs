using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Component
{

    /// <summary>
    /// �� �̱����� �ʱ�ȭ�Ǿ����� Ȯ���ϱ� ���� ����
    /// </summary>
    bool isInitialized = false;

    /// <summary>
    /// ����ó���� ������ Ȯ���ϱ� ���� ����
    /// </summary>
    private static bool isShutdown = false;

    /// <summary>
    /// �� �̱����� ��ü(�ν��Ͻ�)
    /// </summary>
    private static T instance = null;

    /// <summary>
    /// �� �̱����� ��ü�� �б� ���� ������Ƽ.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (isShutdown)  // ����ó���� ������
            {
                Debug.LogWarning("�̱����� �̹� �������̴�.");     // �������ϰ�
                return null;                                     // null ����
            }

            if (instance == null)    // ��ü�� ������
            {
                T singleton = FindAnyObjectByType<T>();         // �ٸ� ���� ������Ʈ�� �ش� �̱����� �ִ��� Ȯ��
                if (singleton == null)                           // �ٸ� ���� ������Ʈ���� �� �̱����� ������
                {
                    GameObject obj = new GameObject();          // �� ���� ������Ʈ �����
                    obj.name = "Singleton";                     // �̸� ������ ����
                    singleton = obj.AddComponent<T>();          // �̱��� ������Ʈ ���� �߰�
                }
                instance = singleton;   // �ٸ� ���ӿ�����Ʈ�� �ִ� �̱����̳� ���θ��� �̱����� ����
                DontDestroyOnLoad(instance.gameObject);         // ���� ����� �� ���ӿ�����Ʈ�� �������� �ʵ��� ����
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)        // ���� �̹� ��ġ�� �ٸ� �̱����� ����.
        {
            instance = this as T;   // ù��°�� ����
            DontDestroyOnLoad(instance.gameObject); // ���� ����� �� ���ӿ�����Ʈ�� �������� �ʵ��� ����
        }
        else
        {
            // �̹� ���� �̱����� �ִ�.
            if (instance != this)    // �װ� ���ڽ��� �ƴϸ�
            {
                Destroy(this.gameObject);   // ���ڽ��� ����
            }
        }
    }

    private void OnEnable()
    {
        // SceneManager.sceneLoaded�� ���� �ε�Ǹ� ����Ǵ� ��������Ʈ
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// ���� �ε�Ǿ��� �� ȣ��� �Լ�
    /// </summary>
    /// <param name="scene">������</param>
    /// <param name="mode">�ε����</param>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!isInitialized)
        {
            OnPreInitialize();
        }
        if (mode != LoadSceneMode.Additive)  // additive�� �ƴҶ��� ����
        {
            OnInitialize();
        }
    }

    /// <summary>
    /// �̱����� ������� �� �� �ѹ��� ȣ��Ǵ� �Լ�
    /// </summary>
    protected virtual void OnPreInitialize()
    {
        isInitialized = true;
    }

    /// <summary>
    /// �̱����� ��������� ���� ����� ������ ȣ��� �Լ�(additive�� �ȵ�)
    /// </summary>
    protected virtual void OnInitialize()
    {
    }


    private void OnApplicationQuit()
    {
        isShutdown = true;
    }
}


// �̱����� ������ ��ü�� 1���̾�� �Ѵ�.
public class TestSingleton
{
    private static TestSingleton instance = null;
    
    public static TestSingleton Instance
    {
        get
        {
            if (instance == null)   // ������ �ν��Ͻ��� ������� ���� ������
            {
                instance = new TestSingleton(); // �ν��Ͻ� ����
            }
            return instance;
        }
    }

    private TestSingleton()
    {
        // ��ü�� �ߺ����� �����Ǵ� ���� �����ϱ� ���� �����ڸ� private���� �Ѵ�.(�⺻ �����ڰ� ��������� ���� ����)
    }
}

//TestSingleton a = new TestSingleton();
//TestSingleton b = new TestSingleton();

//TestSingleton.Instance.i = 30;
//int i = TestSingleton.Instance.i;
