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

    private void Start()
    {
        Type_Blocks = Activation_type.Linear;
        Type_Resources = Activation_type.Linear;
    }

    void Update()
    {

        // get sensor data
        resouceAngle = resourcesDetector.GetAngleToClosestResource();

        AngleWall = blockDetector.GetAngleToClosestObstacle()+180f;

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


        //META2
        if (WallValue > limiar_max_blocks)
        {
            WallValue = min;
        }
        else if (WallValue < limiar_min_blocks)
        {
            WallValue = min;
        }
        if (resourceValue > limiar_max_resources)
            resourceValue = min;
        else if (resourceValue < limiar_min_resources)
            resourceValue = min;

        if (WallValue > max)
        {
            WallValue = max;
        }

        WallValue = weightWall * WallValue;
        resourceValue = weightResource * resourceValue;

        //FIM META2

        // apply to the ball
        applyForce(resouceAngle, resourceValue); // go towards

        applyForce(AngleWall, WallValue); //Fugir da parede
        

    }


}






