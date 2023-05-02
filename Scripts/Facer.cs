using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Facer : Kinematic
{
    public Face myRotateType;

    void Start()
    {
        myRotateType = new Face();
        myRotateType.character = this;
        myRotateType.target = myTarget;
    }


    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
