# Behaviour Tree For Unity

In progress

```cs
using Patterns.BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    INode tree;

    public Transform target;
    public int health;

    void Start()
    {
        tree = new TreeConstructor()
            .ConditionalDelegate(() => return health <= 0)
            .ConditionalDelegate(() => health < 10)
            .Down()
                .ActionDelegate(() =>
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.position, -1 * 3 * Time.deltaTime);
                    return NodeStatus.Running;
                })
            .Up()
            .ConditionalDelegate(() => Vector3.Distance(transform.position, target.transform.position) < 1)
            .Down()
                .ActionDelegate(() =>
                {
                    // AttackTarget()
                    return NodeStatus.Running;
                })
            .Up()
            .ActionDelegate(() => {
                transform.position = Vector3.MoveTowards(transform.position, target.position, 3 * Time.deltaTime);
                return NodeStatus.Running;
            })
        .Construct();

        TreeConstructor.PrintNode(tree);
    }

    private void Update()
    {
        tree.InternalRun();   
    }
}


```
