using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;


public class HillClimberOptimiser : OptimisationAlgorithm
{

    private int bestCost;
    private List<int> newSolution = null;
    

    string fileName = "Assets/Logs/" + System.DateTime.Now.ToString("ddhmmsstt") + "_HillClimberOptimiser.csv";


    protected override void Begin()
    {
        //Inicializar solucao
        CreateFile(fileName);
        CurrentSolution = GenerateRandomSolution(targets.Count);
        bestCost = Evaluate(CurrentSolution);

    }

    protected override void Step()
    {
        //Percorrer iteracoes
        if (CurrentNumberOfIterations < MaxNumberOfIterations)
        {
            //Se a nova solucao encontrada for melhor que a anterior, substitui
            newSolution = GenerateNeighbourSolution(CurrentSolution);
            if (Evaluate(newSolution) <= bestCost)
            {
                CurrentSolution = newSolution;
                bestCost = Evaluate(CurrentSolution);
            }

        }
        else
        {
            bestSequenceFound = CreateSequenceFromSolution(CurrentSolution);
            TargetSequenceDefined = true;
        }

        //DO NOT CHANGE THE LINES BELLOW
        AddInfoToFile(fileName, base.CurrentNumberOfIterations, this.Evaluate(base.CurrentSolution), base.CurrentSolution);
        base.CurrentNumberOfIterations++;

    }

   

}
