using UnityEditor;
using UnityEngine;

namespace MonoBehaviourTree
{
    public class Selector : Composite
    {
        public override void Entry()
        {

        }

        public override void Exit()
        {
            Children.ForEach(x => x.InternalExit());
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

                if (status == NodeStatus.Failure) continue;
                if (status == NodeStatus.Running) return status;
                if (status == NodeStatus.Success) return status;
            }

            return NodeStatus.Failure;
        }

        [MenuItem("GameObject/BehaviourTree/Selector")]
        private static void CreateMenu()
        {
            var obj = new GameObject("Selector");
            obj.AddComponent<Selector>();
            obj.transform.parent = Selection.activeTransform;
            Selection.activeGameObject = obj;
        }
    }
}
