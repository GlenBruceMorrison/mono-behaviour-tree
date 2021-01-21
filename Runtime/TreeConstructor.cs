using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Patterns.BehaviourTree
{
    public class TreeConstructor
    {
        private INode _root;
        private INode _current;

        public TreeConstructor(INode root)
        {
            _root = root;
            _current = _root;
        }

        public TreeConstructor()
        {
            _root = new Sequence();
            _current = _root;
        }

        public TreeConstructor AddComposite(Composite node)
        {
            if (_current is Composite composite)
            {
                composite.Add(node);
            }
            
            return this.Down();
        }

        public TreeConstructor Selector()
        {
            return AddComposite(new Selector());
        }

        public TreeConstructor Sequence()
        {
            return AddComposite(new Sequence());
        }

        public TreeConstructor ActionMono(Transform transform, string monoName)
        {
            return Add(transform.Find(monoName).GetComponent<ActionMono>());
        }

        public TreeConstructor ActionMono<T>(Transform transform) where T : ActionMono
        {
            return Add(transform.GetComponentInChildren<T>());
        }


        public TreeConstructor ActionDelegate(Func<NodeStatus> func_run)
        {
            return Add(new ActionDelegate(func_run));
        }

        public TreeConstructor ConditionalDelegate(Func<bool> func_condition, bool failImmediate = false)
        {
            return Add(new ConditionalDelegate(func_condition, failImmediate));
        }

        public TreeConstructor Add(INode node)
        {
            if (_current is Composite composite)
            {
                composite.Add(node);
            }
            return this;
        }

        public TreeConstructor Down()
        {
            if (_current is Composite composite)
            {
                if (composite.Children.Count > 0)
                {
                    _current = composite.Children.First();
                }
            }
            return this;
        }

        public TreeConstructor Up()
        {
            _current = _current.Parent;
            return this;
        }

        public TreeConstructor Root()
        {
            _current = _root;
            return this;
        }

        public INode Construct()
        {
            var value = _root;
            _current = _root;
            return value;
        }
    }

}