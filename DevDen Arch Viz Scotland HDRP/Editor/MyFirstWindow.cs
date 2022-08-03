using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MyFirstWindow : EditorWindow
{

    MyFirstWindow()
    {
        this.titleContent = new GUIContent("Modle Marshaller");

    }
    [MenuItem("MyTool/Modle Marshaller")]
    static void ShowWindows()
    {
        EditorWindow.GetWindow(typeof(MyFirstWindow));

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
        GUILayout.Label("Modle Marshaller");
        GUILayout.Space(10);
        GUILayout.Space(10);
        GUI.skin.label.fontSize = 12;
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("场景名称:" + EditorSceneManager.GetActiveScene().name);

        GUILayout.Space(10);
        GUILayout.Label("当前时间:                                " + System.DateTime.Now);
        GUILayout.Space(10);
        starObject = (GameObject)EditorGUILayout.ObjectField("选择恒星模型:", starObject, typeof(GameObject), true);
        starObject.tag = "Star";
        GUILayout.Space(10);
        planetObject = (GameObject)EditorGUILayout.ObjectField("选择行星模型:", planetObject, typeof(GameObject), true);
        planetObject.tag = "Planet1";
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("打开显示范围"))
        {
            GameObject.FindWithTag("Star").AddComponent(typeof(StarRange));
        }
        GUILayout.Space(10);
        if (GUILayout.Button("关闭范围显示"))
        {
            DestroyImmediate(GameObject.FindWithTag("Star").GetComponent<StarRange>());
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
        planetVec.x = EditorGUILayout.Slider(planetVec.x, -50, 50);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("Y:");
        planetVec.y = EditorGUILayout.Slider(planetVec.y, -50, 50);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUI.skin.label.alignment = TextAnchor.UpperLeft;
        GUILayout.Label("Z:");
        planetVec.z = EditorGUILayout.Slider(planetVec.z, -50, 50);
        GUILayout.EndHorizontal();

        GameObject.FindWithTag("Planet1").transform.position = planetVec;
        GUILayout.Space(20);

        GUILayout.BeginHorizontal();
       
        if (GUILayout.Button("镜面对称"))
        {
            MirrorSymmetry();
            Debug.Log(GameObject.FindWithTag("Gemini2").transform.position);
            Debug.Log(GameObject.FindWithTag("Planet1").transform.position);
        }
        if (GUILayout.Button("中心对称"))
        {
            CentroSymmetry();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        if (GUILayout.Button("释放标签"))
        {
            ReleaseTag();
            StarRange.aaa = true;
            starObject = null;
            planetObject = null;
        }
        GUILayout.Space(10);

        GUILayout.Space(10);
        float starX = starObject.GetComponent<Renderer>().bounds.size.x;
        float starZ = starObject.GetComponent<Renderer>().bounds.size.z;
        float planetX = planetObject.transform.localPosition.x;
        float planetZ = planetObject.transform.localPosition.z;
        if (Math.Abs(planetX) < starX * 21.74f && Math.Abs(planetZ) < starZ * 55.56f && Math.Abs(planetX) > starX * 8.7f &&  StarRange.aaa == true)
        {
            QuadMarshal();
            StarRange.aaa = false;
            //Debug.Log(planetObject.transform.localPosition.x);
            //planetObject = null;
            //starObject = null;    
        }
        if (Math.Abs(planetX) < starX * 8.7f && Math.Abs(planetZ) < starZ * 55.56f && StarRange.aaa == true)
        {
            
            BiMarshal();
            StarRange.aaa = false;
            //Debug.Log(planetObject.transform.localPosition.x);
            //planetObject = null;
            //starObject = null;
        }

    }


    private void BiMarshal()
    {
        starObject.tag = "Star";
        planetObject.tag = "Planet1";
        Vector3 starVec = starObject.transform.position;
        Quaternion angles = Quaternion.identity;
        Vector3 rotation = new Vector3(0 ,180, 0);
        angles.eulerAngles += rotation;
        Vector3 planet2Vec = new Vector3(2 * starVec.x - planetObject.transform.position.x, planetObject.transform.position.y, 2 * starVec.z - planetObject.transform.position.z);
        GameObject planet2 = Instantiate(planetObject, planet2Vec, angles, planetObject.transform);
        planet2.tag = "Planet2";
        GameObject.FindWithTag("Planet2").AddComponent(typeof(TargetExample));
        Debug.Log("已完成二分编组");
    }
    private void QuadMarshal()
    {
        starObject.tag = "Star";
        planetObject.tag = "Planet1";
        Vector3 starVec = starObject.transform.position;
        Quaternion angles = Quaternion.identity;
        Vector3 rotation = new Vector3(0, 180, 0);
        angles.eulerAngles += rotation;
        GameObject planet2 = Instantiate(planetObject, 2 * starVec - planetObject.transform.position, angles, starObject.transform);
        planet2.tag = "Planet2";
        Vector3 planetVec2 = new Vector3(planetObject.transform.position.x, planetObject.transform.position.y, planet2.transform.position.z);
        GameObject planet3 = Instantiate(planet2, planetVec2, angles , planet2.transform);
        planet3.tag = "Planet3";
        GameObject planet4 = Instantiate(planet3, 2 * starVec - planet3.transform.position, Quaternion.identity, planet3.transform);
        planet4.tag = "Planet4";
        GameObject.FindWithTag("Planet2").AddComponent(typeof(TargetExample));
        GameObject.FindWithTag("Planet3").AddComponent(typeof(TargetExample));
        GameObject.FindWithTag("Planet4").AddComponent(typeof(TargetExample));
        Debug.Log("已完成四分编组");
    }
  
    private void MirrorSymmetry()
    {
        Vector3 starVec = GameObject.FindWithTag("Star").transform.position;
        Vector3 gemini2Vec = new Vector3();
        gemini2Vec.x = GameObject.FindWithTag("Planet1").transform.position.x;
        gemini2Vec.y = GameObject.FindWithTag("Planet1").transform.position.y;
        gemini2Vec.z = 2 * starVec.z - GameObject.FindWithTag("Planet1").transform.position.z;
        GameObject.FindWithTag("Planet2").transform.position = gemini2Vec;
    }
    private void CentroSymmetry() {

        Vector3 starVec = GameObject.FindWithTag("Star").transform.position;
        Vector3 planet2Vec = new Vector3();
        planet2Vec.x = 2 * starVec.x - GameObject.FindWithTag("Planet1").transform.position.x;
        planet2Vec.y = GameObject.FindWithTag("Planet1").transform.position.y;
        planet2Vec.z = 2 * starVec.z - GameObject.FindWithTag("Planet1").transform.position.z;
        GameObject.FindWithTag("Planet2").transform.position = planet2Vec;

        Vector3 planet3Vec = new Vector3 ();
        planet3Vec.x = GameObject.FindWithTag("Planet1").transform.position.x;
        planet3Vec.y = GameObject.FindWithTag("Planet1").transform.position.y;
        planet3Vec.z = GameObject.FindWithTag("Planet2").transform.position.z;
        GameObject.FindWithTag("Planet3").transform.position = planet3Vec;

        Vector3 planet3Rot = new Vector3();
        planet3Rot.x = GameObject.FindWithTag("Planet2").transform.localEulerAngles.x;
        planet3Rot.y = -GameObject.FindWithTag("Planet1").transform.localEulerAngles.y;
        planet3Rot.z = GameObject.FindWithTag("Planet1").transform.localEulerAngles.z;
        GameObject.FindWithTag("Planet3").transform.localEulerAngles = planet3Rot;
        GameObject.FindWithTag("Planet4").transform.position = 2 * starVec - GameObject.FindWithTag("Planet3").transform.position;
    }
    private void ReleaseTag()
    {
        GameObject.FindWithTag("Planet1").tag = "Planet";
        GameObject.FindWithTag("Planet2").tag = "Planet";
        GameObject.FindWithTag("Planet3").tag = "Planet";
        GameObject.FindWithTag("Planet4").tag = "Planet";
        GameObject.FindWithTag("Star").tag = "Untagged";
        
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