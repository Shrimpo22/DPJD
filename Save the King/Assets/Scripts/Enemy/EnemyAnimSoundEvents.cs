using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimSoundEvents : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    [Header("AudioClips")]
    public AudioClip playerSwordGrunt;
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayEnemyGetHitSound()
    {

    }
}
