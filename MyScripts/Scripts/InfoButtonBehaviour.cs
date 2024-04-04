using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoButtonBehaviour : MonoBehaviour
{
    public GameObject InfoPanel;
    public TMP_Text ButtonText;

    public void OpenPanelControl()
    {
        if (InfoPanel != null)
        {
            if (!InfoPanel.activeSelf)
            {
                InfoPanel.SetActive(true);
                ButtonText.text = "Close Info";
            }
            else
            {
                InfoPanel.SetActive(false);
                ButtonText.text = "Info";
            }
        }
    }

}
