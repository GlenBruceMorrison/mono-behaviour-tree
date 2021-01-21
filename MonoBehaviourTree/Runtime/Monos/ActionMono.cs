using Patterns.BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionMono : MonoBehaviour, INode
{
    public string Name
    {
        get
        {
            return name;
        }
    }

    public Node Parent
    {
        get;
        set;
    }

    public void InternalActivate()
    {
        Activate();
    }

    public NodeStatus InternalRun()
    {
        return Run();
    }

    public abstract void Activate();
    public abstract NodeStatus Run();
}
