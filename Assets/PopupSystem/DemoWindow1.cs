using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoWindowController1 : BasePopup
{
    public DemoWindowController1(string path) : base(path)
    {
    }

    protected override void AddEvent()
    {
        //throw new System.NotImplementedException();
    }

    protected override void RemoveEvent()
    {
        //throw new System.NotImplementedException();
    }

    void Start()
    {
        Debug.LogError("demo window 1");
    }


    public override void Awake()
    {
        Debug.LogError("DemoWindowController1 Awake");
    }
}
