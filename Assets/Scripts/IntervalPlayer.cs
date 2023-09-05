using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntervalPlayer : MonoBehaviour
{
    private static IntervalPlayer _instance;
    
    private AudioSource _audioSource;

    private AudioClip _firstNote;
    private AudioClip _secondNote;
    
    private Coroutine _playIntervalCoroutine;
    
    public static IntervalPlayer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<IntervalPlayer>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(nameof(IntervalPlayer));
                    _instance = singletonObject.AddComponent<IntervalPlayer>();
                }
                DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayInterval(bool ascending)
    {
        _playIntervalCoroutine = StartCoroutine(PlayIntervalCoroutine(ascending));
    }
    
    public void StopInterval()
    {
        if (_playIntervalCoroutine != null)
        {
            StopCoroutine(_playIntervalCoroutine);
            _audioSource.Stop();
        }
    }

    private IEnumerator PlayIntervalCoroutine(bool ascending)
    {
        if (_audioSource == null)
        {
            Debug.Log("Audio source is null");
            yield return null;
        }
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                if (ascending)
                {
                    _audioSource.PlayOneShot(_firstNote);
                }
                else
                {
                    _audioSource.PlayOneShot(_secondNote);
                }
            }
            else
            {
                if (ascending)
                {
                    _audioSource.PlayOneShot(_secondNote);
                }
                else
                {
                    _audioSource.PlayOneShot(_firstNote);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void SetFirstClip(AudioClip clip)
    {
        _firstNote = clip;
    }
    
    public void SetSecondClip(AudioClip clip)
    {
        _secondNote = clip;
    }

    public IEnumerator Play()
    {
        _audioSource.clip = _firstNote;
        _audioSource.Play();
        while (_audioSource.isPlaying)
        {
            yield return null;
        }
        _audioSource.clip = _secondNote;
        _audioSource.Play();
    }
}
