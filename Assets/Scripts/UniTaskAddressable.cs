using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UniTaskAddressable : MonoBehaviour
{
    [SerializeField]
    private GameObject box;

    [SerializeField]
    private AssetReferenceGameObject refBox;

    private void Start()
    {
        LoadAndInstantiate().Forget();
    }


    private async UniTaskVoid LoadAndInstantiate()
    {
        var asset = await Addressables.LoadAssetAsync<GameObject>(refBox);
        var instance = Instantiate(asset);
        //Addressables.Release(asset);
    }
}
