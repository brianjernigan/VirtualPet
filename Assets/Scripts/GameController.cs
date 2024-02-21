using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private Button _adoptButton;
    [SerializeField] private GameObject _namingText;

    [Header("Bars")] 
    [SerializeField] private GameObject _hungerBar;
    [SerializeField] private GameObject _boredomBar;
    [SerializeField] private GameObject _fatigueBar;
    

    private Pet _newPet;

    private void Start()
    {
        UpdateButtonState();
        _nameInputField.onValueChanged.AddListener(delegate {UpdateButtonState();} );
    }
    
    private void UpdateButtonState()
    {
        _adoptButton.interactable = !string.IsNullOrEmpty(_nameInputField.text);
    }
    
    public void OnClickAdoptButton()
    {
        var petName = _nameInputField.text;
        _newPet = new Pet(petName);
        _nameInputField.interactable = false;
        _namingText.SetActive(false);
        Debug.Log(_newPet.ToString());
        InitializeBars();
    }

    private void UpdateBars()
    {
        _hungerBar.GetComponent<Slider>().value = _newPet.Hunger;
        _boredomBar.GetComponent<Slider>().value = _newPet.Boredom;
        _fatigueBar.GetComponent<Slider>().value = _newPet.Fatigue;
    }
    
    private void InitializeBars()
    {
        _hungerBar.GetComponent<Slider>().value = 100;
        _boredomBar.GetComponent<Slider>().value = 100;
        _fatigueBar.GetComponent<Slider>().value = 100;
    }

    public void OnClickFeedButton()
    {
        _newPet.Feed(10);
        UpdateBars();
    }

    public void OnClickPlayButton()
    {
        _newPet.Play(10);
        UpdateBars();
    }

    public void OnClickRestButton()
    {
        _newPet.Rest(10);
        UpdateBars();
    }
}
