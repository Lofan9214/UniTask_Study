using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class UniTaskCancellation : MonoBehaviour
{
    private Coroutine coroutine;
    private CancellationTokenSource tokenSource;

    private void Awake()
    {
        tokenSource = new CancellationTokenSource();
    }

    private void Start()
    {
        // �ڷ�ƾ�� ���� �ڷ�ƾ �޼ҵ� ����
        coroutine = StartCoroutine(CoCancelWait());

        // �����½�ũ �޼ҵ� ����
        // Forget()�� ���� �����½�ũ���� �߻��ϴ� ���ܸ� �����Ѵ�.
        TaskCancelWait().Forget();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // StopCoroutine�� ���� �ڷ�ƾ�� �����Ѵ�.
            StopCoroutine(coroutine);
            // CancellationTokenSource�� Cancel�� �̿��� ��� ������Ʈ�� ������
            tokenSource.Cancel();
        }
    }

    private void OnDestroy()
    {
        // �ı��� �� ��� ������Ʈ�� ������.
        tokenSource.Cancel();
        tokenSource.Dispose();
    }

    private IEnumerator CoCancelWait()
    {
        yield return new WaitForSeconds(5.0f);
        Debug.Log("CoOver, 5 sec");
    }

    private async UniTaskVoid TaskCancelWait()
    {
        await UniTask.Delay(5000, cancellationToken: tokenSource.Token);
        await UniTask.Delay(5000, cancellationToken: this.GetCancellationTokenOnDestroy());
        Debug.Log("TaskOver, 5 sec");
    }
}
