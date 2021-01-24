using System;
using System.Collections.Generic;

namespace Patterns.BehaviourTree
{
    public interface INode
    {
        INode Parent { get; set; }
        void InternalInitiate();
        void InternalDeactivate();
        NodeStatus InternalRun();
    }

    public abstract class Node : INode
    {
        private INode _parent;
        private List<INode> _children = new List<INode>();

        private bool _initiated;

        public INode Parent
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

        public List<INode> Children
        {
            get
            {
                return _children;
            }
        }

        public bool Initiated
        {
            get
            {
                return _initiated;
            }
            set
            {
                if (_initiated == value)
                {
                    return;
                }

                _initiated = value;

                if (_initiated)
                {
                    HandleInitiation();
                }
                else
                {
                    HandleDeactivation();
                }
            }
        }

        public void InternalInitiate()
        {
            if (Initiated)
            {
                return;
            }

            Initiated = true;
        }

        public void InternalDeactivate()
        {
            if (!Initiated)
            {
                return;
            }

            Initiated = false;
        }

        public NodeStatus InternalRun()
        {
            if (!Initiated)
            {
                InternalInitiate();
            }

            var status = Run();

            if (status != NodeStatus.Running)
            {
                InternalDeactivate();
            }

            return status;
        }

        public virtual void HandleInitiation() { }

        public virtual void HandleDeactivation() { }

        public virtual NodeStatus Run()
        {
            return NodeStatus.Success;
        }
    }
}