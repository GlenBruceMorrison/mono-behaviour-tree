using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

namespace MonoBehaviourTree
{
    public class BehaviourTree : MonoBehaviour
    {
        public class Builder
        {
            public Node Current;
            public BehaviourTree tree;

            public Builder(BehaviourTree tree)
            {
                this.tree = tree;
            }

            public Builder Up()
            {
                if (Current.Parent != null)
                {
                    Current = Current.Parent;
                }

                return this;
            }
            
            public Builder Down()
            {
                if (Current is Composite composite)
                {
                    Current = composite.Children.Last();
                }

                return this;
            }

            public Builder Add<T>() where T : Composite
            {
                var obj = new GameObject(typeof(T).Name);
                obj.transform.parent = Current.transform;
                Current = obj.AddComponent<T>();
                return this;
            }
        }

        private Builder _myBuilder;
        public Builder MyBuilder
        {
            get
            {
                if (_myBuilder == null)
                {
                    _myBuilder = new Builder(this);
                }

                return _myBuilder;
            }
        }

        [SerializeField]
        private Node _current;

        [SerializeField]
        private List<Node> _children;

        private Dictionary<string, object> _data = new Dictionary<string, object>();

        public Node Current
        {
            get
            {
                return _current;
            }
        }

        public List<Node> Children
        {
            get
            {
                

                return _children;
            }
        }

        /*
        private void Awake()
        {
            foreach (var node in GetComponentsInChildren<Node>())
            {
                node.InitializeData(ref _data);
            }
        }
        */

        private void Start()
        {
            foreach (Transform child in transform)
            {
                var node = child.gameObject.GetComponent<Node>();
                _children.Add(node);
            }

            var test = GetComponentInChildren<Node>();

            _current = Children.First();
        }

        public void Update()
        {
            _current.InternalRun();
        }

        public void SetData(string name, object data)
        {
            if (_data.ContainsKey(name))
            {
                _data[name] = data;
                return;
            }

            _data.Add(name, data);
        }

        public void SetData<T>(T data)
        {
            if (GetData<T>() == null)
            {
                _data.Add(new GUID().ToString(), data);
            }
        }

        public object GetData(string name)
        {
            return _data[name];
        }

        public T GetData<T>(string name)
        {
            var data = _data[name];

            if (data is Func<T> func)
            {
                return func.Invoke();
            }

            return (T)_data[name];
        }

        public T GetData<T>()
        {
            return (T)_data.Where(x => x.Value.GetType() == typeof(T)).FirstOrDefault().Value;
        }
    }
}
