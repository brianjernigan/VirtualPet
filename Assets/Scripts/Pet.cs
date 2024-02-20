using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pet
{
    private string _name;
    private int _hunger;
    private int _boredom;
    private int _fatigue;

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public int Hunger
    {
        get => _hunger;
        set => _hunger = value;
    }

    public int Boredom
    {
        get => _boredom;
        set => _boredom = value;
    }

    public int Fatigue
    {
        get => _fatigue;
        set => _fatigue = value;
    }

    public void Eat(int cals)
    {
        _hunger -= cals;
    }

    public void Play(int exercise)
    {
        _boredom -= exercise;
    }

    public void Rest(int sleep)
    {
        _fatigue -= sleep;
    }
}
