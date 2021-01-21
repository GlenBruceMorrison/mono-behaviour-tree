using System.Collections.Generic;

namespace Patterns.BehaviourTree
{
    public interface INode
    {
        Node Parent { get; set; }
        void InternalActivate();
        NodeStatus InternalRun();
    }

    public abstract class Node : INode
    {
        private Node _parent;

        private bool _isActive;

        public Node Parent
        {
            set
            {
                _parent = value;
            }
            get
            {
                return _parent;
            }
        }

        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                if (_isActive == value)
                {
                    return;
                }

                _isActive = true;

                if (_isActive)
                {
                    InternalActivate();
                }
            }
        }

        public void InternalActivate()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;

            InternalActivate();
        }

        public NodeStatus InternalRun()
        {
            if (!IsActive)
            {
                InternalActivate();
            }

            var status = Run();

            return status;
        }

        public virtual void Activate()
        {

        }

        public virtual NodeStatus Run()
        {
            return NodeStatus.Success;
        }
    }
}