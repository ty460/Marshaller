using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MyThirdWindow : EditorWindow
{

    MyThirdWindow()
    {
        this.titleContent = new GUIContent("Marshaller - Z plane");

    }
    [MenuItem("MyTool/Symmetrical Layout/Marshaller - Z plane &z")]
    static void ShowWindows()
    {
        EditorWindow.GetWindow(typeof(MyThirdWindow));

    }
    //string bugReportName = "";

    GameObject starObject;
    GameObject planetObject;
    // string bugDes = "";
    private void OnGUI()
    {

        GUILayout.Space(10);
        GUI.skin.label.fontSize = 24;
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Marshaller");
        GUILayout.Space(10);
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("Scene Name:                          " + EditorSceneManager.GetActiveScene().name);

        GUILayout.Space(10);
        GUILayout.Label("Time:                                        " + System.DateTime.Now);
        GUILayout.Space(10);
        starObject = (GameObject)EditorGUILayout.ObjectField("Select a Star Model: ", starObject, typeof(GameObject), true);
        starObject.tag = "Star";
        GUILayout.Space(10);
        planetObject = (GameObject)EditorGUILayout.ObjectField("Select a Planet Model: ", planetObject, typeof(GameObject), true);
        planetObject.tag = "Planet1";


        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Turn On Range Display"))
        {
            GameObject.FindWithTag("Star").AddComponent(typeof(StarRange));
            GameObject.FindWithTag("Planet2").AddComponent(typeof(TargetExample));
            GameObject.FindWithTag("Planet3").AddComponent(typeof(TargetExample));
            GameObject.FindWithTag("Planet4").AddComponent(typeof(TargetExample));
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Turn Off Range Display"))
        {
            DestroyImmediate(GameObject.FindWithTag("Star").GetComponent<StarRange>());
            DestroyImmediate(GameObject.FindWithTag("Planet2").GetComponent<TargetExample>());
            DestroyImmediate(GameObject.FindWithTag("Planet3").GetComponent<TargetExample>());
            DestroyImmediate(GameObject.FindWithTag("Planet4").GetComponent<TargetExample>());
        }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        Vector3 planetVec = GameObject.FindWithTag("Planet1").transform.position;

        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("Transform:");

        GUILayout.BeginHorizontal();
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("X:");
        planetVec.x = EditorGUILayout.Slider(planetVec.x, -20, 20);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("Y:");
        planetVec.y = EditorGUILayout.Slider(planetVec.y, -20, 20);
        GUILayout.EndHorizontal();

        GameObject.FindWithTag("Planet1").transform.position = planetVec;
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Mirror Symmetry"))
        {
            MirrorSymmetry();
           // Debug.Log(GameObject.FindWithTag("Gemini2").transform.position);
           // Debug.Log(GameObject.FindWithTag("Planet1").transform.position);
        }
        if (GUILayout.Button("Central Symmetry"))
        {
            CentroSymmetry();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        if (GUILayout.Button("Release Tag"))
        {
            DestroyImmediate(GameObject.FindWithTag("Star").GetComponent<StarRange>());
            ReleaseTag();
            StarRange.aaa = true;
        }
        GUILayout.Space(10);
        GUILayout.Space(10);
        GUILayout.Space(10);
        GUILayout.Space(10);
        if (GUILayout.Button("Clear Cache"))
        {
            StarRange.aaa = true;
        }
        GUILayout.Space(10);

        GUILayout.Space(10);
        float starX = starObject.GetComponent<Renderer>().bounds.size.x;
        Vector3 starWorVec = starObject.transform.TransformPoint(planetObject.transform.localPosition);
        // Debug.Log(starX);
        float starY = starObject.GetComponent<Renderer>().bounds.size.y;
        // Debug.Log(starZ);
        Vector3 planetWorVec = planetObject.transform.TransformPoint(planetObject.transform.localPosition);
        // Debug.Log("x-: " + (planetWorVec.x - starWorVec.x));
        // Debug.Log("z-: " + (planetWorVec.z - starWorVec.z));
        if (Math.Abs(planetWorVec.x - starWorVec.x) < starX * 0.586f && Math.Abs(planetWorVec.y - starWorVec.y) < starY * 1.51f && Math.Abs(planetWorVec.x - starWorVec.x) > starX * 0.1f && StarRange.aaa == true)
        {
            QuadMarshal();
            StarRange.aaa = false;

        }
        if (Math.Abs(planetWorVec.x - starWorVec.x) < starX * 0.2f && Math.Abs(planetWorVec.y - starWorVec.y) < starY * 1.51f && StarRange.aaa == true)
        {

            BiMarshal();
            StarRange.aaa = false;

        }

    }

    private void BiMarshal()
    {
        starObject.tag = "Star";
        planetObject.tag = "Planet1";
        Vector3 starVec = starObject.transform.position;
        Quaternion angles = Quaternion.identity;
        Vector3 rotation = new Vector3(0, 180, 0);
        angles.eulerAngles += rotation;
        Vector3 planet2Vec = new Vector3(2 * starVec.x - planetObject.transform.position.x, 2 * starVec.y - planetObject.transform.position.y, planetObject.transform.position.z);
        GameObject planet2 = Instantiate(planetObject, planet2Vec, angles, starObject.transform);
        planet2.tag = "Planet2";
        GameObject.FindWithTag("Planet2").AddComponent(typeof(TargetExample));
        //Debug.Log("BiMarshal Completed");
    }
    private void QuadMarshal()
    {
        starObject.tag = "Star";
        planetObject.tag = "Planet1";
        Vector3 starVec = starObject.transform.position;
        Quaternion angles = Quaternion.identity;
        Vector3 rotation = new Vector3(0, 180, 0);
        angles.eulerAngles += rotation;
        Vector3 planetVec2 = new Vector3(2 * starVec.x - planetObject.transform.position.x, 2 * starVec.y - planetObject.transform.position.y, planetObject.transform.position.z);
        GameObject planet2 = Instantiate(planetObject, planetVec2, angles, starObject.transform);
        planet2.tag = "Planet2";
        Vector3 planetVec3 = new Vector3(planetObject.transform.position.x, planetObject.transform.position.y, planet2.transform.position.z);
        GameObject planet3 = Instantiate(planet2, planetVec3, angles, planet2.transform);
        planet3.tag = "Planet3";
        Vector3 planetVec4 = new Vector3(2 * starVec.x - planet3.transform.position.x, 2 * starVec.y - planet3.transform.position.y, planet3.transform.position.z);
        GameObject planet4 = Instantiate(planet3, planetVec4, Quaternion.identity, planet3.transform);
        planet4.tag = "Planet4";
        GameObject.FindWithTag("Planet2").AddComponent(typeof(TargetExample));
        GameObject.FindWithTag("Planet3").AddComponent(typeof(TargetExample));
        GameObject.FindWithTag("Planet4").AddComponent(typeof(TargetExample));
        //Debug.Log("QuadMarshal Completed");
    }

    private void MirrorSymmetry()
    {
        Vector3 starVec = GameObject.FindWithTag("Star").transform.position;
        Vector3 gemini2Vec = new Vector3();
        gemini2Vec.x = GameObject.FindWithTag("Planet1").transform.position.x;
        gemini2Vec.y = 2 * starVec.y - GameObject.FindWithTag("Planet1").transform.position.y;
        gemini2Vec.z = GameObject.FindWithTag("Planet1").transform.position.z;
        GameObject.FindWithTag("Planet2").transform.position = gemini2Vec;
    }
    private void CentroSymmetry()
    {

        Vector3 starVec = GameObject.FindWithTag("Star").transform.position;
        Vector3 planet2Vec = new Vector3();
        planet2Vec.x = 2 * starVec.x - GameObject.FindWithTag("Planet1").transform.position.x;
        planet2Vec.y = 2 * starVec.y - GameObject.FindWithTag("Planet1").transform.position.y;
        planet2Vec.z = GameObject.FindWithTag("Planet1").transform.position.z;
        GameObject.FindWithTag("Planet2").transform.position = planet2Vec;

        Vector3 planet3Vec = new Vector3();
        planet3Vec.x = GameObject.FindWithTag("Planet1").transform.position.x;
        planet3Vec.y = GameObject.FindWithTag("Planet1").transform.position.y;
        planet3Vec.z = GameObject.FindWithTag("Planet2").transform.position.z;
        GameObject.FindWithTag("Planet3").transform.position = planet3Vec;

        Vector3 planet3Rot = new Vector3();
        planet3Rot.x = GameObject.FindWithTag("Planet2").transform.localEulerAngles.x;
        planet3Rot.y = -GameObject.FindWithTag("Planet1").transform.localEulerAngles.y;
        planet3Rot.z = GameObject.FindWithTag("Planet1").transform.localEulerAngles.z;
        GameObject.FindWithTag("Planet3").transform.localEulerAngles = planet3Rot;
        Vector3 planet4Vec = new Vector3();
        planet4Vec.x = 2 * starVec.x - GameObject.FindWithTag("Planet3").transform.position.x;
        planet4Vec.y = 2 * starVec.y - GameObject.FindWithTag("Planet3").transform.position.y;
        planet4Vec.z = GameObject.FindWithTag("Planet3").transform.position.z;

        GameObject.FindWithTag("Planet4").transform.position = planet4Vec;
    }
    private void ReleaseTag()
    {
        if (GameObject.FindWithTag("Star").tag != null)
        {
            GameObject.FindWithTag("Star").tag = "Planet";
        }
        starObject = null;
        if (GameObject.FindWithTag("Planet1").tag != null)
        {
            GameObject.FindWithTag("Planet1").tag = "Planet";
        }
        planetObject = null;
        if (GameObject.FindWithTag("Planet2").tag != null)
        {
            GameObject.FindWithTag("Planet2").tag = "Planet";
        }

        if (GameObject.FindWithTag("Planet3").tag != null)
        {
            GameObject.FindWithTag("Planet3").tag = "Planet";
        }
        if (GameObject.FindWithTag("Planet4").tag != null)
        {
            GameObject.FindWithTag("Planet4").tag = "Planet";
        }

    }


}