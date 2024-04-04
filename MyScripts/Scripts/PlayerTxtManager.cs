using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTxtManager : MonoBehaviour
{
    [Header ("Texts")]
    public TMPro.TextMeshProUGUI bombText;
    public TMPro.TextMeshProUGUI flameText;
    public TMPro.TextMeshProUGUI speedText;
    public TMPro.TextMeshProUGUI kickBomb;

    // Set new Bomb text
    public void SetBombText(int numberOfBombs) 
    {
        bombText.text = "Bombs = " + numberOfBombs;
    }

    // Set new flame text
    public void SetFlameText(int flameSize)
    {
        flameText.text = "Flame = " + flameSize;
    }

    // Set new speed text
    public void SetSpeedText(int speed) {
        speedText.text = "Speed = " + speed;
    }

    // Set new kick text
    public void SetKickText()
    {
        kickBomb.text = "Kick = Yes";
    }
}
