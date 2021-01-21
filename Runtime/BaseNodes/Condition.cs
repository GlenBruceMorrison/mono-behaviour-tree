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


        /*
         * [Revisit]
         * 
         * 
         * 
         */
        public override NodeStatus Run()
        {
            if (Child != null)
            {
                var result = Evaluate();

                // If true the run child
                if (result)
                {
                    return Child.InternalRun();
                }
                else
                {
                    //if (FailImmediate)
                    //{
                        return NodeStatus.Success;
                    //}
                    //else
                    //{
                    //    return NodeStatus.Running;
                    //}
                }
            }


            ///////////////////




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