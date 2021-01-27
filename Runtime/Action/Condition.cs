using MonoBehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Condition : Node
{
    public override void Entry() { }

    public override void Exit() { }

    public override NodeStatus Run()
    {
        if (Evaluate())
        {
            return NodeStatus.Success;
        }

        return NodeStatus.Failure;
    }

    public abstract bool Evaluate();
}
