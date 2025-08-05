# 유니태스크 공부 레포지토리

유니티의 유니태스크를 통한 비동기 프로그래밍을 공부하기 위한 레포지토리입니다.

|  | 코루틴 | 유니태스크 |
| --- | --- | --- |
| 일정시간 대기 | yield return WaitForSeconds | await UniTask.Delay |
| 다음 프레임까지 대기 | yield return null | await UniTask.Yield // await UniTask.NextFrame |
| 프레임 끝까지 대기 | yield return WaitForEndOfFrame | await UniTask.WaitForEndofFrame |
| 픽스드업데이트 대기 | yield return WaitForFixedUpdate | await UniTask.WaitForFixedUpdate |
| 조건 대기 | yield return WaitUntil | await UniTask.WaitUntil |
