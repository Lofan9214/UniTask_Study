using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class UniTaskWaitUntil : MonoBehaviour
{
    [SerializeField]
    private GameObject box;

    [SerializeField]
    private Vector3 coroutinePosition;

    [SerializeField]
    private Vector3 uniTaskPosition;

    [SerializeField]
    private bool checkBox = false;

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
        yield return new WaitUntil(() => checkBox);
        Instantiate(box, coroutinePosition, Quaternion.identity);
    }

    private async UniTaskVoid TaskGenerateBox()
    {
        // async void ���� �޼ҵ忡�� ���ܰ� �߻��ϸ� crash�� �߻��ϹǷ� UniTaskVoid�� ����ؾ� �Ѵ�.
        // UniTask.WaitUntil(predicate)�� �ڷ�ƾ�� WaitUntil(predicate)�� �����ȴ�.
        await UniTask.WaitUntil(() => checkBox);
        Instantiate(box, uniTaskPosition, Quaternion.identity);
    }
}
