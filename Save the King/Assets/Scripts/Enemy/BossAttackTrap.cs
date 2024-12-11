using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;

public class BossAttackTrap : MonoBehaviour
{
    public GameObject bearTrap;
    public Transform trapSpawnPoint;
    public Transform groundTarget;
    public float launchForce = 2f;
    public Rigidbody rb;
    private GameObject currentTrap;
    public List<GameObject> activeTraps = new List<GameObject>();


    private void Start()
    {
        rb = bearTrap.GetComponent<Rigidbody>();

    }
    public void ActivateTrap()
    {
        if ((activeTraps.Count <= 4))
        {
            currentTrap = Instantiate(bearTrap, trapSpawnPoint.position, trapSpawnPoint.rotation);
            rb = currentTrap.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            currentTrap.SetActive(true);
            currentTrap.transform.position = trapSpawnPoint.position;
            currentTrap.transform.rotation = trapSpawnPoint.rotation;
            currentTrap.transform.parent = trapSpawnPoint;
            currentTrap.transform.Find("beartrappainted/Cylinder").GetComponent<Collider>().enabled = false;
            currentTrap.GetComponent<Collider>().enabled = false;
            activeTraps.Add(currentTrap);
            if (activeTraps.Count >= 3)
                StartCoroutine(DestroyTrapAfterDelay(activeTraps[0], 10f));
        }
    }

    public void LaunchTrap()
    {
        currentTrap.transform.parent = null;
        currentTrap.transform.Find("beartrappainted/Cylinder").GetComponent<Collider>().enabled = true;
        currentTrap.GetComponent<Collider>().enabled = true;

        rb.isKinematic = false;
        rb.mass = 100;

        currentTrap.transform.rotation = Quaternion.Euler(0, 0, 0);
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        Vector3 launchDirection = gameObject.transform.forward * 30f;
        Debug.DrawLine(trapSpawnPoint.position, trapSpawnPoint.position + launchDirection);
        rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);


    }

    public void DeactivateTrap()
    {
        List<GameObject> trapsToRemove = new List<GameObject>();
        foreach (GameObject trap in activeTraps)
        {
            if (trap.transform.parent == trapSpawnPoint)
                trapsToRemove.Add(trap);
        }

        foreach (GameObject trap in trapsToRemove)
        {
            activeTraps.Remove(trap);
            Destroy(trap);
        }
    }

    private IEnumerator DestroyTrapAfterDelay(GameObject trap, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (activeTraps.Contains(trap))
        {
            activeTraps.Remove(trap);
            Destroy(trap);
        }
    }

}
