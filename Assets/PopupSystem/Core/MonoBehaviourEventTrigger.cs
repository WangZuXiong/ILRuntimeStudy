using System;
using UnityEngine;

public class MonoBehaviourEventTrigger : MonoBehaviour
{
    public Action awake;
    public Action onEnable;
    public Action start;
    public Action update;
    public Action onDisable;
    public Action onDestroy;

    private void Awake()
    {
        awake?.Invoke();
    }

    private void OnEnable()
    {
        onEnable?.Invoke();
    }

    private void Start()
    {
        start?.Invoke();
    }

    private void Update()
    {
        update?.Invoke();
    }

    private void OnDisable()
    {
        onDisable?.Invoke();
    }

    private void OnDestroy()
    {
        onDestroy?.Invoke();
    }
}

