using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.MeshDeformation
{
    [RequireComponent(typeof(MeshFilter))]
    public class MeshDeformationComponent : MonoBehaviour
    {
        private Mesh deformingMesh;
        private Vector3[] originalVertices, displacedVertices, vertexVelocities;
        public float springForce = 20f;
        public float damping = 5f;
        private MeshCollider _meshCollider;
        void Start()
        {
            deformingMesh = GetComponent<MeshFilter>().mesh;
            _meshCollider = GetComponent<MeshCollider>();
            originalVertices = deformingMesh.vertices;
            displacedVertices = new Vector3[originalVertices.Length];
            for (int i = 0; i < originalVertices.Length; i++)
            {
                displacedVertices[i] = originalVertices[i];
            }
            vertexVelocities = new Vector3[originalVertices.Length];
        }

        void UpdateCollider()
        {
            _meshCollider.sharedMesh = deformingMesh;
        }
        void Update()
        {
            for (int i = 0; i < displacedVertices.Length; i++)
            {
                UpdateVertex(i);
            }
            deformingMesh.vertices = displacedVertices;
            deformingMesh.RecalculateNormals();
        }
        void UpdateVertex(int i)
        {
            Vector3 velocity = vertexVelocities[i];
            Vector3 displacement = displacedVertices[i] - originalVertices[i];
            velocity -= displacement * springForce * Time.deltaTime;
            velocity *= 1f - damping * Time.deltaTime;
            vertexVelocities[i] = velocity;
            displacedVertices[i] += velocity * Time.deltaTime;
            UpdateCollider();
        }
        public void AddDeformingForce(Vector3 point, float force)
        {
            point = transform.InverseTransformPoint(point);
            for (int i = 0; i < displacedVertices.Length; i++)
            {
                AddForceToVertex(i, point, force);
            }
        }

        void AddForceToVertex(int i, Vector3 point, float force)
        {
            Vector3 pointToVertex = displacedVertices[i] - point;
            float attenuatedForce = force / (1f + pointToVertex.sqrMagnitude);
            float velocity = attenuatedForce * Time.deltaTime;
            vertexVelocities[i] += pointToVertex.normalized * velocity;
        }
    }

}
