using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSpawnArea
{
    public Vector3 AreaCenter;
    [Min(0f)] public Vector3 AreaSize;
    [Header("If it is not true ignore DeadZone variables.")]
    public bool EnableDeadZone;
    public Vector3 DeadZoneCenter;
    [Min(0f)] public Vector3 DeadZoneSize;


#if UNITY_EDITOR
    // Custom inspector validation
    void OnValidate()
    {
        // Ensure DeadZone is always within Area
        DeadZoneCenter.x = Mathf.Clamp(DeadZoneCenter.x, AreaCenter.x - AreaSize.x / 2f, AreaCenter.x + AreaSize.x / 2f);
        DeadZoneCenter.y = Mathf.Clamp(DeadZoneCenter.y, AreaCenter.y - AreaSize.y / 2f, AreaCenter.y + AreaSize.y / 2f);
        DeadZoneCenter.z = Mathf.Clamp(DeadZoneCenter.z, AreaCenter.z - AreaSize.z / 2f, AreaCenter.z + AreaSize.z / 2f);

        DeadZoneSize.x = Mathf.Clamp(DeadZoneSize.x, 0f, AreaSize.x);
        DeadZoneSize.y = Mathf.Clamp(DeadZoneSize.y, 0f, AreaSize.y);
        DeadZoneSize.z = Mathf.Clamp(DeadZoneSize.z, 0f, AreaSize.z);
    }
#endif
    public Vector3 GetRandomSpawnPoint()
    {
        Vector3 randomPoint;

        if (!EnableDeadZone || DeadZoneSize == Vector3.zero)
        {
            randomPoint = new Vector3(
                Random.Range(AreaCenter.x - AreaSize.x / 2f, AreaCenter.x + AreaSize.x / 2f),
                Random.Range(AreaCenter.y - AreaSize.y / 2f, AreaCenter.y + AreaSize.y / 2f),
                Random.Range(AreaCenter.z - AreaSize.z / 2f, AreaCenter.z + AreaSize.z / 2f));

        }
        else
        {
            // Precaution for infinite loop.
            int attemptCount = 0;
            do
            {
                randomPoint = new Vector3(
                    Random.Range(AreaCenter.x - AreaSize.x / 2f, AreaCenter.x + AreaSize.x / 2f),
                    Random.Range(AreaCenter.y - AreaSize.y / 2f, AreaCenter.y + AreaSize.y / 2f),
                    Random.Range(AreaCenter.z - AreaSize.z / 2f, AreaCenter.z + AreaSize.z / 2f));
                attemptCount++;
            } while (IsInsideDeadZone(randomPoint) || attemptCount > 10);
        }

        return randomPoint;
    }

    private bool IsInsideDeadZone(Vector3 point)
    {
        float minX = DeadZoneCenter.x - DeadZoneSize.x / 2f;
        float maxX = DeadZoneCenter.x + DeadZoneSize.x / 2f;
        float minY = DeadZoneCenter.y - DeadZoneSize.y / 2f;
        float maxY = DeadZoneCenter.y + DeadZoneSize.y / 2f;
        float minZ = DeadZoneCenter.z - DeadZoneSize.z / 2f;
        float maxZ = DeadZoneCenter.z + DeadZoneSize.z / 2f;

        return point.x >= minX && point.x <= maxX && point.y >= minY && point.y <= maxY && point.z >= minZ && point.z <= maxZ;
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(AreaCenter, AreaSize);

        if (EnableDeadZone)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(DeadZoneCenter, DeadZoneSize);
        }
    }
}
