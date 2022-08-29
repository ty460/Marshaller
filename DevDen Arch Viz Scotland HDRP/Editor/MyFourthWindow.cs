using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MyFourthWindow : EditorWindow
{

    MyFourthWindow()
    {
        this.titleContent = new GUIContent("Marshaller");

    }
    [MenuItem("MyTool/Rotating Layout/Marshaller &r")]
    static void ShowWindows()
    {
        EditorWindow.GetWindow(typeof(MyFourthWindow));

    }
    //string bugReportName = "";
    int planetNum = 0;
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
        planetNum = EditorGUILayout.IntField("Enter Quantity: ", planetNum);

        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Turn On Range Display"))
        {
            GameObject.FindWithTag("Star").AddComponent(typeof(StarRange));
            GameObject.FindWithTag("Planet2").AddComponent(typeof(TargetExample));
        }
        GUILayout.Space(10);
        if (GUILayout.Button("Turn Off Range Display"))
        {
            DestroyImmediate(GameObject.FindWithTag("Star").GetComponent<StarRange>());

            foreach (Transform child in GameObject.FindWithTag("Star").transform)
            {
                DestroyImmediate(child.GetComponent<TargetExample>());
            }
            
            
            
        }
            
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        if (GUILayout.Button("Generate"))
        {

            RotaMarshal();


            planetNum = 0;
        }
        Vector3 planetVec = GameObject.FindWithTag("Planet1").transform.position;

        GameObject.FindWithTag("Planet1").transform.position = planetVec;
        GUILayout.Space(20);



        if (GUILayout.Button("Rotation Symmetry"))
        {
            RotaSymmetry();
        }

        GUILayout.Space(10);
        if (GUILayout.Button("Release Tag"))
        {
            DestroyImmediate(GameObject.FindWithTag("Star").GetComponent<StarRange>());
            ReleaseTag();
            StarRange.aaa = true;
        }
        GUILayout.Space(10);
        GUILayout.Space(10);
        if (GUILayout.Button("Clear Cache"))
        {
            StarRange.aaa = true;
        }
        GUILayout.Space(10);
    }

    private void RotaMarshal()
    {
        Vector3 starSymm1 = new Vector3();
        Vector3 starVec = starObject.transform.position;
        starSymm1.x = 2 * planetObject.transform.position.x - starVec.x;
        starSymm1.y = planetObject.transform.position.y;
        starSymm1.z = 2 * planetObject.transform.position.z - starVec.z;
        GameObject.FindWithTag("Planet1").transform.LookAt(starSymm1);
        float distance = (float)Math.Pow(Math.Pow(planetObject.transform.localPosition.x, 2) + Math.Pow(planetObject.transform.localPosition.z, 2), 0.5);
        Quaternion angles = Quaternion.identity;
        Vector3 rotation = new Vector3(0, 180, 0);
        angles.eulerAngles += rotation;
        Vector3 planet2Vec = new Vector3();
        double angle1 = Math.Acos(Vector3.Dot(planetObject.transform.localPosition, Vector3.right) / distance);
        for (int i = 0; i < planetNum - 1; i++)
        {
            double newAngle = angle1 + (i + 1) * (2 * Math.PI / planetNum);
            planet2Vec.x = (float)(distance * Math.Cos(newAngle));
            planet2Vec.y = planetObject.transform.localPosition.y;
            planet2Vec.z = (float)(distance * Math.Sin(newAngle));
            Vector3 worldPlanet2Vec = starObject.transform.TransformPoint(planet2Vec);
            GameObject planet2 = Instantiate(planetObject, worldPlanet2Vec, angles, starObject.transform);
            Vector3 starSymm2 = new Vector3();
            starSymm2.x = 2 * worldPlanet2Vec.x - starVec.x;
            starSymm2.y = worldPlanet2Vec.y;
            starSymm2.z = 2 * worldPlanet2Vec.z - starVec.z;
            planet2.transform.LookAt(starSymm2);
            planet2.tag = "Planet2";
            planet2.AddComponent(typeof(TargetExample));
            planet2 = null;
        }
       // Debug.Log("RotaMarshal Completed");
        
    }
    
    private void RotaSymmetry()
    {

        Vector3 starSymm1 = new Vector3();
        Vector3 starVec = starObject.transform.position;
        starSymm1.x = 2 * planetObject.transform.position.x - starVec.x;
        starSymm1.y = planetObject.transform.position.y;
        starSymm1.z = 2 * planetObject.transform.position.z - starVec.z;
        GameObject.FindWithTag("Planet1").transform.LookAt(starSymm1);
        

    }
    private void ReleaseTag()
    {
        foreach (Transform child in GameObject.FindWithTag("Star").transform)
        {
            child.tag = "Planet";
        }
        
        GameObject.FindWithTag("Star").tag = "Planet";
        starObject = null;
        planetObject = null;
        
    }
}

//private void CreatBugText()
//{
//    Directory.CreateDirectory("Assets/BugReports/" + bugReportName);
//    StreamWriter sw = new StreamWriter("Assets/BugReports/" + bugReportName + "/" + bugReportName + ".txt");
//    sw.WriteLine(bugReportName);
//    sw.WriteLine(System.DateTime.Now.ToString());
//    sw.WriteLine(EditorSceneManager.GetActiveScene().name);
//    sw.WriteLine(bugDes);
//    sw.Close();

//    Debug.Log("生成了报告文本");
//}
//private void CreatBugTextAndImg()
//{
//    Debug.Log("生成了报告文本和截图");
//}