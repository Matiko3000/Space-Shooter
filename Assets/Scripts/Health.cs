using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] int scorePerEnemyKilled = 25;

    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;

    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null )
        {
            damageDealer.Hit();
            PlayHitEffeect();
            TakeDamage(damageDealer.GetDamage());
        }
    }

    void TakeDamage(int damage)
    {
        ShakeCamera();
        health -= damage;
        audioPlayer.PlayDamageTakenClip();
        if (health <= 0)
        {
            if (gameObject.tag == "Enemy") scoreKeeper.addScore(scorePerEnemyKilled);
            else levelManager.LoadGameOver();
            Destroy(gameObject);
        }
    }

    void PlayHitEffeect()
    {
        if (hitEffect != null) 
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position + new Vector3(0, 0.1f, 0), Quaternion.identity);
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera()
    {
        if (cameraShake != null && applyCameraShake) cameraShake.Play();  
    }

    public int GetCurrentHealth()
    {
        return health;
    }
}
