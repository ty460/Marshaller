using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class MySecondWindow : EditorWindow
{

    MySecondWindow()
    {
        this.titleContent = new GUIContent("Marshaller - X plane");

    }
    [MenuItem("MyTool/Symmetrical Layout/Marshaller - X plane")]
    static void ShowWindows()
    {
        EditorWindow.GetWindow(typeof(MySecondWindow));

    }
}