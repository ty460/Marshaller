using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class StarRange : MonoBehaviour
{
    public static bool aaa = true;
    private void Update()
    {

    }
}
public class GizmosTest2
{
    [DrawGizmo(GizmoType.Active | GizmoType.NonSelected)]
    private static void OnDrawGizmos(StarRange target, GizmoType gizmoType)
    {
        float starX = GameObject.FindWithTag("Star").GetComponent<Renderer>().bounds.size.x;
        float starZ = GameObject.FindWithTag("Star").GetComponent<Renderer>().bounds.size.z;
        var color = Gizmos.color;
        
        Vector3 vector2 = new Vector3(starX*0.65f, 0, starZ * 1.67f);
        Vector3 vector3 = new Vector3(starX * 0.65f, 0, -starZ * 1.67f);
        Vector3 vector4 = new Vector3(-starX * 0.65f, 0, starZ * 1.67f);
        Vector3 vector5 = new Vector3(-starX * 0.65f, 0, -starZ * 1.67f);
        Vector3 vector1 = new Vector3(0.26f * starX, 0, starZ * 1.67f);
        Vector3 vector6 = new Vector3(0.26f * starX, 0, -starZ * 1.67f);
        Vector3 vector7 = new Vector3(-0.26f * starX, 0, -starZ * 1.67f);
        Vector3 vector8 = new Vector3(-0.26f * starX, 0, starZ * 1.67f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(target.transform.position + vector2, target.transform.position + vector3);
        Gizmos.DrawLine(target.transform.position + vector3, target.transform.position + vector5);
        Gizmos.DrawLine(target.transform.position + vector5, target.transform.position + vector4);
        Gizmos.DrawLine(target.transform.position + vector4, target.transform.position + vector2);
        Gizmos.color = color;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(target.transform.position + vector1, target.transform.position + vector6);
        Gizmos.DrawLine(target.transform.position + vector6, target.transform.position + vector7);
        Gizmos.DrawLine(target.transform.position + vector7, target.transform.position + vector8);
        Gizmos.DrawLine(target.transform.position + vector8, target.transform.position + vector1);
        Gizmos.color = color;
    }
}