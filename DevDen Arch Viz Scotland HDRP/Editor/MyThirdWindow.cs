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
    [MenuItem("MyTool/Symmetrical Layout/Marshaller - Z plane")]
    static void ShowWindows()
    {
        EditorWindow.GetWindow(typeof(MyThirdWindow));

    }
}