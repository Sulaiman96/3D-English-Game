using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueTextBox;
    public TextMeshProUGUI continueText; 
    public string[] sentences;
    private int index;
    public float dialogueWrittenSpeed = 0.02f;
    private bool textHasBeenWritten;

    private void Start()
    {
       StartCoroutine(TypeMySentence());
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

    private void Update()
    {

        continueText.GetComponent<TextMeshProUGUI>().enabled = textHasBeenWritten;
        Debug.Log(continueText.IsActive());

        if (Input.GetKeyDown(KeyCode.Space) && textHasBeenWritten)
        {
            NewSentence();
        }
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
