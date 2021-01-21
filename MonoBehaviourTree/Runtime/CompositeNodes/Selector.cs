using System.Collections.Generic;

namespace Patterns.BehaviourTree
{
    public class Selector : Composite
    {
        public override NodeStatus Run()
        {
            if (Children.Count == 0)
            {
                return NodeStatus.Success;
            }

            foreach(var child in Children)
            {
                var status = child.InternalRun();

                if (status != NodeStatus.Running)
                {
                    continue;
                }

                return status;
            }

            return NodeStatus.Failure;
        }
    }
}