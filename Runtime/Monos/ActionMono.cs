using Patterns.BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionMono : MonoBehaviour, INode
{
    private bool _initiated;

    public string Name
    {
        get
        {
            return name;
        }
    }

    public bool Initiated
    {
        get
        {
            return _initiated;
        }
        set
        {
            if (_initiated == value)
            {
                return;
            }

            _initiated = value;

            if (_initiated)
            {
                HandleInitiation();
            }
            else
            {
                HandleDeactivation();
            }
        }
    }

    public INode Parent
    {
        get;
        set;
    }

    public void InternalInitiate()
    {
        if (Initiated)
        {
            return;
        }

        Initiated = true;
    }

    public void InternalDeactivate()
    {
        if (!Initiated)
        {
            return;
        }

        Initiated = false;
    }

    public NodeStatus InternalRun()
    {
        if (!Initiated)
        {
            InternalInitiate();
        }

        var status = Run();

        if (status != NodeStatus.Running)
        {
            InternalDeactivate();
        }

        return status;
    }

    public abstract void HandleInitiation();

    public abstract void HandleDeactivation();

    public abstract NodeStatus Run();
    
    public override string ToString()
    {
        return "[Action Mono]";
    }

}
