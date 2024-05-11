using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyExtensions.TransformExtension
{
    public static class TransformExtension
    {
        public static void ResetTransformation(this Transform transform)
        {
            ResetPosition(transform);
            ResetRotation(transform);
            ResetScale(transform);
        }
        public static void ResetPosition(this Transform transform)
        {
            transform.position = Vector3.zero;
        }
        public static void ResetRotation(this Transform transform)
        {
            transform.localRotation = Quaternion.identity;
        }
        public static void ResetScale(this Transform transform)
        {
            transform.localScale = Vector3.one;
        }
        public static void DestroyChilds(this Transform transform)
        {
            foreach (Transform child in transform)
            {
                if (child == transform) continue;
                UnityEngine.Object.Destroy(child.gameObject);
            }
        }
        public static void SetActiveChilds(this Transform transform, bool activate)
        {
            foreach (Transform child in transform)
            {
                if (child == transform) continue;

                child.gameObject.SetActive(activate);
            }
        }
    }
}
