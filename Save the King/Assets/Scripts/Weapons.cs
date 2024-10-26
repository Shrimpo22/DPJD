using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    public float damage;
    public float attackSpeed;

    private Collider swordCollider;
    private bool hitPlayer;

    // Start is called before the first frame update
    void Start()
    {
        swordCollider = GetComponent<Collider>();
        swordCollider.enabled = false;        
        swordCollider.isTrigger = true;
        hitPlayer = false;        
    }

    public void EnableCollider(){
        swordCollider.enabled = true;
    }

    public void DisableCollider(){
        swordCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player") && hitPlayer == false){
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            player.TakeDamage(damage);
            hitPlayer = true;     
        }
    }

    public void RemoveHitTargets(){
        hitPlayer = false;
    }

    float getDamage(){
        return damage;
    }

    float getAttackSpeed(){
        return attackSpeed;
    }
}
