using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPUInstancing
{
    public class GPUInstanceComponent : MonoBehaviour
    {
        [Header("Check true, If you want to GetComponents from this object.\n" +
        "If it is false, you must provide the variables manually.")]
        [SerializeField] private bool _isThisObject;
        [SerializeField] private Transform _instanceTransform;
        [SerializeField] private Mesh _instanceMesh;
        [Tooltip("Don't forget to set the material's 'Enabled GPU Instancing' variable to true.")]
        [SerializeField] private Material[] _instanceMaterial;
        private InstanceData _instanceData;

        private void Awake()
        {
            if (_isThisObject)
            {
                _instanceTransform = transform;
            }

            if (_instanceTransform.TryGetComponent(out MeshFilter meshFilter))
            {
                _instanceMesh = meshFilter.sharedMesh;
            }
            else
            {
                Debug.LogError("Mesh Filter not found!");
                return;
            }

            if (_instanceTransform.TryGetComponent(out MeshRenderer meshRenderer))
            {
                meshRenderer.enabled = false;
                _instanceMaterial = meshRenderer.sharedMaterials;
            }
            else
            {
                Debug.LogError("Mesh renderer not found!");
                return;
            }
            _instanceData = new InstanceData(_instanceTransform);
        }

        private void Update()
        {
            if (_instanceTransform.hasChanged)
            {
                SendData();
            }
        }

        private void SendData()
        {

        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}

