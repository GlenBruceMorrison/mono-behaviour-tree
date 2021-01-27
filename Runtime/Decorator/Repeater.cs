using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviourTree
{
    public class Repeater : Decorator
    {
        public enum Type
        {
            Infinite,
            SetAmount,
            UntilFail
        }

        [SerializeField]
        private Type _type;

        [SerializeField]
        private int _repeatTotal;
        private int _repeatCurrent;

        public int RepeatTotal
        {
            get
            {
                if (_repeatTotal < 0)
                {
                    _repeatTotal = 0;
                }

                return _repeatTotal;
            }
        }

        public int RepeatCurrent
        {
            get
            {
                return _repeatCurrent;
            }
        }

        public override NodeStatus Run()
        {
            switch (_type)
            {
                case Type.Infinite:
                    Child.Run();
                    return NodeStatus.Running;

                case Type.SetAmount:
                    if (RepeatTotal > 0)
                    {
                        if (_repeatCurrent + 1 >= RepeatCurrent)
                        {
                            return Child.Run();
                        }
                    }

                    var status = Child.Run();

                    if (status == NodeStatus.Failure)
                    {
                        return status;
                    }

                    _repeatCurrent++;

                    return NodeStatus.Running;

                case Type.UntilFail:
                    var childStatus = Child.Run();

                    if (childStatus == NodeStatus.Failure || childStatus == NodeStatus.Running)
                    {
                        return childStatus;
                    }

                    return NodeStatus.Success;
            }

            return NodeStatus.Running;
        }
    }
}
