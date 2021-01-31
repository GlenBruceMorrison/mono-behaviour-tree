using System;
using UnityEngine;

namespace MonoBehaviourTree
{
    public abstract class BasicConditional<T> : Conditional<T>
    {
        [Header("operation to perform on data")]
        public Operator operatorComparison;

        [Header("data to perform operation with")]
        public string otherTreeValue;
        public T value;

        public T GetCompareVal()
        {
            if (string.IsNullOrEmpty(otherTreeValue))
            {
                return value;
            }

            return Root.BlackBoard.Get<T>(otherTreeValue);
        }

        public override bool CompareValues(T treeValue)
        {
            return CompareValue(treeValue, GetCompareVal());
        }

        public abstract bool CompareValue(T treeVal, T compareVal);
    }

    public abstract class Conditional<T> : Condition
    {
        public enum Operator
        {
            EqualTo,
            NotEqualTo,
            LessThan,
            MoreThan,
            LessOrEquals,
            MoreOrEquals
        }

        [Header("Name of value in tree data dictionary")]
        public string treeValueName;

        public override bool Evaluate()
        {
            object treeValue = Root.BlackBoard.Get(treeValueName);
            T compareValue = default;

            if (treeValue is Func<T> val)
            {
                compareValue = val.Invoke();
            }
            else
            {
                compareValue = (T)treeValue;
            }

            return CompareValues(compareValue);
        }

        public abstract bool CompareValues(T treeValue);
    }
}