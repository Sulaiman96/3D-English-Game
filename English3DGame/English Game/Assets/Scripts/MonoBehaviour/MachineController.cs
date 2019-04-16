using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MachineController : MonoBehaviour
{
    public TMP_Text interactionText;
    public GameObject questionPanel;
    private GameController gameController;
    private bool roundHasNotStarted;
    private bool inRange;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        interactionText.GetComponent<TextMeshProUGUI>().enabled = false;
        roundHasNotStarted = false;
        inRange = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        interactionText.GetComponent<TextMeshProUGUI>().enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        interactionText.GetComponent<TextMeshProUGUI>().enabled = false;
        roundHasNotStarted = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && !roundHasNotStarted && inRange)
        {
            interactionText.GetComponent<TextMeshProUGUI>().enabled = false;
            roundHasNotStarted = true;
            gameController.StartTheGame();
            questionPanel.SetActive(true);
        }
    }
}
