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
        // 코루틴을 통해 코루틴 메소드 실행
        StartCoroutine(CoGenerateBox());

        // 유니태스크 메소드 실행
        // Forget()을 통해 유니태스크에서 발생하는 예외를 무시한다.
        TaskGenerateBox().Forget();
    }

    //IEnumerator를 통해 코루틴을 사용하는 메소드
    private IEnumerator CoGenerateBox()
    {
        yield return new WaitForSeconds(5.0f);
        Instantiate(box, coroutinePosition, Quaternion.identity);
    }

    private async UniTaskVoid TaskGenerateBox()
    {
        // async void 사용시 메소드에서 예외가 발생하면 crash가 발생하므로 UniTaskVoid를 사용해야 한다.
        // UniTask.Delay(int milliseconds)는 코루틴의 WaitForSeconds, Task.Delay(int milliseconds)에 대응된다.
        await UniTask.Delay(5000);
        Instantiate(box, uniTaskPosition, Quaternion.identity);
    }
}
