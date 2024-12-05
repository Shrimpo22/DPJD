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
        public float time = 5f;
        
        public AudioClip soundquest;  
        void Start(){
        if(audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        if(time<3) {
            time = 5;
        }
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
        if(!hasPlayed)
        {
            if(soundquest != null){
                audioSource.clip = soundquest;
                audioSource.Play();
            }
            quest.SetActive(true);
            quest.GetComponent<Animation>().Play();
            hasPlayed = true;
        }
            yield return new WaitForSeconds(time);
            
            text.GetComponent<Text>().text = "";
            quest.SetActive(false);
            quest.SetActive(false );
            Destroy(quest.gameObject);
            Destroy(gameObject);
        }   
    }
