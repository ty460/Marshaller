using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TargetExample : MonoBehaviour
{
    private void Update()
    {
        
    }
}
public class GizmosTest1
{
    [DrawGizmo(GizmoType.Active | GizmoType.NonSelected)]
    private static void MyCustomOnDrawGizmos(TargetExample target, GizmoType gizmoType)
    {
        var color = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.transform.position, 0.7f);
        Gizmos.color = color;
    }
}