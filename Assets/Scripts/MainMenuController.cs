//////////////////////////////////////////////
//Assignment/Lab/Project: Virtual Pet
//Name: Brian Jernigan
//Section: SGD.213.2172
//Instructor: Brian Sowers
//Date: 02/26/2024
/////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnClickStartButton()
    {
        SceneManager.LoadScene("VirtualPet");
    }
}
