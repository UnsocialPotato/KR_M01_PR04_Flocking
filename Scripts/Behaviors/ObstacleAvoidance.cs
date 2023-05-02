using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : Seek
{
    public float avoidDistance = 2f;

    public float lookahead = 3f;

    public bool hitWall = false;

    protected override Vector3 getTargetPosition()
    {
        RaycastHit hit;

        if (Physics.Raycast(character.transform.position, character.linearVelocity, out hit, lookahead))
        {
            //Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * hit.distance, Color.red, 0.5f);
            //Debug.DrawRay(hit.point, hit.normal * avoidDistance, Color.red, 0.5f);

            hitWall = true;
            return hit.point + (hit.normal * avoidDistance);
        }
        else
        {
            //Debug.DrawRay(character.transform.position, character.linearVelocity.normalized * lookahead, Color.green, 0.5f);
            hitWall = false;
            return base.getTargetPosition();
        }
    }
}
