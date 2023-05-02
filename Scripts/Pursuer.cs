using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : Kinematic
{
    Pursue myMoveType;
    Evade myEvadeType;
    Face myRotateType;

    public bool isEvading;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Pursue();
        myMoveType.character = this;
        myMoveType.target = myTarget;

        myEvadeType = new Evade();
        myEvadeType.character = this;
        myEvadeType.target = myTarget;

        myRotateType = new Face();
        myRotateType.character = this;
        myRotateType.target = myTarget;

    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        if (isEvading == true)
        {
            steeringUpdate.linear = myEvadeType.getSteering().linear;
        }
        else
        {
            steeringUpdate.linear = myMoveType.getSteering().linear;
        }
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}
