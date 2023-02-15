using System;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.AddressableAssets.Addressables;

namespace Addressables
{
    public static class AddressableHelper
    {		
        public static void LoadAssetAsyncWithCompletionHandler<T>(string key, Action<T> completionHandlerCallback)
        {
            var handle = LoadAssetAsync<T>(key);
            handle.Completed += (_) => OnLoadedAssetAsync(key, _, completionHandlerCallback);
        }
     
        private static void OnLoadedAssetAsync<T>(string key, AsyncOperationHandle<T> handle, System.Action<T> completionHandlerCallback)
        {
            if(handle.Status == AsyncOperationStatus.Succeeded)
            {
                completionHandlerCallback(handle.Result);
            }
            else
            {
                Debug.LogError($"Failed to load addressable with address {key} of type {typeof(T)}");
                completionHandlerCallback(default);
            }
        }
    }
}