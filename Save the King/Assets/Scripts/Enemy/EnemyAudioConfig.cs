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
    public AudioClip attack1;
    public AudioClip attack2;
    public AudioClip attack3;
    public AudioClip playerSwordGrunt;
}
