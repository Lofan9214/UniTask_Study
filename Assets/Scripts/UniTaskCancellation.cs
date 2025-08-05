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
        // 코루틴을 통해 코루틴 메소드 실행
        coroutine = StartCoroutine(CoCancelWait());

        // 유니태스크 메소드 실행
        // Forget()을 통해 유니태스크에서 발생하는 예외를 무시한다.
        TaskCancelWait().Forget();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // StopCoroutine을 통해 코루틴을 종료한다.
            StopCoroutine(coroutine);
            // CancellationTokenSource의 Cancel을 이용해 취소 리퀘스트를 날린다
            tokenSource.Cancel();
        }
    }

    private void OnDestroy()
    {
        // 파괴될 때 취소 리퀘스트를 날린다.
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
