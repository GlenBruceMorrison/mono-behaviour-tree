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
                if (_child == null)
                {
                    _child = transform.GetChild(0).GetComponent<Node>();
                }

                return _child;
            }
        }

        public override void Entry()
        {
            Child.Activate();
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
