using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    private static float _minMovementValue = 0.2f;
    public static void ResetTransformation(this Transform transform)
    {
        transform.position = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = new Vector3(1, 1, 1);
    }

    public static void SetParentNull(this Transform transform)
    {
        transform.SetParent(null);
    }

    public static bool CheckMovementValue(Vector3 movement)
    {
        return !(movement.sqrMagnitude > _minMovementValue );
    }
    
}
