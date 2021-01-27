using System;

namespace MonoBehaviourTree
{
    public class ConditionalIntCheck : BasicConditional<int>
    {
        public override bool CompareValue(int treeVal, int compareVal)
        {
            switch (operatorComparison)
            {
                case Operator.EqualTo:
                    return treeVal == compareVal;
                case Operator.NotEqualTo:
                    return treeVal != compareVal;
                case Operator.LessThan:
                    return treeVal < compareVal;
                case Operator.MoreThan:
                    return treeVal > compareVal;
                case Operator.LessOrEquals:
                    return treeVal <= compareVal;
                case Operator.MoreOrEquals:
                    return treeVal >= compareVal;
                default:
                    return false;
            }
        }
    }
}