using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : MonoBehaviour
{
    [SerializeField] public RoundData[] allRoundData; //can extend for multiple rounds.

    public RoundData GetCurrentRoundData() {
        return allRoundData[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
