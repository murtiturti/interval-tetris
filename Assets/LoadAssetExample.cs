using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadAssetExample : MonoBehaviour
{
    public string key;
    private AudioSource _audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Addressables.LoadAssetAsync<AudioClip>("LP/" + key).Completed += LoadCompleted;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void LoadCompleted(AsyncOperationHandle<AudioClip> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Loaded asset: " + obj.Result.name);
            _audioSource.clip = obj.Result;
            _audioSource.Play();
        }
        else
        {
            Debug.Log("Failed to load asset: " + obj.Result.name);
        }
    }
}
