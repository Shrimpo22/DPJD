using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimSoundEvents : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    public EnemyAudioConfig audioConfig;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    public void PlayEnemyGetHitSound()
    {
        if (audioConfig.playerSwordGrunt != null)
            audioSource.PlayOneShot(audioConfig.playerSwordGrunt);
    }

    public void EnemyAttack()
    {
        if(audioConfig.attack1 != null)
            audioSource.PlayOneShot(audioConfig.attack1);
    }
    
    public void EnemyAttack2()
    {
        if(audioConfig.attack2 != null)
            audioSource.PlayOneShot(audioConfig.attack2);
    }

    public void EnemyWalk()
    {
        float speed = animator.GetFloat("speed");
        if (audioConfig.walkSound != null && speed > 0.5f)
            Debug.Log("Estou a andar");
        {
            audioSource.PlayOneShot(audioConfig.walkSound);
        }
    }

    public void EnemyGetHit()
    {
        if (audioConfig.hitSound != null)
            audioSource.PlayOneShot(audioConfig.hitSound);
    }    
    
    public void EnemyDeath()
    {
        if (audioConfig.deathSound != null)
            audioSource.PlayOneShot(audioConfig.deathSound);
    }

    public void EnemyAttackSwing()
    {
        if(audioConfig.swingSound != null)
            audioSource.PlayOneShot(audioConfig.swingSound);
    }
}
