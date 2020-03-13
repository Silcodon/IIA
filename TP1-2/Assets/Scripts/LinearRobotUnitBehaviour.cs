using System;
using System.Collections;
using UnityEngine;

public class LinearRobotUnitBehaviour : RobotUnit
{
    public float weightResource;
    public float weightWall;
    public float resourceValue;
    public float resouceAngle;
    public float AngleWall;
    public float WallValue;
    public float max;
    public float min;
    public float limiar_max_blocks;
    public float limiar_min_blocks;
    public float limiar_max_resources;
    public float limiar_min_resources;
    public enum Activation_type {None, Linear, Log, Gaussian };
    public Activation_type Type_Blocks;
    public Activation_type Type_Resources;


    void Update()
    {
        // get sensor data
        resouceAngle = resourcesDetector.GetAngleToClosestResource();

        AngleWall = blockDetector.GetAngleToClosestObstacle()+180f;

        //META2
        if (blockDetector.strength > limiar_max_blocks)
        {
            blockDetector.strength = min;
        }
        else if (blockDetector.strength < limiar_min_blocks)
        {
            blockDetector.strength = min;
        }
        else
        {
            switch (Type_Blocks)
            {
                case Activation_type.Linear:
                    WallValue = blockDetector.GetLinearOuput();
                    break;
                case Activation_type.Gaussian:
                    WallValue = blockDetector.GetGaussianOutput();
                    break;
                case Activation_type.Log:
                    WallValue = blockDetector.GetLogaritmicOutput();
                    break;
            }
        }
        if (resourcesDetector.strength > limiar_max_resources)
            resourcesDetector.strength = min;
        else if (resourcesDetector.strength < limiar_min_resources)
            resourcesDetector.strength = min;
        else
        {
            switch (Type_Resources)
            {
                case Activation_type.Linear:
                    resourceValue = resourcesDetector.GetLinearOuput();
                    break;
                case Activation_type.Gaussian:
                    resourceValue = resourcesDetector.GetGaussianOutput();
                    break;
                case Activation_type.Log:
                    resourceValue = resourcesDetector.GetLogaritmicOutput();
                    break;
            }
        }

        if (WallValue > max)
        {
            WallValue = max;
        }
        if(resourceValue > max)
        {
            resourceValue = max;
        }

        if (WallValue < min)
        {
            WallValue = min;
        }
        if (resourceValue < min)
        {
            resourceValue = min;
        }

        WallValue = weightWall * WallValue;
        resourceValue = weightResource * resourceValue;

        //FIM META2

        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards

        applyForce(AngleWall, WallValue); //Fugir da parede
        

    }


}






