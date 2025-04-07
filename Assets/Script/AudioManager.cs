using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClip _snapSound;
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        // Singleton implementation
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate instances
            return;
        }

        DontDestroyOnLoad(gameObject); // Persist across scenes
    }

    public void Snap()
    {
        Debug.Log("SOUND");
        _audioSource.PlayOneShot(_snapSound);
    }
}
