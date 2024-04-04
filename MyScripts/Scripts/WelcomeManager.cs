using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeManager : MonoBehaviour
{
    [Header("UI element")]
    public GameObject rulesPanel;
    
    // Start the game
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Open the rules panel
    public void OpenRules()
    {
        rulesPanel.SetActive(true); 
    }

    // Close the rules panel
    public void CloseRulses()
    {
        rulesPanel.SetActive(false);    
    }
}
