using System;

namespace MonoBehaviourTree
{
    public class ConditionalNullCheck : Conditional<object>
    {
        public override bool CompareValues(object treeValue)
        {
            return treeValue == null && (string)treeValue == "null";
        }
    }
}