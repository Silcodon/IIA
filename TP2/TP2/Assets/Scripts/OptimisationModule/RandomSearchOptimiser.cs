using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;


public class RandomSearchOptimiser : OptimisationAlgorithm
{
    public int MaxNumberOfIterations = 100;
    private int CurrentNumberOfIterations = 1;


    private string fileName = "Assets/Logs/" + System.DateTime.Now.ToString("ddhmmsstt") + "_RandomSearchOptimiser.csv";


    protected override void Begin()
    {

        CreateFile(fileName);
        bestSequenceFound = new List<GameObject>();

    }

    protected override void Step()
    {
        if(CurrentNumberOfIterations < MaxNumberOfIterations)
        {
            CurrentSolution = GenerateRandomSolution(targets.Count);

        }
        else
        {
            TargetSequenceDefined = true;
        }


        //DO NOT CHANGE THE LINES BELLOW
        AddInfoToFile(fileName, CurrentNumberOfIterations, this.Evaluate(CurrentSolution), CurrentSolution);
        CurrentNumberOfIterations++;

    }



}
