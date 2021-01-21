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
        private INode Current
        {
            get
            {
                return _current;
            }
            set
            {
                _current = value;
            }
        }

        public TreeConstructor(INode root)
        {
            _root = root;
            Current = _root;
        }

        public TreeConstructor()
        {
            _root = new Sequence();
            Current = _root;
        }

        private TreeConstructor AddComposite(Composite node)
        {
            if (Current is Composite composite)
            {
                composite.Add(node);
            }
            Down();
            return this;
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

        private TreeConstructor Add(INode node)
        {
            if (Current is Composite composite)
            {
                composite.Add(node);
            }

            else if (Current is Decorator decorator)
            {
                decorator.Child = node;
            }

            return this;
        }

        public TreeConstructor Down()
        {
            if (Current is Composite composite)
            {
                if (composite.Children.Count > 0)
                {
                    Current = composite.Children.Last();
                }
            }

            else if (Current is Decorator decorator)
            {
                Current = decorator.Child;
            }

            return this;
        }

        public TreeConstructor Up()
        {
            Current = Current.Parent;
            return this;
        }

        public TreeConstructor Root()
        {
            Current = _root;
            return this;
        }

        public INode Construct()
        {
            var value = _root;
            Current = _root;
            return value;
        }

        public static void PrintNode(INode node)
        {
            string indentation = "";
            PrintNode(node, indentation);
        }

        public static void PrintNode(INode node, string indentation)
        {
            Debug.Log(indentation + node.ToString());

            if (node is Composite composite)
            {
                foreach (var child in composite.Children)
                {
                    var n = indentation + "==";
                    PrintNode(child, n);
                }
            }

            if (node is Decorator decorator)
            {
                if (decorator.Child == null)
                {
                    return;
                }

                var n = indentation + "==";
                PrintNode(decorator.Child, n);
            }
        }
    }
}