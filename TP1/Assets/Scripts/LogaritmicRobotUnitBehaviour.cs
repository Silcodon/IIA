using System;
using System.Collections;
using UnityEngine;

public class LogaritmicRobotUnitBehaviour : RobotUnit
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
        resourceValue = weightResource * resourcesDetector.GetLogaritmicOutput();
        //META1
        AngleWall = blockDetector.GetAngleToClosestObstacle() + 180f;
        WallValue = weightWall * blockDetector.GetLogaritmicOutput();

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


