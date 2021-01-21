namespace Patterns.BehaviourTree
{
    public abstract class Decorator : Node
    {
        private INode _child;

        public Decorator()
        {

        }

        public INode Child
        {
            get
            {
                return _child;
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