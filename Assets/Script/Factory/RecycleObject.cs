using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class RecycleObject : MonoBehaviour
{
    /// <summary>
    /// ��Ȱ�� ������Ʈ�� ��Ȱ��ȭ �� �� ����Ǵ� ��������Ʈ
    /// </summary>
    public Action onDisable;
    protected virtual void OnEnable()
    {
        transform.localPosition = Vector3.zero; // �θ��� ��ġ
        transform.localRotation = Quaternion.identity; // �θ��� ȸ���� ����
        StopAllCoroutines(); // ������Ÿ�� �ʱ�ȭ
    }
    protected virtual void OnDisable()
    {
        onDisable?.Invoke(); //��Ȱ��ȭ �˸�(Ǯ���鶧 �� �����߰��Ǿ���)
    }
    /// <summary>
    /// �����ð� �Ŀ� ������Ʈ ��Ȱ��ȭ ��Ű�� �ڷ�ƾ
    /// </summary>
    /// <param name="delay">��Ȱ��ȭ�� �ɸ��� �ð�</param>
    /// <returns></returns>
    protected IEnumerator LifeOver(float delay = 0.0f)
    {
        yield return new WaitForSeconds(delay); //��ٸ���
        gameObject.SetActive(false); // ��Ȱ��ȭ
    }
}
