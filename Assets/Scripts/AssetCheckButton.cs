using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AssetCheckButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI downloadSizeText;

    [SerializeField]
    private AssetDownloadPanel downloadPanel;

    public void CheckMusicDownloaded()
    {
        CheckMusicDownload().Forget();
        //CheckCatalogUpdate().Forget();
    }

    private async UniTaskVoid CheckMusicDownload()
    {
        var assetSize = await Addressables.GetDownloadSizeAsync("music");
        var assetSize2 = await Addressables.GetDownloadSizeAsync("musicAll");
        if (assetSize > 0 || assetSize2 > 0)
        {
            downloadPanel.ShowDownloadPanel(assetSize, assetSize2);
        }
        else
        {
            downloadSizeText.text = $"Download Not Need";
        }
    }

    private async UniTaskVoid CheckCatalogUpdate()
    {
        var result = await Addressables.CheckForCatalogUpdates();

        if (result.Count > 0)
        {
            downloadSizeText.text = string.Join(',', result.ToArray());
        }
    }
}
