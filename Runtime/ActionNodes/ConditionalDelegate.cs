using System;

namespace Patterns.BehaviourTree
{
    public class ConditionalDelegate : Condition
    {
        public Func<bool> evaluate_func;

        public ConditionalDelegate() { }
        public ConditionalDelegate(Func<bool> evaluate_func, bool failImmediate=false) : base(failImmediate)
        {
            this.evaluate_func = evaluate_func;
        }

        public override bool Evaluate()
        {
            return evaluate_func();
        }

        public override string ToString()
        {
            return "[Conditional Delegate]";
        }
    }
}