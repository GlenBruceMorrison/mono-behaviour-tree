using System.Collections.Generic;

namespace Patterns.BehaviourTree
{
    public class Parallel : Composite
    {
        public override NodeStatus Run()
        {
            if (Children.Count == 0)
            {
                return NodeStatus.Success;
            }

            var allSucceed = true;

            foreach (var child in Children)
            {
                var status = child.InternalRun();

                if (status == NodeStatus.Failure) return status;
                if (status == NodeStatus.Running) allSucceed = false;
            }

            if (allSucceed)
            {
                return NodeStatus.Success;
            }

            return NodeStatus.Running;
        }

        public override string ToString()
        {
            return "[Selector]";
        }
    }

}