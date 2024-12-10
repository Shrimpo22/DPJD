using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAgua : MonoBehaviour
{
    public AudioClip soundClip; // O som a ser tocado
    public float minInterval = 3f; // Tempo mínimo entre reproduções
    public float maxInterval = 8f; // Tempo máximo entre reproduções

    public AudioSource audioSource;

    void Start()
    {

        // Configura o AudioSource
        audioSource.clip = soundClip;
        audioSource.playOnAwake = false;

        // Inicia a reprodução aleatória
        StartCoroutine(PlaySoundRandomly());
    }

    IEnumerator PlaySoundRandomly()
    {
        while (true)
        {
            // Escolhe um intervalo aleatório entre minInterval e maxInterval
            float waitTime = Random.Range(minInterval, maxInterval);

            // Espera pelo intervalo escolhido
            yield return new WaitForSeconds(waitTime);

            // Toca o som se houver um clipe atribuído
            if (audioSource.clip != null)
            {
                audioSource.Play();
            }
        }
    }
}
