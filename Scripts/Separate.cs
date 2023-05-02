using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separate : Kinematic
{
    Separation myMoveType;

    public Kinematic[] targets;

    void Start()
    {
        myMoveType = new Separation();
        myMoveType.character = this;
        myMoveType.targets = targets;
    }

    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = 0;
        base.Update();
    }
}
