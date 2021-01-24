using System;

namespace Patterns.BehaviourTree
{
    public class ActionDelegate : Node, INode
    {
        public Func<NodeStatus> run_func;

        public ActionDelegate() { }
        public ActionDelegate(Func<NodeStatus> run_func)
        {
            this.run_func = run_func;
        }

        public override NodeStatus Run()
        {
            return run_func();
        }
        public override string ToString()
        {
            return "[Action Delegate]";
        }
    }
}