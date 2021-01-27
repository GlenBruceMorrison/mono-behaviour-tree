using System;

namespace MonoBehaviourTree
{

    public class ConditionalFloatCheck : BasicConditional<float>
    {
        public override bool CompareValue(float treeVal, float compareVal)
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