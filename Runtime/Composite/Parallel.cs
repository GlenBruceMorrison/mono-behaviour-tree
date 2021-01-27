using UnityEditor;
using UnityEngine;

namespace MonoBehaviourTree
{
    public class Parallel : Composite
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

            var allSucceed = true;

            foreach (var child in Children)
            {
                var status = child.InternalRun();

                if (status == NodeStatus.Failure) return status;
                if (status == NodeStatus.Running) allSucceed = false;
            }

            if (allSucceed)
            {
                return NodeStatus.Success;
            }

            return NodeStatus.Running;
        }

        [MenuItem("GameObject/BehaviourTree/Parallel")]
        private static void CreateMenu()
        {
            var obj = new GameObject("Parallel");
            obj.AddComponent<Parallel>();
            obj.transform.parent = Selection.activeTransform;
            Selection.activeGameObject = obj;
        }
    }
}
