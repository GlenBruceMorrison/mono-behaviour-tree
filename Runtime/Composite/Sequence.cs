using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MonoBehaviourTree
{
    public class Sequence : Composite
    {
        public override void Entry()
        {

        }

        public override void Exit()
        {
            Children.ForEach(x => x.Activate());
        }

        public override NodeStatus Run()
        {
            if (Children.Count == 0)
            {
                return NodeStatus.Success;
            }

            foreach (var child in Children)
            {
                var status = child.InternalRun();

                if (status == NodeStatus.Failure) return status;
                if (status == NodeStatus.Running) return status;
            }

            return NodeStatus.Success;
        }

        [MenuItem("GameObject/BehaviourTree/Sequence")]
        private static void CreateMenu()
        {
            var obj = new GameObject("Sequence");
            obj.AddComponent<Sequence>();
            obj.transform.parent = Selection.activeTransform;
            Selection.activeGameObject = obj;
        }
    }
}
