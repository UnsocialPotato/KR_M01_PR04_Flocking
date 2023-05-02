using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : Kinematic
{
    public bool avoidObstacles = false;
    public GameObject myCohereTarget;

    BlendedSteering mySteering;
    PrioritySteering myAdvancedSteering;
    Kinematic[] kBirds;

    void Start()
    {
        //Seperate from other birds
        Separation separate = new Separation();
        separate.character = this;
        GameObject[] goBirds = GameObject.FindGameObjectsWithTag("bird");
        kBirds = new Kinematic[goBirds.Length - 1];
        int j = 0;
        for (int i=0; i < goBirds.Length-1; i++)
        {
            if (goBirds[1] == this)
            {
                continue;
            }
            kBirds[j++] = goBirds[i].GetComponent<Kinematic>();
        }
        separate.targets = kBirds;

        //cohere to center of mass - ez mode
        Arrive cohere = new Arrive();
        cohere.character = this;
        cohere.target = myCohereTarget;

        //look where center of mass is going - ez mode
        LookWhereGoing myRotateType = new LookWhereGoing();
        myRotateType.character = this;

        mySteering = new BlendedSteering();
        mySteering.behaviors = new BehaviorAndWeight[3];
        mySteering.behaviors[0] = new BehaviorAndWeight();
        mySteering.behaviors[0].behavior = separate;
        mySteering.behaviors[0].weight = 1f; //3
        mySteering.behaviors[1] = new BehaviorAndWeight();
        mySteering.behaviors[1].behavior = cohere;
        mySteering.behaviors[1].weight = 1f; //.5
        mySteering.behaviors[2] = new BehaviorAndWeight();
        mySteering.behaviors[2].behavior = myRotateType;
        mySteering.behaviors[2].weight = 1f;

        //set up prioritySteering
        ObstacleAvoidance myAvoid = new ObstacleAvoidance();
        myAvoid.character = this;
        myAvoid.target = myCohereTarget;
        myAvoid.flee = true; // otherwise I seek to the obstacle
        BlendedSteering myHighPrioritySteering = new BlendedSteering();
        myHighPrioritySteering.behaviors = new BehaviorAndWeight[1];
        myHighPrioritySteering.behaviors[0] = new BehaviorAndWeight();
        myHighPrioritySteering.behaviors[0].behavior = myAvoid;
        myHighPrioritySteering.behaviors[0].weight = 1f;

        myAdvancedSteering = new PrioritySteering();
        myAdvancedSteering.groups = new BlendedSteering[2];
        myAdvancedSteering.groups[0] = new BlendedSteering();
        myAdvancedSteering.groups[0] = myHighPrioritySteering;
        myAdvancedSteering.groups[1] = new BlendedSteering();
        myAdvancedSteering.groups[1] = mySteering;
    }


    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        if (!avoidObstacles)
        {
            steeringUpdate = mySteering.getSteering();
        }
        else
        {
            steeringUpdate = myAdvancedSteering.getSteering();
        }
        base.Update();
    }
}
