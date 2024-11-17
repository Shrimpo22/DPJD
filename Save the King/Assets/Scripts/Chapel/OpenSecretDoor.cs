using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenSecretDoor : MonoBehaviour
{

    public int rightPieces = 0;
    GameObject secretDoor;

    void Start()
    {
        secretDoor = this.gameObject;
    }

    public void sum(){
        rightPieces++;
    }

    public void subtract(){
        if(rightPieces>0)
            rightPieces--;
    }
    void Update()
    {
        if (rightPieces == 1){
            if(secretDoor.GetComponent<Door>()!=null)
                this.gameObject.GetComponent<Door>().OpenDoor();
        }
    }
}
