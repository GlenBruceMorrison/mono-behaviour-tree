using System.Collections.Generic;

namespace Patterns.BehaviourTree
{
    public class Sequence : Composite
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

                if (status == NodeStatus.Success) continue;
                if (status == NodeStatus.Running) return status;
                if (status == NodeStatus.Failure) return status;
            }

            return NodeStatus.Failure;
        }

        public override string ToString()
        {
            return "[Sequence]";
        }
    }
}