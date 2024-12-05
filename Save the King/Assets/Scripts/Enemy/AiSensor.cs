using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

[ExecuteInEditMode]
public class AiSensor : MonoBehaviour
{
    public bool drawSensor;
    public AiAgent agent;
    public float maxSightDistance = 0;
    public float angle = 0;
    public float height = 0;
    public float verticalAngle = 30f;
    public Color meshColor;
    public bool drawSight = false;

    private GameObject playerOnSensor;

    public int scanFrequency = 30;
    public LayerMask layers;
    public LayerMask occlusionLayers;
    [SerializeField]
    public List<GameObject> Objects
    {
        get
        {
            objects.RemoveAll(obj => !obj);
            return objects;
        }
    }
    private List<GameObject> objects = new List<GameObject>();
    Mesh mesh;

    float scanInterval;
    float scanTimer;

    // Start is called before the first frame update
    void Start()
    {
        agent = transform.parent.gameObject.GetComponent<AiAgent>();
        maxSightDistance = agent.config.maxSightDistance;
        angle = agent.config.angle;
        height = agent.config.height;
        meshColor = agent.config.meshColor;


        scanInterval = 1.0f / scanFrequency;

    }

    // Update is called once per frame
    void Update()
    {
        scanTimer -= Time.deltaTime;
        if (scanTimer < 0)
        {
            scanTimer += scanInterval;
            Scan();
        }
    }

    public void Scan()
    {
        objects.Clear();
        if (playerOnSensor == null) return;
        if (IsInSight(playerOnSensor))
        {
            objects.Add(playerOnSensor);
        }
    }

    public bool IsInSight(GameObject obj)
    {

        Vector3 origin = transform.position;

        CharacterController characterController = obj.GetComponent<CharacterController>();
        if (characterController == null) return false;

        // Get the center, top, and bottom points of the CharacterController's capsule
        Vector3 playerCenter = obj.transform.position + characterController.center;
        Vector3 topPoint = playerCenter + Vector3.up * (characterController.height / 2 - characterController.radius);
        Vector3 bottomPoint = playerCenter - Vector3.up * (characterController.height / 2 - characterController.radius);

        // Define check points for the line of sight check
        Vector3[] checkPoints = new Vector3[5];
        checkPoints[0] = bottomPoint; // Bottom center
        checkPoints[1] = playerCenter; // Center
        checkPoints[2] = topPoint;     // Top center
        checkPoints[3] = bottomPoint + obj.transform.right * characterController.radius; // Bottom side
        checkPoints[4] = topPoint + obj.transform.right * characterController.radius; // Top side

        origin.y += height*3/4; // Adjust to enemy's eye level

        foreach (var point in checkPoints)
        {
            // Check if there's a clear line of sight to each point
            bool isVisible = !Physics.Linecast(origin, point, occlusionLayers);

            // Draw the line with a color indicating visibility (green = visible, red = obstructed)
            if (drawSight)
                Debug.DrawLine(origin, point, isVisible ? Color.green : Color.red);

            // If any of the points is visible, set the flag to true
            if (isVisible)
            {
                return true;
            }
        }

        return false;
    }

    Mesh CreateWedgeMesh()
    {
        Mesh mesh = new Mesh();
        int segments = 10;

        int numTriangles = (segments * 8) + 4 + 4; // Adjust for extra triangles
        int numVertices = numTriangles * 3;

        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numVertices];

        Vector3 topCenter = Vector3.zero;
        Vector3 bottomCenter = topCenter + Vector3.up * height;

        // Calculate initial corners for vertical splay
        Vector3 bottomLeft = Quaternion.Euler(-verticalAngle, -angle, 0) * Vector3.forward * maxSightDistance;
        Vector3 bottomRight = Quaternion.Euler(-verticalAngle, angle, 0) * Vector3.forward * maxSightDistance;

        Vector3 topLeft = Quaternion.Euler(verticalAngle, -angle, 0) * Vector3.forward * maxSightDistance + Vector3.up * height;
        Vector3 topRight = Quaternion.Euler(verticalAngle, angle, 0) * Vector3.forward * maxSightDistance + Vector3.up * height;

        int vert = 0;

        // Left side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = bottomLeft;
        vertices[vert++] = topLeft;

        vertices[vert++] = topLeft;
        vertices[vert++] = topCenter;
        vertices[vert++] = bottomCenter;

        // Right side
        vertices[vert++] = bottomCenter;
        vertices[vert++] = topCenter;
        vertices[vert++] = topRight;

        vertices[vert++] = topRight;
        vertices[vert++] = bottomRight;
        vertices[vert++] = bottomCenter;

        float currentAngle = -angle;
        float deltaAngle = (angle * 2) / segments;

        for (int i = 0; i < segments; i++)
        {
            // Bottom corners for the current and next segment
            Vector3 segmentBottomLeft = Quaternion.Euler(-verticalAngle, currentAngle, 0) * Vector3.forward * maxSightDistance;
            Vector3 segmentBottomRight = Quaternion.Euler(-verticalAngle, currentAngle + deltaAngle, 0) * Vector3.forward * maxSightDistance;

            // Top corners for the current and next segment
            Vector3 segmentTopLeft = Quaternion.Euler(verticalAngle, currentAngle, 0) * Vector3.forward * maxSightDistance + Vector3.up * height;
            Vector3 segmentTopRight = Quaternion.Euler(verticalAngle, currentAngle + deltaAngle, 0) * Vector3.forward * maxSightDistance + Vector3.up * height;

            // Far side (vertical face)
            vertices[vert++] = segmentBottomLeft;
            vertices[vert++] = segmentBottomRight;
            vertices[vert++] = segmentTopRight;

            vertices[vert++] = segmentTopRight;
            vertices[vert++] = segmentTopLeft;
            vertices[vert++] = segmentBottomLeft;

            // Top face
            vertices[vert++] = topCenter;
            vertices[vert++] = segmentTopLeft;
            vertices[vert++] = segmentTopRight;

            // Bottom face
            vertices[vert++] = bottomCenter;
            vertices[vert++] = segmentBottomRight;
            vertices[vert++] = segmentBottomLeft;

            currentAngle += deltaAngle;
        }

        for (int i = 0; i < numVertices; i++)
        {
            triangles[i] = i;
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        MeshCollider meshCollider = gameObject.GetOrAddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        meshCollider.convex = true; // Set this to true if you want it to interact with other colliders as a trigger
        meshCollider.isTrigger = true;
        return mesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        Debug.Log(other.name + " entered sensor");
        playerOnSensor = other.gameObject;

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Player") return;
        Debug.Log(other.name + " exited sensor");
        playerOnSensor = null;
    }

    private void OnValidate()
    {
        mesh = CreateWedgeMesh();
        scanInterval = 1.0f / scanFrequency;

    }

    private void OnDrawGizmos()
    {
        if (drawSensor)
            if (mesh)
            {
                Gizmos.color = meshColor;
                Gizmos.DrawMesh(mesh, transform.position, transform.rotation);

                Gizmos.color = Color.yellow;

                // Draw a small sphere at each vertex
                Vector3[] vertices = mesh.vertices;
                for (int i = 0; i < vertices.Length; i++)
                {
                    // Transform the vertex position from local to world space
                    Vector3 worldPosition = transform.TransformPoint(vertices[i]);
                    Gizmos.DrawSphere(worldPosition, 0.05f); // Adjust the sphere size as needed
                }
            }
    }
}
