using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandelabraLights : MonoBehaviour
{
    AudioSource audiosource;
    public AudioClip burning;
    public AudioClip ignite;
    public AudioClip blow;

    void Start(){
        audiosource = gameObject.AddComponent<AudioSource>();
        audiosource.maxDistance = 5.0f;
        audiosource.volume = 0.5f;
    }

    public bool lit = false;
    void OnMouseDown(){
        if(!lit)
            audiosource.clip = ignite;
        else
            audiosource.clip = blow;
        audiosource.loop = false;
        audiosource.Play();
        foreach (Transform child in transform){
            child.gameObject.SetActive(!child.gameObject.activeSelf);
            lit = child.gameObject.activeSelf;
        }
        
        gameObject.transform.parent.gameObject.GetComponent<CandelabraCombo>().CheckCombo();
    }

    void Update(){
        if (lit){
            if (!audiosource.isPlaying){
                if(audiosource.clip != burning)
                    audiosource.clip = burning;
                audiosource.loop = true;
                audiosource.Play();
            }    
        }
    }
}
