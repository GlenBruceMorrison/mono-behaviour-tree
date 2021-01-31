using System.Collections.Generic;
using UnityEngine;

namespace MonoBehaviourTree
{
    public abstract class Composite : Node
    {
        private List<Node> _children = new List<Node>();

        public List<Node> Children
        {
            get
            {
                if (_children == null || _children.Count <= 0)
                {
                    foreach (Transform child in transform)
                    {
                        var node = child.GetComponent<Node>();
                        _children.Add(node);
                    }
                }

                return _children;
            }
        }

        public override void ParentEntry()
        {
            Children.ForEach(x => x.ParentEntry());
        }

        public override void ParentExit()
        {
            Children.ForEach(x => x.ParentExit());
        }
    }
}
