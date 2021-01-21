using System.Collections.Generic;

namespace Patterns.BehaviourTree
{
    public abstract class Condition : Decorator
    {
        private readonly bool _failImmediate = false;

        public Condition()
        {

        }

        public Condition(bool failImmediate=false)
        {
            this._failImmediate = failImmediate;
        }

        public bool FailImmediate
        {
            get
            {
                return _failImmediate;
            }
        }

        public abstract bool Evaluate();

        public override NodeStatus Run()
        {
            if (!Evaluate())
            {
                if (FailImmediate)
                {
                    return NodeStatus.Failure;
                }
                else
                {
                    return NodeStatus.Running;
                }
            }

            return NodeStatus.Success;
        }
    }
}