using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;


public class AssetDownloadPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI downloadText;
    [SerializeField]
    private TextMeshProUGUI downloadText2;

    [SerializeField]
    private Slider downloadProgress;

    [SerializeField]
    private Button downloadButton;

    [SerializeField]
    private Button closeButton;

    private long downloadSize;
    private long downloadAllSize;

    public void ShowDownloadPanel(long musicSize, long musicAllSize)
    {
        gameObject.SetActive(true);
        downloadSize = musicSize;
        downloadAllSize = musicAllSize;
        downloadText.text = musicSize.ToString();
        downloadText2.text = musicAllSize.ToString();
    }

    public void OnClickDownloadButton()
    {
        downloadButton.gameObject.SetActive(false);
        DownloadScene().Forget();
    }

    private async UniTaskVoid DownloadScene()
    {
        var handle = Addressables.DownloadDependenciesAsync("music");

        await UniTask.WaitUntil(() => handle.IsValid());

        while (handle.PercentComplete < 1f)
        {
            var status = handle.GetDownloadStatus();
            downloadText.text = $"{status.DownloadedBytes} / {status.TotalBytes} ( {status.Percent:P2} )";
            downloadProgress.value = status.Percent;
            await UniTask.NextFrame();
        }
        downloadText.text = "Complete";
        downloadProgress.value = handle.PercentComplete;

        closeButton.gameObject.SetActive(true);

        Addressables.Release(handle);
    }
}
