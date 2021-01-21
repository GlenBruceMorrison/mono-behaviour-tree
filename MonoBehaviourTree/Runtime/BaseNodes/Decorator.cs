namespace Patterns.BehaviourTree
{
    public abstract class Decorator : Node
    {
        private Node _child;

        public Decorator()
        {

        }

        public Node Child
        {
            get
            {
                return _child;
            }
        }
    }
}