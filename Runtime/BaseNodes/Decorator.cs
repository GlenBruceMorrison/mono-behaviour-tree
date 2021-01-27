using UnityEditor;
using UnityEngine;

namespace MonoBehaviourTree
{
    public abstract class Decorator : Node
    {
        private Node _child;

        public Node Child
        {
            get
            {
                return _child;
            }
        }

        public override void Entry()
        {
            Child.InternalEntry();
        }

        public override void Exit()
        {

        }

        public override NodeStatus Run()
        {
            if (Child == null)
            {
                return NodeStatus.Success;
            }

            return Child.InternalRun();
        }
    }
}
