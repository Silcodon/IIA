using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;


public class HillClimberOptimiser : OptimisationAlgorithm
{
    public int MaxNumberOfIterations = 100;
    private int CurrentNumberOfIterations = 1;
    private List<int> solution = null;
    private int bestCost;

    string fileName = "Assets/Logs/" + System.DateTime.Now.ToString("ddhmmsstt") + "_HillClimberOptimiser.csv";


    protected override void Begin()
    {

        CreateFile(fileName);
        CurrentSolution = GenerateRandomSolution(targets.Count);
        bestCost = Evaluate(CurrentSolution);

    }

    protected override void Step()
    {
        if (CurrentNumberOfIterations < MaxNumberOfIterations)
        {
            solution = GenerateNeighbourSolution(CurrentSolution);
            if (Evaluate(solution) <= bestCost)
            {
                CurrentSolution = solution;
                bestCost = Evaluate(solution);
            }

        }
        else
        {
            bestSequenceFound = CreateSequenceFromSolution(CurrentSolution);
            TargetSequenceDefined = true;
        }

        //DO NOT CHANGE THE LINES BELLOW
        AddInfoToFile(fileName, CurrentNumberOfIterations, this.Evaluate(base.CurrentSolution), base.CurrentSolution);
        CurrentNumberOfIterations++;

    }

   

}
