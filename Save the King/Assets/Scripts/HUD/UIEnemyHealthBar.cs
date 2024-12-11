using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Transform target; // The current target
    public Image foregroundImage;
    public Image backgroundImage;
    public Vector3 offset; // Offset for non-boss health bars
    public Transform headTarget;
    public Transform enemyTarget;
    public LayerMask ignoreLayer;
    public bool isBoss; // Flag to check if the target is a boss

    void LateUpdate()
    {
        // if (!isBoss && transform.parent != GameObject.FindGameObjectWithTag("HUD").transform)
        //      transform.parent = GameObject.FindGameObjectWithTag("HUD").transform;
        if (isBoss)
        {
            PositionBossHealthBar();
        }
        else
        {
            PositionEnemyHealthBar();
        }
    }

    void PositionEnemyHealthBar()
    {
        try
        {
            float magnitude = Vector3.Distance(target.position, Camera.main.transform.position);
            if (magnitude <= 10)
            {
                bool isVisible = IsEnemyVisible() && !IsEnemyOccluded();
                foregroundImage.enabled = isVisible;
                backgroundImage.enabled = isVisible;
                GetComponent<RectTransform>().localScale = Vector3.one;


                if (isVisible)
                {
                    transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
                }
            }
            else
            {
                foregroundImage.enabled = false;
                backgroundImage.enabled = false;
            }
        }
        catch (Exception)
        {
            return;
        }
    }

    void PositionBossHealthBar()
    {
        // Always display the boss health bar at the top-center of the screen
        foregroundImage.enabled = true;
        backgroundImage.enabled = true;
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Set the anchor to the bottom-center
        rectTransform.anchorMin = new Vector2(0.5f, 0f); // Bottom-left corner of the anchor
        rectTransform.anchorMax = new Vector2(0.5f, 0f); // Top-right corner of the anchor

        // Set the pivot to the bottom-center
        rectTransform.pivot = new Vector2(0.5f, 0f); // Centered horizontally and at the bottom vertically

        // Optional: Adjust position relative to the anchor
        rectTransform.anchoredPosition = new Vector2(0f, 50f);
        rectTransform.localScale = new Vector3(5.08892584f, 1.7593323f, 1.82787776f);
    }

    public void SetHealthBarPercentage(float percentage)
    {
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

    bool IsEnemyOccluded()
    {
        Vector3 directionToEnemy = enemyTarget.position - Camera.main.transform.position;
        float distanceToEnemy = Vector3.Distance(Camera.main.transform.position, enemyTarget.position);

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

    void OnDrawGizmos()
    {
        Vector3 a = target.position - Camera.main.transform.position.normalized;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(Camera.main.transform.position, a);
    }
}
