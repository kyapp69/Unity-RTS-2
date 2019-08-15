﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour
{
    public  int Cash { get; set; }
    public int Day { get; set; }
    public float PopulationCurrent { get; set; }
    public float PopulationCeiling { get; set; }
    public int JobsCurrent { get; set; }
    public int JobsCeiling { get; set; }
    public float Food { get; set; }

    public int[] buildingCounts = new int[5];
    private UIController uiController;

    // Start is called before the first frame update
    void Start() {

        for (int i = 0; i < 4; i++)
        {
            if (buildingCounts[i] < 1)
            {
                buildingCounts[i]++;
            }
        }
        uiController = GetComponent<UIController>();
        Day = 1;
        Cash = 50;
        Food = 0;
        uiController.UpdateCityData();
        uiController.UpdateDayCount();
    }

    public void EndTurn()
    {
        if (!PauseMenu.GameIsPaused)
        {
            Day++;
            CalculatePopulation();
            CalculateJobs();
            CalculateFood();
            CalculateCash();
            uiController.UpdateCityData();
            uiController.UpdateDayCount();
            Debug.Log("Day ended.");
            Debug.LogFormat
                ("Jobs: {0}/{1}, Cash: {2}, pop: {3}/{4}, Food: {5}", JobsCurrent, JobsCeiling, Cash, PopulationCurrent, PopulationCeiling, Food);
        }
    }

    void CalculateJobs()
    {
        JobsCeiling = buildingCounts[3] * 10;
        JobsCurrent = Mathf.Min((int)PopulationCurrent, JobsCeiling);
    }
    void CalculateCash()
    {
        Cash += JobsCurrent * 2;
    }

    public void DepositCash(int cash)
    {
        Cash += cash;
    }

    void CalculateFood()
    {
        Food += buildingCounts[2] * 4f;
    }
    void CalculatePopulation()
    {
        PopulationCeiling = buildingCounts[1] * 5;
        if (Food >= PopulationCurrent && PopulationCurrent < PopulationCeiling)
        {
            Food -= PopulationCurrent * .25f;
            PopulationCurrent = Mathf.Min(PopulationCurrent += Food * .25f, PopulationCeiling);
        }
        else if (Food < PopulationCurrent)
        {
            PopulationCurrent -= (PopulationCurrent - Food) * .5f;
        }
    }
}
