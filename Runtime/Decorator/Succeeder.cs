using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviourTree
{
    public class Succeeder : Decorator
    {
        public override NodeStatus Run()
        {
            Child.Run();
            return NodeStatus.Success;
        }
    }
}
