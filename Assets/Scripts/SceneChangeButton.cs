using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using TMPro;

public class SceneChangeButton : MonoBehaviour
{
    [SerializeField]
    private const string sceneName = "Assets/Scenes/AddressableScene.unity";

    [SerializeField]
    private TextMeshProUGUI downloadSizeText;

    public void LoadScene()
    {
        CheckNeedSceneDownload().Forget();
    }

    private async UniTaskVoid CheckNeedSceneDownload()
    {
        long size = await Addressables.GetDownloadSizeAsync(sceneName);

        if (size > 0)
        {
            downloadSizeText.text = size.ToString();
            await DownloadScene(size);
        }

        await Addressables.LoadSceneAsync(sceneName);
    }

    private async UniTask DownloadScene(long size)
    {
        var handle = Addressables.DownloadDependenciesAsync(sceneName);

        await UniTask.WaitUntil(() => handle.IsValid());

        while (handle.PercentComplete < 1f)
        {
            var status = handle.GetDownloadStatus();
            downloadSizeText.text = $"{status.DownloadedBytes} / {status.TotalBytes} ( {status.Percent:P2} )";
            await UniTask.Delay(10);
        }

        Addressables.Release(handle);
    }
}
