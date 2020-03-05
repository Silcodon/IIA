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

    void Update()
    {

        // get sensor data
        resouceAngle = resourcesDetector.GetAngleToClosestResource();
        resourceValue = weightResource * resourcesDetector.GetGaussianOutput();
        //META1
        AngleWall = blockDetector.GetAngleToClosestObstacle() + 180f;
        WallValue = weightWall * blockDetector.GetGaussianOutput();

        //META2
        if (WallValue > max)
        {
            WallValue = max;
        }
        else if (WallValue < min)
        {
            WallValue = min;
        }
        if (resourceValue > max)
            resourceValue = max;
        else if (resourceValue < min)
            resourceValue = min;
        //FIM META2


        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards

        applyForce(AngleWall, WallValue); //Fugir da parede(META1)


    }


}


