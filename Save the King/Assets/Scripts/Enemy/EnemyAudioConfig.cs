using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyAudioConfig", menuName = "Enemy/AudioConfig")]
public class EnemyAudioConfig : ScriptableObject
{
    public AudioClip walkSound;
    public AudioClip hitSound;
    public AudioClip deathSound;
    public AudioClip swingSound;
    public AudioClip swingSound2;
    public AudioClip swingSound3;
    public AudioClip swingSound4;
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;
    public AudioClip playerSwordGrunt;
    public AudioClip throwGruntSound;
    public AudioClip bossGrunt;

}
