using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatedAnnealingOptimiser : OptimisationAlgorithm
{
    public int MaxNumberOfIterations = 100;
    private int CurrentNumberOfIterations = 1;
    private List<int> newSolution = null;
    private int CurrentSolutionCost;
    public float Temperature;
    float probOfAcceptance;
    private int newSolutionCost;
    private float e = 2.718281828459045235360287f;
    private float zero = Mathf.Pow(10, -6);
    string fileName = "Assets/Logs/" + System.DateTime.Now.ToString("ddhmmsstt") + "_SimulatedAnnealingOptimiser.csv";


    protected override void Begin()
    {

        CreateFileSA(fileName);
        CurrentSolution = GenerateRandomSolution(targets.Count);
        CurrentSolutionCost = Evaluate(CurrentSolution);
    }

    protected float TemperatureSchedule(float Temperature)
    {
        Temperature = -Mathf.Log(Temperature);
        return Temperature;
    }

    protected override void Step()
    {
        if (CurrentNumberOfIterations < MaxNumberOfIterations && Temperature>0) 
        {
            newSolution = GenerateNeighbourSolution(CurrentSolution);
            newSolutionCost = Evaluate(newSolution);
            probOfAcceptance = Mathf.Pow(e,(CurrentSolutionCost-newSolutionCost)/Temperature);
            if(newSolutionCost<=CurrentSolutionCost || probOfAcceptance > Random.Range(0, 1))
            {
                CurrentSolution = newSolution;
                CurrentSolutionCost = newSolutionCost;
            }
            Temperature = TemperatureSchedule(Temperature);

        }
        else
        {
            bestSequenceFound = CreateSequenceFromSolution(CurrentSolution);
            TargetSequenceDefined = true;
        }

        //DO NOT CHANGE THE LINES BELLOW
        AddInfoToFile(fileName, CurrentNumberOfIterations, CurrentSolutionCost, CurrentSolution, Temperature);
        CurrentNumberOfIterations++;

    }


}
