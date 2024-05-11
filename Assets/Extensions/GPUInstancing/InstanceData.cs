using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GPUInstancing
{
    [System.Serializable]
    public class InstanceData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;
        public Matrix4x4 Matrix;
        public InstanceData(Transform instanceTransform)
        {
            Position = instanceTransform.position;
            Rotation = instanceTransform.rotation;
            Scale = instanceTransform.lossyScale;
            Matrix = Matrix4x4.TRS(Position, Rotation, Scale);
        }
    }

}
