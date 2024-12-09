using System.Collections;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrowPrefab; // Assign the arrow prefab in the Inspector
    public Transform targetDirection; // The transform that defines the direction
    public GameObject arrowTransform;
    private GameObject arrowProjectile;

    public float arrowSpeed = 20f; // Speed of the arrow

    public void Start(){
        targetDirection = FindChildRecursive(GameObject.FindGameObjectWithTag("Player").transform, "mixamorig:Spine2");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ShootArrow();
        }
    }

    void EnableArrow(){
        arrowProjectile = Instantiate(arrowPrefab, arrowTransform.transform.position, arrowTransform.transform.rotation);
        arrowProjectile.transform.parent = arrowTransform.transform.parent;
    }

    void ShootArrow()
    {
        arrowProjectile.transform.parent = null;
        // Set the arrow to look in the direction of the target direction
        arrowProjectile.transform.LookAt(targetDirection);

        // Get the Rigidbody component (assuming the arrow has one)
        Rigidbody rb = arrowProjectile.GetComponentInChildren<Rigidbody>();

        if (rb != null)
        {
            // Apply force in the forward direction of the arrow to launch it
            rb.velocity = arrowProjectile.transform.forward * arrowSpeed;
            StartCoroutine(activateCollider());
        }
        else
        {
            Debug.LogWarning("Arrow prefab needs a Rigidbody component for movement.");
        }
    }

    void OnDrawGizmos(){
        if(arrowProjectile){
            Gizmos.color = Color.green;
            Gizmos.DrawLine(arrowProjectile.transform.position, arrowProjectile.transform.forward.normalized);
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(arrowProjectile.transform.position, targetDirection.transform.position);
        }
    }

    Transform FindChildRecursive(Transform parent, string childName)
    {
        foreach (Transform child in parent)
        {
            if (child.name == childName)
                return child;

            Transform result = FindChildRecursive(child, childName);
            if (result != null)
                return result;
        }
        return null;
    }

    IEnumerator activateCollider(){
        yield return new WaitForSeconds(0.03f);
        Debug.Log("aCTIVATED");
        arrowProjectile.GetComponentInChildren<BoxCollider>().enabled = true;
        //arrowProjectile.GetComponentInChildren<Rigidbody>().useGravity = true;
    }
}
