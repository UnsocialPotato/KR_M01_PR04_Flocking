using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoider : Kinematic
{
    ObstacleAvoidance myMoveType;
    LookWhereGoing myRotateType;

    void Start()
    {
        myMoveType = new ObstacleAvoidance();
        myMoveType.character = this;
        myMoveType.target = myTarget;

        myRotateType = new LookWhereGoing();
        myRotateType.character = this;

    }

    protected override void Update()
    {
        steeringUpdate = myMoveType.getSteering();

        //steeringUpdate.angular = myRotateType.getTargetAngle();
        base.Update();
    }
}
