using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;
using System.ComponentModel;

namespace MonoBehaviourTree
{
    public class BlackBoard : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private Dictionary<string, object> _data = new Dictionary<string, object>();

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        public void Set<T>(string name, T data)
        {
            if (_data.ContainsKey(name))
            {
                _data[name] = data;
                return;
            }

            _data.Add(name, data);
        }

        public void Set(string name, object data)
        {
            if (_data.ContainsKey(name))
            {
                _data[name] = data;
                return;
            }

            _data.Add(name, data);
        }

        public object Get(string name)
        {
            return _data[name];
        }

        public T Get<T>(string name)
        {
            try
            {
                var data = _data[name];

                if (data is Func<T> func)
                {
                    return func.Invoke();
                }

                return (T)_data[name];
            }
            catch (InvalidCastException ex)
            {
                Debug.LogError($"Cast is not valid for {name} to type {typeof(T).ToString()}");
            }
            catch (KeyNotFoundException ex)
            {
                Debug.LogWarning($"Key [{name}] does not exist in blackboard at this time.");
            }

            return default;
        }

        public T Get<T>()
        {
            return (T)_data.Where(x => x.Value.GetType() == typeof(T)).FirstOrDefault().Value;
        }
    }

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

        private BlackBoard _blackBoard = new BlackBoard();

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

        public BlackBoard BlackBoard
        {
            get
            {
                return _blackBoard;
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
    }
}
