using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : Kinematic
{
    public Kinematic[] otherBirds;

    private GameObject flockMass;
    private Arrive cohesion;
    private Separation separation;
    private Align alignment;
    private ObstacleAvoidance avoid;

    public FlockController flockController;

    // Start is called before the first frame update
    void Start()
    {
        flockMass = myTarget;

        cohesion = new Arrive();
        cohesion.character = this;
        cohesion.target = flockMass;

        separation = new Separation();
        separation.character = this;
        separation.targets = otherBirds;

        avoid = new ObstacleAvoidance();
        avoid.character = this;
        avoid.target = flockMass;
    }

    // Update is called once per frame
    void Update()
    {
        steeringUpdate = new SteeringOutput();

        steeringUpdate.linear = avoid.getSteering().linear;

        if (avoid.hitWall == false)
        {
            steeringUpdate.linear = cohesion.getSteering().linear * flockController.cohesionSTR;
            steeringUpdate.linear += separation.getSteering().linear * flockController.separationSTR;
        }

        base.Update();
    }
}
