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
        private bool _active = false;

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

        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;

                if (_active)
                {
                    gameObject.name = $"(active){gameObject.name}";
                }
                else
                {
                    gameObject.name = gameObject.name.Replace("(active)", string.Empty);
                }
            }
        }

        public bool IsParent
        {
            get
            {
                return transform.childCount > 0;
            }
        }

        internal NodeStatus InternalRun()
        {
            if (!Active)
            {
                Activate();
            }

            var status = Run();

            if (status != NodeStatus.Running)
            {
                Deactivate();
            }

            return status;
        }

        internal void Activate()
        {
            if (Active)
            {
                return;
            }

            Entry();
            Active = true;

            if (IsParent)
            {
                ParentEntry();
            }
        }

        internal void Deactivate()
        {
            if (!Active)
            {
                return;
            }

            Exit();
            Active = false;

            if (IsParent)
            {
                ParentExit();
            }
        }

        public virtual void ParentEntry() { }

        public virtual void ParentExit() { }

        public virtual void Entry() { }

        public virtual void Exit() { }

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
