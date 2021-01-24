using System.Collections.Generic;

namespace Patterns.BehaviourTree
{
    public abstract class Decorator : Node, INode
    {
        private INode _child;

        public Decorator()
        {

        }

        public INode Child
        {
            get
            {
                return Children[0];
            }
            set
            {
                value.Parent = this;
                _child = value;
            }
        }

        public override string ToString()
        {
            return "[Decorator]";
        }
    }
}