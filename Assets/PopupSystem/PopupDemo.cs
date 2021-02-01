using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDemo : MonoBehaviour
{
    async void OnGUI()
    {
        if (GUILayout.Button("Open DemoWindow"))
        {
            await PopupSystem.Instance.ShowPopup<DemoWindowController>("DemoWindow");
        }
        else if (GUILayout.Button("Open DemoWindow 1"))
        {
            //PopupSystem.Instance.ShowPopup<DemoWindowController1>("DemoWindow_1");

            //DemoWindowController1 windowController1 = new DemoWindowController1();
            //windowController1.UseLifeCycle = true;
            //PopupSystem.Instance.ShowPopup("DemoWindow_1", windowController1);


            await PopupSystem.Instance.ShowPopup(new DemoWindowController1("DemoWindow_1")
                   .SetLifeCycle(true)
                   .SetUseMask(true)
                   .SetCloseOnClickMask(true));
        }
        else if (GUILayout.Button("CloseAllPopup"))
        {
            PopupSystem.Instance.CloseAllPopup();
        }
    }
}
