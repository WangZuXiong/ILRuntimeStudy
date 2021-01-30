using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DemoWindowController : BasePopup
{
    public async override Task InitView0(GameObject gameObject)
    {
        await base.InitView0(gameObject);

        var mono = gameObject.AddComponent<MonoBehaviourEventTrigger>();
        mono.InvokeRepeating("Foo", 0, 20);
        mono.awake = () =>
        {
            Debug.LogError("awake");
        };
        mono.onDestroy = () =>
        {
            Debug.LogError("onDestroy");
        };
    }

    protected override void AddEvent()
    {
        //throw new System.NotImplementedException();
    }

    protected override void RemoveEvent()
    {
        //throw new System.NotImplementedException();
    }

    void Foo()
    {
        Debug.LogError("demo window");
    }
}
