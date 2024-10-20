using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Transform target;
    public Image foregroundImage;
    public Image backgroundImage;
    public Vector3 offset;
    public Transform headTarget;
    public Transform enemyTarget;
    public LayerMask ignoreLayer;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = (target.position - Camera.main.transform.position).normalized;
        foregroundImage.enabled = IsEnemyVisible() && !IsEnemyOccluded();
        backgroundImage.enabled = IsEnemyVisible() && !IsEnemyOccluded();
        transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
    }

    public void SetHealthBarPercentage(float percentage){
        Debug.Log("Percentage: " + percentage);
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percentage;
        foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }

    bool IsEnemyVisible()
    {
        Vector3 viewportPoint = Camera.main.WorldToViewportPoint(headTarget.position);

        // Check if the enemy's position is within the camera's viewport
        return (viewportPoint.x >= 0 && viewportPoint.x <= 1 &&
                viewportPoint.y >= 0 && viewportPoint.y <= 1 &&
                viewportPoint.z >= 0);  // Z must be positive, in front of the camera
    }

    // Check if there's an obstacle between the camera and the enemy
    bool IsEnemyOccluded()
    {
        Vector3 directionToEnemy = enemyTarget.position -  Camera.main.transform.position;
        float distanceToEnemy = Vector3.Distance( Camera.main.transform.position, enemyTarget.position);

        // Cast a ray from the camera to the enemy
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, directionToEnemy, out hit, distanceToEnemy, ~ignoreLayer))
        {
            // If the ray hits something other than the enemy, it's occluded
            if (hit.transform != enemyTarget)
            {
                return true;  // Enemy is occluded by another object
            }
        }
        return false;
    }

    void OnDrawGizmos(){
        Vector3 a = target.position - Camera.main.transform.position.normalized;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(Camera.main.transform.position, a);

    }
}
