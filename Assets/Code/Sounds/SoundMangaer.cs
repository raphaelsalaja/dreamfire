using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip playerHitSound, fireSound, enemyDeathSound;
    public AudioSource audioSrc;
    private void Start()
    {
        //playerHitSound =
        playerHitSound = Resources.Load<AudioClip>("hit_001");
        fireSound = Resources.Load<AudioClip>("snShoot");
        enemyDeathSound = Resources.Load<AudioClip>("snDeath");
        audioSrc.GetComponent<AudioSource>();
    }

}
