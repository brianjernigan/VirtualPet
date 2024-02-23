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
        else
        {
            _hunger = 0;
        }
    }
    
    public void IncreaseHunger(int perSecondValue, float maxValue)
    {
        if (_hunger + (perSecondValue * Time.deltaTime) <= maxValue)
        {
            _hunger += perSecondValue * Time.deltaTime;
        }
        else
        {
            _hunger = maxValue;
        }
    }

    public void Play(int exercise)
    {
        if (_boredom - exercise >= 0)
        {
            _boredom -= exercise;
        }
        else
        {
            _boredom = 0;
        }
    }
    
    public void IncreaseBoredom(int perSecondValue, float maxValue)
    {
        if (_boredom + (perSecondValue * Time.deltaTime) <= maxValue)
        {
            _boredom += perSecondValue * Time.deltaTime;
        }
        else
        {
            _boredom = maxValue;
        }
    }

    public void Rest(int sleep)
    {
        if (_fatigue - sleep >= 0)
        {
            _fatigue -= sleep;    
        }
        else
        {
            _fatigue = 0;
        }
    }
    
    public void IncreaseFatigue(int perSecondValue, float maxValue)
    {
        if (_fatigue + (perSecondValue * Time.deltaTime) <= maxValue)
        {
            _fatigue += perSecondValue * Time.deltaTime;
        }
        else
        {
            _fatigue = maxValue;
        }
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
