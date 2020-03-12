using System;
using System.Collections;
using UnityEngine;

public class GaussianRobotUnitBehaviour : RobotUnit
{
    public float weightResource;
    public float weightWall;
    public float resourceValue;
    public float resouceAngle;
    public float AngleWall;
    public float WallValue;
    public float max;
    public float min;
    public float limiar_max;
    public float limiar_min;

    void Update()
    {

        // get sensor data
        resouceAngle = resourcesDetector.GetAngleToClosestResource();
        resourceValue = weightResource * resourcesDetector.GetGaussianOutput();
        //META1
        AngleWall = blockDetector.GetAngleToClosestObstacle() + 180f;
        WallValue = blockDetector.GetGaussianOutput();

        //META2
        if (WallValue > limiar_max)
        {
            WallValue = min;
        }
        else if (WallValue < limiar_min)
        {
            WallValue = min;
        }
        if (resourceValue > limiar_max)
            resourceValue = min;
        else if (resourceValue < limiar_min)
            resourceValue = min;

        if (WallValue > max)
        {
            WallValue = max;
        }

        WallValue = weightWall * WallValue;
        //FIM META2


        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards

        applyForce(AngleWall, WallValue); //Fugir da parede(META1)


    }


}


