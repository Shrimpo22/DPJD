using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float damage;
    public float attackSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float getDamage(){
        return damage;
    }

    float getAttackSpeed(){
        return attackSpeed;
    }
}
