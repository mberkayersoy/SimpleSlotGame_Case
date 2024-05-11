using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.MeshDeformation
{
    public class MeshDeformer : MonoBehaviour
    {
        public float force = 10f;
        public float forceOffset = 0.1f;
        //void Update()
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        HandleInput();
        //    }
        //}

        //void HandleInput()
        //{
        //    Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(inputRay, out hit))
        //    {
        //        MeshDeformationComponent deformer = hit.collider.GetComponent<MeshDeformationComponent>();
        //        if (deformer)
        //        {
        //            Vector3 point = hit.point;
        //            point += hit.normal * forceOffset;
        //            deformer.AddDeformingForce(point, force);
        //        }
        //    }
        //}

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out MeshDeformationComponent deform))
            {
                Vector3 point = collision.contacts[0].point + collision.contacts[0].normal * forceOffset;
                deform.AddDeformingForce(point, force);
            }
        }
    }
}

