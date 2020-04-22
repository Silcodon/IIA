using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulatedAnnealingOptimiser : OptimisationAlgorithm
{
    private List<int> newSolution = null;
    private int CurrentSolutionCost;
    public float Temperature;
    private float probOfAcceptance;
    private float e = 2.7182818284f;
    private int newSolutionCost;
    public enum Activation_type {tempsobre2, temp097};
    public Activation_type Escalonamento;
    private float zero = Mathf.Pow(10, -6);// numbers bellow this value can be considered zero.

    string fileName = "Assets/Logs/" + System.DateTime.Now.ToString("ddhmmsstt") + "_SimulatedAnnealingOptimiser.csv";


    protected override void Begin()
    {
        //Inicializar array solucao e calcular custo
        CreateFileSA(fileName);
        CurrentSolution = GenerateRandomSolution(targets.Count);
        CurrentSolutionCost = Evaluate(CurrentSolution);
    }


    //Funcoes de escalonamento de temperatura
    protected float TemperatureSchedule(float Temperature)
    {
        Temperature = Temperature * 0.97f;
        return Temperature;
    }

    protected float TemperatureSchedule2(float Temperature)
    {
        Temperature = Temperature / 2;
        return Temperature;
    }


    protected override void Step()
    {
        //Percorrer iteraçoes
        if (CurrentNumberOfIterations < MaxNumberOfIterations && Temperature > 0)
        {
            //A nova solucao vai ter uma probabilidade de ser aceite, sendo esta maior quando a temperatura for maior. Ela é logo aceite se tiver um custo menor.
            newSolution = GenerateNeighbourSolution(CurrentSolution);
            newSolutionCost = Evaluate(newSolution);
            probOfAcceptance = Mathf.Pow(e, (CurrentSolutionCost - newSolutionCost) / Temperature);
            if (newSolutionCost <= CurrentSolutionCost || probOfAcceptance > Random.Range(0, 1))
            {
                CurrentSolution = newSolution;
                CurrentSolutionCost = newSolutionCost;
            }
            //Funcao para fazer variar a temperatura ao longo do tempo.
            switch (Escalonamento)
            {
                case Activation_type.tempsobre2:
                    Temperature = TemperatureSchedule2(Temperature);
                    break;
                case Activation_type.temp097:
                    Temperature = TemperatureSchedule(Temperature);
                    break;
            }
            

        }
        else
        {
            bestSequenceFound = CreateSequenceFromSolution(CurrentSolution);
            TargetSequenceDefined = true;
        }
        //DO NOT CHANGE THE LINES BELLOW
        AddInfoToFile(fileName, base.CurrentNumberOfIterations, CurrentSolutionCost, CurrentSolution, Temperature);
        base.CurrentNumberOfIterations++;

    }


}
