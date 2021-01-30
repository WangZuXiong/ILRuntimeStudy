using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDemo : MonoBehaviour
{
    void OnGUI()
    {
        if (GUILayout.Button("Open DemoWindow"))
        {
            PopupSystem.Instance.ShowPopup<DemoWindowController>("DemoWindow");
        }
        else if (GUILayout.Button("Open DemoWindow 1"))
        {
            PopupSystem.Instance.ShowPopup<DemoWindowController1>("DemoWindow_1");
        }
        else if (GUILayout.Button("CloseAllPopup"))
        {
            PopupSystem.Instance.CloseAllPopup();
        }
    }
}
