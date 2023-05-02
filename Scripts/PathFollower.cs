using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : Kinematic
{
    public Seek myMoveType;
    public LookWhereGoing myRotateType;

    public GameObject[] targets;

    [SerializeField]
    private int targetIndex = 0;

    public float waypointDetectRange;

    void Start()
    {
        myMoveType = new Seek();
        myMoveType.character = this;
        myMoveType.target = targets[targetIndex];

        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
    }

    protected override void Update()
    {
        if (Vector3.Distance(this.transform.position, targets[targetIndex].transform.position) < waypointDetectRange)
        {
            if (targetIndex < targets.Length - 1)
            {
                targetIndex++;
            }
            else
            {
                targetIndex = 0;
            }
            myMoveType.target = targets[targetIndex];
        }

        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myRotateType.getSteering().angular;

        base.Update();
    }
}
