using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueTextBox;
    public TextMeshProUGUI continueText;
    public TMP_Text npcShout;
    public TMP_Text interactText;
    public string[] sentences;
    private int index;
    public float dialogueWrittenSpeed = 0.02f;
    private bool textHasBeenWritten;
    private bool inRange;
    private bool hasAlreadyInteracted;

    private void Start()
    {
        textHasBeenWritten = false;
        inRange = false;
        hasAlreadyInteracted = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
        interactText.GetComponent<TextMeshProUGUI>().enabled = inRange && !hasAlreadyInteracted;
        dialogueTextBox.GetComponent<TextMeshProUGUI>().enabled = true;
        npcShout.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
        interactText.GetComponent<TextMeshProUGUI>().enabled = false;
        dialogueTextBox.GetComponent<TextMeshProUGUI>().enabled = false;
        continueText.GetComponent<TextMeshProUGUI>().enabled = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && !textHasBeenWritten && inRange)
        {
            hasAlreadyInteracted = true;
            interactText.GetComponent<TextMeshProUGUI>().enabled = false;
            StartCoroutine(TypeMySentence());
        }

        continueText.GetComponent<TextMeshProUGUI>().enabled = textHasBeenWritten && inRange;

        //Debug.Log(continueText.IsActive());

        if (Input.GetKeyDown(KeyCode.Space) && textHasBeenWritten)
        {
            NewSentence();
        }
    }

    IEnumerator TypeMySentence()
    {
        textHasBeenWritten = false;
        foreach (char letter in sentences[index].ToCharArray())
        {
            dialogueTextBox.text += letter;
            yield return new WaitForSeconds(dialogueWrittenSpeed);
        }
        textHasBeenWritten = true;
    }

    public void NewSentence()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            dialogueTextBox.text = " ";
            StartCoroutine(TypeMySentence());
        }
        else
        {
            dialogueTextBox.text = " ";
            textHasBeenWritten = false;
        }
    }
}
