    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Tutorial : MonoBehaviour
    {
        public Canvas canvas;
        public GameObject sword;
        public bool enableSword = false;    
        void Start()
        {
        canvas.gameObject.SetActive(false); 
        }

        private void OnTriggerEnter(Collider other) {
            if(other.tag == "Player"){
            canvas.gameObject.SetActive(true); 
            GetComponent<Collider>().enabled = false; 
            StartCoroutine(DisplayImageForSeconds(8));
            }
        }

        IEnumerator DisplayImageForSeconds(float duration)
        {
            
            
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(duration); 

            canvas.gameObject.SetActive(false); 
            Time.timeScale = 1;
            if(enableSword) sword.SetActive(true);
            gameObject.SetActive(false); 
            
        }

    }
