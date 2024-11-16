using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static Key;

public class PlayerAttack : MonoBehaviour
{
    public Collider swordCollider;
    public int lightAttack = 10;
    public int heavyAttack = 20;
    public int stealthAttack = 10000;
    List<Collider> ignoreColliders = new List<Collider>();
    private HashSet<Collider> hitTargets = new HashSet<Collider>();
    private Animator animator;
    public Transform handTransform;
    public PlayerMovement player;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        handTransform = player.handTransform;
        IgnoreMyOwnColliders();
        swordCollider = GetComponent<Collider>();
        swordCollider.gameObject.SetActive(true);
        swordCollider.isTrigger = true;
        swordCollider.enabled = true;
        animator = GetComponentInParent<Animator>();
    }



    public void EnableSwordCollider()
    {
        swordCollider.enabled = true;
    }

    public void DisabeSwordCollider()
    {
        swordCollider.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Ignora as colis�es com os pr�prios colliders do personagem
        if (ignoreColliders.Contains(other))
        {
            return;
        }

        // Se o objeto atingido for um "Target" e ainda n�o tiver recebido dano
        if (other.CompareTag("Target") && !hitTargets.Contains(other))
        {
            AiAgent target = other.GetComponent<AiAgent>();
            if (target != null && (player.animator.GetCurrentAnimatorStateInfo(0).IsName("hit1") || player.animator.GetCurrentAnimatorStateInfo(0).IsName("hit2") || player.animator.GetCurrentAnimatorStateInfo(0).IsName("hit3")))
            {
                Debug.Log("Helooooooo");
                target.TakeDamage(lightAttack);
                hitTargets.Add(other);
            }
            else if (target != null && (player.animator.GetCurrentAnimatorStateInfo(0).IsName("heavy1") || player.animator.GetCurrentAnimatorStateInfo(0).IsName("heavy2")))
            {
                Debug.Log("HEAVYYYYY");
                target.TakeDamage(heavyAttack);
                hitTargets.Add(other);
            }
            else if (target != null && player.animator.GetCurrentAnimatorStateInfo(0).IsName("stabbing"))
            {
                target.TakeDamage(stealthAttack);
            }
        }
    }


    public void EquipSword()
    {
        this.gameObject.SetActive(true);
        this.tag = "Weapon";
        DisabeSwordCollider();
        transform.SetParent(handTransform);
        transform.localPosition = Vector3.zero; 
        transform.localRotation = Quaternion.identity;
        player.isSwordEquipped = true;
        player.swordColider = swordCollider;
        player.playerAttack = this;   
    }
    public void IgnoreMyOwnColliders()
    {
        Collider characterControllerCollider = GetComponent<Collider>();
        Collider[] damageableCharacterColliders = GetComponentsInChildren<Collider>();
        foreach (var collider in damageableCharacterColliders)
        {
            ignoreColliders.Add(collider);
        }
        ignoreColliders.Add(characterControllerCollider);

        foreach (var collider in ignoreColliders)
        {
            foreach (var otherCollider in ignoreColliders)
            {
                Physics.IgnoreCollision(collider, otherCollider, true);
            }
        }
    }

    public void PerformAttack()
    {
        hitTargets.Clear();
    }
    private void OnDrawGizmos()
    {
        if (swordCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(swordCollider.bounds.center, swordCollider.bounds.size);
        }
    }
}