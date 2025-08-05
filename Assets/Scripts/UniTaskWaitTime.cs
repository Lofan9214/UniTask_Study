using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class UniTaskWaitTime : MonoBehaviour
{
    [SerializeField]
    private GameObject box;

    [SerializeField]
    private Vector3 coroutinePosition;

    [SerializeField]
    private Vector3 uniTaskPosition;

    void Start()
    {
        // �ڷ�ƾ�� ���� �ڷ�ƾ �޼ҵ� ����
        StartCoroutine(CoGenerateBox());

        // �����½�ũ �޼ҵ� ����
        // Forget()�� ���� �����½�ũ���� �߻��ϴ� ���ܸ� �����Ѵ�.
        TaskGenerateBox().Forget();
    }

    //IEnumerator�� ���� �ڷ�ƾ�� ����ϴ� �޼ҵ�
    private IEnumerator CoGenerateBox()
    {
        yield return new WaitForSeconds(5.0f);
        Instantiate(box, coroutinePosition, Quaternion.identity);
    }

    private async UniTaskVoid TaskGenerateBox()
    {
        // async void ���� �޼ҵ忡�� ���ܰ� �߻��ϸ� crash�� �߻��ϹǷ� UniTaskVoid�� ����ؾ� �Ѵ�.
        // UniTask.Delay(int milliseconds)�� �ڷ�ƾ�� WaitForSeconds, Task.Delay(int milliseconds)�� �����ȴ�.
        await UniTask.Delay(5000);
        Instantiate(box, uniTaskPosition, Quaternion.identity);
    }
}
