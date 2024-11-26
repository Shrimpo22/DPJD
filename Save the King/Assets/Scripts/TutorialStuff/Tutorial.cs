    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
    {
        public GameObject quest;
        public GameObject trigger;
        public GameObject text; 
        public AudioSource soundOfQuest;  
       
        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player"){
                StartCoroutine(Quest_start());
            }
        }

        private IEnumerator Quest_start(){
            soundOfQuest.Play();
            quest.GetComponent<Animation>().Play("ObjDisplayAnimation");
            text.GetComponent<Text>().text = "Leave the Cell";
            yield return new WaitForSeconds(5f);
            
            text.GetComponent<Text>().text = "";
            quest.SetActive(false);
        }
    }
