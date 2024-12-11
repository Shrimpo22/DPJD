using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLastSiblings : MonoBehaviour
{
    public GameObject objectA;  // Reference to object A
    public GameObject objectB;  // Reference to object B
    public GameObject parentObject; // Reference to the parent object (Object 1)

    void LateUpdate()
    {
        // Set the order: move A and then B to the end in the correct order
        MoveToLastSiblingInOrder(objectA.transform, objectB.transform);
    }

    public void MoveToLastSiblingInOrder(Transform a, Transform b)
    {
        // Make sure the parent is set correctly
        if (a.parent != b.parent)
        {
            Debug.LogError("Objects do not share the same parent.");
            return;
        }

        // First, move object A to the end
        a.SetAsLastSibling();

        // Then, move object B to the end after object A
        b.SetAsLastSibling();
    }
}
