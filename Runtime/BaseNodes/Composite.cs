using System.Collections.Generic;

namespace Patterns.BehaviourTree
{
    public abstract class Composite : Node
    {
        private List<INode> _children;

        public Composite()
        {
            _children = new List<INode>();
        }

        public List<INode> Children
        {
            get
            {
                return _children;
            }
        }

        public void Add(INode node)
        {
            _children.Add(node);
            node.Parent = this;
        }
    }
}