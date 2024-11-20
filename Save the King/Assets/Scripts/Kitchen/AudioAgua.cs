using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioAgua : MonoBehaviour
{
    public AudioClip soundClip; // O som a ser tocado
    public float minInterval = 3f; // Tempo mínimo entre reproduções
    public float maxInterval = 8f; // Tempo máximo entre reproduções

     private AudioSource audioSource;

    void Start()
    {
        // Adiciona um componente AudioSource ao GameObject (se ainda não existir)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Configura o AudioSource
        audioSource.clip = soundClip;
        audioSource.playOnAwake = false;
        audioSource.volume = 0.25f; // Define o volume para 0.25

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
