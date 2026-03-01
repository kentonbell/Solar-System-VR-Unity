using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshCollider))]
public class DeformingMesh : MonoBehaviour
{
    public MeshFilter filter;         // Called 'filter' in slides
    public MeshCollider meshCol;      // Mesh collider used for collision update

    private Vector3[] mVertices;      // Mesh vertices
    private Vector3[] originalVertices; // Original unmodified vertices
    private Vector3[] mNormals;       // Normals at each vertex
    private Color32[] mColors;        // Per-vertex color array for crater coloring

    public float deformationRadius = 0.5f;     // Radius around the impact point
    public float deformationFactorScale = 0.05f; // Used to scale impact force

    void Start()
    {
        // Get MeshFilter and MeshCollider
        filter = GetComponent<MeshFilter>();
        meshCol = GetComponent<MeshCollider>();

        // Get vertices and normals from mesh
        mVertices = filter.mesh.vertices;
        originalVertices = filter.mesh.vertices;
        mNormals = filter.mesh.normals;

        // Initialize all vertex colors to transparent black
        mColors = new Color32[mVertices.Length];
        for (int i = 0; i < mColors.Length; i++)
        {
            Color32 myColor = new Color32(0, 0, 0, 0);
            mColors[i] = myColor;
        }

        // Apply colors to mesh
        filter.mesh.colors32 = mColors;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Get collision contact point and impulse
        Vector3 hitPoint = collision.contacts[0].point;  // In world space
        Vector3 deformDirection = -collision.impulse.normalized; // Impact direction
        float deformMagnitude = collision.impulse.magnitude;     // Impact strength

        // Traverse all vertices
        for (int i = 0; i < mVertices.Length; i++)
        {
            // Convert vertex to world space
            Vector3 worldVertex = filter.transform.TransformPoint(mVertices[i]);

            // Measure distance from hit point
            float distance = Vector3.Distance(worldVertex, hitPoint);

            // If within radius, apply deformation
            if (distance < deformationRadius)
            {
                float falloff = 1 - (distance / deformationRadius); // Optional falloff

                // Deform vertex in direction of impulse
                mVertices[i] += deformDirection * deformMagnitude * deformationFactorScale * falloff;

                // Color deformed vertex to mark crater (solid white)
                Color32 myColor = new Color32(255, 255, 255, 255);
                mColors[i] = myColor;
            }
        }

        // Apply updates to mesh
        filter.mesh.vertices = mVertices;
        filter.mesh.colors32 = mColors;
        filter.mesh.RecalculateNormals();
        filter.mesh.RecalculateBounds();

        // Update mesh collider
        meshCol.sharedMesh = null;
        meshCol.sharedMesh = filter.mesh;
    }
}
