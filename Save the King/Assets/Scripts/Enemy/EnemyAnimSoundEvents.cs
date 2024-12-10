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
    public void EnemyAttack3()
    {
        if(audioConfig.attack2 != null)
            audioSource.PlayOneShot(audioConfig.attack3);
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
    public void EnemyAttackSwing2()
    {
        if(audioConfig.swingSound2 != null)
            audioSource.PlayOneShot(audioConfig.swingSound2);
    }
      public void EnemyAttackSwing3()
    {
        if(audioConfig.swingSound3 != null)
            audioSource.PlayOneShot(audioConfig.swingSound3);
    }
      public void EnemyAttackSwing4()
    {
        if(audioConfig.swingSound4 != null)
            audioSource.PlayOneShot(audioConfig.swingSound4);
    }
    public void ThrowGruntSound()
    {
        if(audioConfig.throwGruntSound != null)
            audioSource.PlayOneShot(audioConfig.throwGruntSound);
    }

    public void BossGrunt()
    {
        if(audioConfig.bossGrunt != null)
            audioSource.PlayOneShot(audioConfig.bossGrunt);
    }

}