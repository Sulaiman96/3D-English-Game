using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPCShout : MonoBehaviour
{
    public TMP_Text shoutText;
    private bool hasShoutedAlready;

    private void Start()
    {
        shoutText.GetComponent<TextMeshProUGUI>().enabled = false;
        hasShoutedAlready = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        shoutText.GetComponent<TextMeshProUGUI>().enabled |= !hasShoutedAlready;
        hasShoutedAlready = true;
    }

    private void OnTriggerExit(Collider other)
    {
        shoutText.GetComponent<TextMeshProUGUI>().enabled = false;
    }
}
