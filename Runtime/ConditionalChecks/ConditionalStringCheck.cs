using System;

namespace MonoBehaviourTree
{
    public class ConditionalStringCheck : Conditional<string>
    {
        public Operator operatorComparison;
        public string value;

        public override bool CompareValues(string treeValue)
        {
            switch (operatorComparison)
            {
                case Operator.EqualTo:
                    return value == treeValue;
                case Operator.NotEqualTo:
                    return value != treeValue;
                default:
                    return false;
            }
        }
    }
}