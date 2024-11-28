    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
    {
        public GameObject quest;
        public GameObject trigger;
        public GameObject text; 
        public AudioSource audioSource;
        public bool hasPlayed=false;
        
        public AudioClip soundquest;  
        void Start(){
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        if(soundquest == null)
            soundquest = Resources.Load<AudioClip>("Sounds/Puma");
        }
       
        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player"){
                if(!hasPlayed){
                StartCoroutine(Quest_start());
                hasPlayed=true;
                }
            }
        }

        private IEnumerator Quest_start(){
            audioSource.clip = soundquest;
            audioSource.Play();
            quest.SetActive(true);
            quest.GetComponent<Animation>().Play("ObjectiveDisplayAnimation");
            text.GetComponent<Text>().text = "Leave the Cell";
            yield return new WaitForSeconds(5f);
            
            text.GetComponent<Text>().text = "";
            quest.SetActive(false);
            quest.SetActive(false );
        }
    }
