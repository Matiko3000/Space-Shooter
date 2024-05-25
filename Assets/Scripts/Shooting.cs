using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.35f;

    [HideInInspector] public bool isFiring;

    [Header("AI Settings")]
    [SerializeField] bool useAI;
    [SerializeField] float fireRateVariance = 0f;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if(useAI) isFiring = true;
    }

    void Update()
    {
        Fire();
    }
    
    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinously()
    {
        while(true)
        {
            //Destroy(Instantiate(projectilePrefab, transform.position, Quaternion.identity), projectileLifetime);
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = transform.up * projectileSpeed;

            Destroy(instance, projectileLifetime);
            float tempFireRate = Random.Range(firingRate - fireRateVariance, firingRate + fireRateVariance);
            tempFireRate = Mathf.Clamp(tempFireRate, 0.1f, 2 * firingRate);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(tempFireRate);
        }
    }
}
