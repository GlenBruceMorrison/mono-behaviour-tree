using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviourTree
{
    public class Invertor : Decorator
    {
        public override NodeStatus Run()
        {
            var status = Child.Run();

            switch (status)
            {
                case NodeStatus.Success:
                    return NodeStatus.Failure;
                case NodeStatus.Failure:
                    return NodeStatus.Success;
            }

            return NodeStatus.Running;
        }
    }
}
