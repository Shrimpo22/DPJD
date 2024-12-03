using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackTrap : MonoBehaviour
{
    public GameObject bearTrap;
    public Transform trapSpawnPoint;
    public Transform groundTarget;
    public float launchForce = 5f;
    public Rigidbody rb;
    private GameObject currentTrap; 


    private void Start()
    {
        rb = bearTrap.GetComponent<Rigidbody>();

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ActivateTrap();
            LaunchTrap();
        }


    }
    public void ActivateTrap()
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
    }

    public void LaunchTrap()
    {
        currentTrap.transform.parent = null;
        currentTrap.transform.Find("beartrappainted/Cylinder").GetComponent<Collider>().enabled = true;
        currentTrap.GetComponent<Collider>().enabled = true;

        rb.isKinematic = false;

        // Calcula a direção do lançamento
        Vector3 direction = (groundTarget.position - trapSpawnPoint.position).normalized;
        Vector3 launchDirection = direction + Vector3.up * 0.3f;
        rb.AddForce(launchDirection * launchForce, ForceMode.Impulse);

    }
}
