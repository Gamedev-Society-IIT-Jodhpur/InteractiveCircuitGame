using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScoringScript
{
     static List<int> errors ;
     static List<double> penalty;
     
    static double score = 0.0;
    /*
     ClassNo Error Class                            Penality Score
     0       Solder-Break                           10
     1       Non-stand.                             10
     2       Forgets_to                             0
             buy  items   
     3       Doesn't Meet Specs                     20
     4       Circuit Doesn't match in Tinker        20
     5       Component Damage                       20
          
     */
    public static void InitializeScoring()
    {
        int[] inError = { 0, 0, 0, 0, 0, 0 };
        double[] inPenality = { 10, 10, 0, 20, 20, 20 };
        errors = new List<int>(inError);
        penalty = new List<double>(inPenality);
    }
    public static void UpdateError(int classNo , int incrementBy =1)
    {
        try
        {
            errors[classNo] += incrementBy;
        }
        catch (Exception e)
        {
            Debug.Log("Invalid class");
        }
        
    }
    public static void UpdatePenality(int classNo, int incrementBy = 1)
    {
        
        try
        {
            penalty[classNo] += incrementBy;
        }
        catch (Exception e)
        {
            Debug.Log("Invalid class");
        }
    }

    public static void CalcScore()
    {
        double TotalPenalty = 0.0;
        double Time = Timer.currentTime;
        double money = MoneyAndXPData.money;
        for(int i = 0; i < errors.Count; i++)
        {
            TotalPenalty += (errors[i]*penalty[i]);
        }
        double weight1 = 0.6; // For getting it right 
        double weight2 = 0.2; // For fast solving
        double weight3 = 0.2; // Respecting Budget constraint
        score = Sigmoid(TotalPenalty) * weight1 + Sigmoid(Time , 100 , 0.5 , -10) * weight2 + Sigmoid(money) * weight3;
        // TO-DO figure out hyperparameters for sigmoids of totalpenalty as well as money contraint and customize weights
    }

    static double Sigmoid( double x ,double k=1 , double a=0 , double b=-1 )
    {
        return (1/(1+Math.Exp(a+b*x)));
    }

}