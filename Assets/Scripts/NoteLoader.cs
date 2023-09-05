using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class NoteLoader
{
    public static AudioClip LoadAudioClip(string key)
    {
        AsyncOperationHandle<AudioClip> handle = Addressables.LoadAssetAsync<AudioClip>("LP/" + key);
        handle.WaitForCompletion();
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            return handle.Result;
        }
        else
        {
            Debug.Log("Failed to load asset: " + handle.Result.name);
            return null;
        }
    }
}
