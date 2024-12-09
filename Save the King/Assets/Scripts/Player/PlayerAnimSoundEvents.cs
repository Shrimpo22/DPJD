using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimSoundEvents : MonoBehaviour
{

    private AudioSource audioSource;
    private Animator animator;

    [Header("AudioClips")]
    public AudioClip m1;
    public AudioClip m2;
    public AudioClip playerHitted;
    public AudioClip playerAttackGruntM1;
    public AudioClip playerAttackGruntM2;
    public AudioClip dodge;
    public AudioClip walk;
    public AudioClip die;
    public AudioClip stealtAttackSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStealthAttackSound()
    {
        if(die != null)
            audioSource.PlayOneShot(stealtAttackSound);
    }

    public void PlayM1Sound()
    {
        if(m1!= null)
            audioSource.PlayOneShot(m1);
    }
    public void PlayM2Sound()
    {
        if(m2!= null)
            audioSource.PlayOneShot(m2);
    }
    public void PlayDodgeSound()
    {
        if(dodge!= null)
            audioSource.PlayOneShot(dodge);
    }

    public void PlayPlayerHittedSound()
    {
        if (playerHitted!=null)
            audioSource.PlayOneShot(playerHitted);
    }

    public void PlayPlayerAttackGruntSoundM1()
    {
        if (playerAttackGruntM1 != null)
            audioSource.PlayOneShot(playerAttackGruntM1);
    }    
    public void PlayPlayerAttackGruntSoundM2()
    {
        if (playerAttackGruntM2 != null)
            audioSource.PlayOneShot(playerAttackGruntM2);
    }

    public void PlayWalkSound()
    {
        float speed = animator.GetFloat("movementSpeed");
        if(walk != null && speed > 0.5f ){
            audioSource.PlayOneShot(walk);
        }
    }

    public void PlayDieSound()
    {
        if(die != null)
            audioSource.PlayOneShot(die);
    }
}
