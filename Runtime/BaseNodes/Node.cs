using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace MonoBehaviourTree
{
    public enum NodeStatus
    {
        Success,
        Failure,
        Running
    }

    public abstract class Node : MonoBehaviour
    {
        private BehaviourTree _root;
        private Node _parent;
        private bool _initiated;
        private bool _running;

        public Node Parent
        {
            get
            {
                if (_parent == null)
                {
                    _parent = GetComponent<Node>();
                }

                return _parent;
            }
        }

        public BehaviourTree Root
        {
            get
            {
                if (_root == null)
                {
                    _root = GetComponentInParent<BehaviourTree>();
                }

                return _root;
            }
        }

        public bool Initiated
        {
            get
            {
                return _initiated;
            }
            set
            {
                if (_initiated == value)
                {
                    return;
                }

                _initiated = value;

                if (_initiated)
                {
                    InternalEntry();
                }
                else
                {
                    InternalExit();
                }
            }
        }
        public bool Running
        {
            get
            {
                return _running;
            }
        }

        //public abstract void InitializeData(ref Dictionary<string, object> treeData);

        internal NodeStatus InternalRun()
        {
            if (!Initiated)
            {
                Initiated = true;
            }

            var status = Run();

            if (status != NodeStatus.Running)
            {
                Exit();
            }

            return status;
        }

        internal void InternalEntry()
        {
            Entry();
            _running = true;
        }

        internal void InternalExit()
        {
            Exit();
            _running = false;
        }

        public abstract void Entry();

        public abstract void Exit();

        public abstract NodeStatus Run();

        [MenuItem("GameObject/BehaviourTree/Action")]
        private static void CreateMenu()
        {
            var obj = new GameObject("Action");
            obj.transform.parent = Selection.activeTransform;
            Selection.activeGameObject = obj;
        }
    }
}
