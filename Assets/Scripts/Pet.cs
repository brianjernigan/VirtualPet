//////////////////////////////////////////////
//Assignment/Lab/Project: Virtual Pet
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 02/26/2024
/////////////////////////////////////////////

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet
{
    private string _name;
    private float _hunger;
    private float _boredom;
    private float _fatigue;
    
    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public float Hunger
    {
        get => _hunger;
        set => _hunger = value;
    }

    public float Boredom
    {
        get => _boredom;
        set => _boredom = value;
    }

    public float Fatigue
    {
        get => _fatigue;
        set => _fatigue = value;
    }

    public bool IsFed { get; set; }
    public bool HasPlayed { get; set; }
    public bool IsRested { get; set; }

    // Constructor
    public Pet(string name)
    {
        _name = name;
    }
    
    // Abstracted methods for increasing or decreasing stats
    private void DecreaseStat(ref float stat, float decreaseAmount)
    {
        stat = Mathf.Max(0, stat - decreaseAmount);
    }

    private void IncreaseStat(ref float stat, float increaseAmount, float maxValue)
    {
        var deltaSecond = increaseAmount * Time.deltaTime;
        stat = Mathf.Min(maxValue, stat + deltaSecond);
    }

    // Implementation of specific stat methods 
    // Must pass backing variable by reference
    public void Feed(int cals) => DecreaseStat(ref _hunger, cals);
    public void Play(int exercise) => DecreaseStat(ref _boredom, exercise);
    public void Rest(int sleep) => DecreaseStat(ref _fatigue, sleep);

    public void IncreaseHunger(float perSecondValue, float maxValue) =>
        IncreaseStat(ref _hunger, perSecondValue, maxValue);

    public void IncreaseBoredom(float perSecondValue, float maxValue) =>
        IncreaseStat(ref _boredom, perSecondValue, maxValue);

    public void IncreaseFatigue(float perSecondValue, float maxValue) =>
        IncreaseStat(ref _fatigue, perSecondValue, maxValue);

    // For debugging
    public override string ToString() => $"Name: {_name}, Hunger: {_hunger}, Boredom: {_boredom}, Fatigue: {_fatigue}";
}
