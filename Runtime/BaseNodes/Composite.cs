using System.Collections.Generic;

namespace Patterns.BehaviourTree
{
    public abstract class Composite : Node, INode
    {
        public void Add(INode node)
        {
            Children.Add(node);
            node.Parent = this;
        }
    }
}