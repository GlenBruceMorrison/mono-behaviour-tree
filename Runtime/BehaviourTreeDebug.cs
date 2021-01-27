using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using MonoBehaviourTree;

namespace MonoBehaviourTree
{
    public class BehaviourTreeDebug : MonoBehaviour
    {
        private BehaviourTree _behaviourTree;

        private void Awake()
        {
            _behaviourTree = GetComponent<BehaviourTree>();
        }

        private void OnDrawGizmos()
        {
            if (_behaviourTree == null)
            {
                return;
            }

            Handles.Label(transform.position, _behaviourTree.Current.ToString());
        }
    }
}
