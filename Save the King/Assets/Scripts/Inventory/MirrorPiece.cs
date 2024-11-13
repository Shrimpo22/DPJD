using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MirrorPiece : MonoBehaviour
{
    public AudioClip soundClip;
    public TMP_Text text;
    public bool grabbed = false;
    public void textActivate()
    {
        text.text = "(E) Grab Mirror Piece";
        text.gameObject.SetActive(true);
    }

    public void textClear()
    {
        text.gameObject.SetActive(false);
    }


    public void grabMirrorPiece()
    {
        if(!grabbed){
            grabbed = true;
            GameObject inventory = GameObject.FindGameObjectWithTag("Inventory");
            inventory.GetComponent<Inventory>().AddItem("GlassShard", 1);
            StartCoroutine(PlayAudioAndDestroy());
        }
    }

    IEnumerator PlayAudioAndDestroy(){
        AudioSource audiosource = gameObject.AddComponent<AudioSource>();
        audiosource.clip = soundClip;
        audiosource.Play();
        yield return new WaitForSeconds(audiosource.clip.length + 0.2f);
        Destroy(this.transform.parent.gameObject);

    }
}
