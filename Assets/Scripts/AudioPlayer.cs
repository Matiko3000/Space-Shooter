using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] float shootingVolume = 1f;

    [Header("Taking Damage")]
    [SerializeField] AudioClip damageTakenClip;
    [SerializeField][Range(0f, 1f)] float damageTakenVolume = 1f;

    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        int instanceCount = FindObjectsOfType<AudioPlayer>().Length;
        if (instanceCount > 1 )
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayShootingClip()
    {
        if (shootingClip != null) AudioSource.PlayClipAtPoint(shootingClip, Camera.main.transform.position, shootingVolume);
    }

    public void PlayDamageTakenClip()
    {
        if (damageTakenClip != null) AudioSource.PlayClipAtPoint(damageTakenClip, Camera.main.transform.position, damageTakenVolume);
    }
}
