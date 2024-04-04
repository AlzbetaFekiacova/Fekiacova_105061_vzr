using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header ("Players")]
    public GameObject player1;
    public GameObject player2;
    [Header("UI  objects")]
    public GameObject panel;
    public TMPro.TextMeshProUGUI winningText;
     
    // Check who won at the end of the game
    public void CheckWinState()
    {
        if (!player1.activeSelf)
        {
            winningText.text = "Player 2 won!";
            panel.SetActive(true);
        }
        else if (!player2.activeSelf)
        {
            winningText.text = "Player 1 won!";
            panel.SetActive(true);
        }
        else if(!player1.activeSelf && !player2.activeSelf)
        {
            winningText.text = "No one won!";
            panel.SetActive(true);
        }

    }

    // Restart the game
    public void RestartGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
