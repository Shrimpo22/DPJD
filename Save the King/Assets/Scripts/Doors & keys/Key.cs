using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Key : MonoBehaviour//Item
{
    [SerializeField] public TMP_Text textKey;
    [SerializeField] KeyType keyType;
    private AudioSource audioSource;
    public enum KeyType{
        None,
        keyOfCell,
        keyOfKingThrone,
        keyOfGeneralRoom2,
        keyOfFeastRoom,

    }

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }
    
   

    public void textActivate(){
      textKey.text  = "(E) Grab Key";
      textKey.gameObject.SetActive(true);  
    }

    public void textClear(){
      textKey.gameObject.SetActive(false);  
    }
    /*public override string GiveName()
    {
        return nameOfKey;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/nameOfKey Icon");
    }*/

    private TMP_Text textOfKey;
    public void grabKey(GameObject player) {
        
        if (Input.GetKeyDown(KeyCode.E)){
            //grabbed
            //GameObject.FindGameObjectsWithTag("Inventory").GetComponent<Inventory>().AddItem(keyType.ToString,1,GiveItemImage());
            player.GetComponent<PlayerEventItens>().AddKey(keyType);
            audioSource.Play();
            Destroy(this.gameObject, audioSource.clip.length);
        }   
    }

    private void OnTriggerExit(Collider other){
        textClear();
    }


}
