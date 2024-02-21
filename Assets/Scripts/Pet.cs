using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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

    public void Feed(int cals)
    {
        if (_hunger - cals >= 0)
        {
            _hunger -= cals;
        }
    }

    public void Play(int exercise)
    {
        _boredom -= exercise;
    }

    public void Rest(int sleep)
    {
        _fatigue -= sleep;
    }

    public Pet(string name)
    {
        _name = name;
        _hunger = 0;
        _boredom = 0;
        _fatigue = 0;
    }

    public override string ToString()
    {
        return $"Name: {_name}, Hunger: {_hunger}, Boredom: {_boredom}, Fatigue: {_fatigue}";
    }
}
