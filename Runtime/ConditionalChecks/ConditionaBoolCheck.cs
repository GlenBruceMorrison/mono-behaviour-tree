using System;

namespace MonoBehaviourTree
{

    public class ConditionalBoolCheck : BasicConditional<bool>
    {
        public override bool CompareValue(bool treeVal, bool compareVal)
        {
            switch (operatorComparison)
            {
                case Operator.EqualTo:
                    return compareVal == treeVal;
                case Operator.NotEqualTo:
                    return compareVal != treeVal;
                default:
                    return false;
            }
        }
    }

}