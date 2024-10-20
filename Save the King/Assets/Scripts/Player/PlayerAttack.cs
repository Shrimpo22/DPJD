using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider swordCollider;
    public int damage = 10;
    List<Collider> ignoreColliders = new List<Collider>();
    private HashSet<Collider> hitTargets = new HashSet<Collider>();
    private Animator animator;

    void Start()
    {
        IgnoreMyOwnColliders();
        swordCollider = GetComponent<Collider>();
        swordCollider.gameObject.SetActive(true);
        swordCollider.isTrigger = true;
        swordCollider.enabled = false;
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
            if (target != null)
            {
                target.TakeDamage(damage);
                hitTargets.Add(other); // Adiciona o alvo � lista de alvos atingidos
            }
        }
        
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

    // Ativar o colisor durante a anima��o de ataque
    public void PerformAttack()
    {
        hitTargets.Clear();
    }
    // Coroutine para desativar o colisor ap�s um curto per�odo

    // Gizmos para visualizar o colisor da espada no editor
    private void OnDrawGizmos()
    {
        if (swordCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(swordCollider.bounds.center, swordCollider.bounds.size);
        }
    }
}