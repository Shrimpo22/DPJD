using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    private BoxCollider bx;
    private bool hasHit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bx = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        // Check if the arrow has already hit something to avoid multiple collisions
        if (hasHit) return;

        // Check if the object has a collider (like the character)
        if (collision.collider != null)
        {
            // Stop the arrow's movement
            rb.isKinematic = true; // Disable further physics movement
            bx.enabled = false;

            // Stick the arrow to the hit object by making it a child
            transform.parent = collision.transform;

            // Set the flag to true to prevent further collisions
            hasHit = true;
            if(collision.gameObject.CompareTag("Player Collider")){
                collision.gameObject.GetComponentInParent<PlayerHealth>().TakeDamage(25);
            };
            Destroy(gameObject, 10f);
        }
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward.normalized);
    }
}
