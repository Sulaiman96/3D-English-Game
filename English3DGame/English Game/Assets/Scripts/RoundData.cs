using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RoundData 
{
    [SerializeField] public string name;
    [SerializeField] public int timeLimitInSeconds;
    [SerializeField] public int pointsAddedForCorrectAnswer;
    [SerializeField] public QuestionData[] questions;

}
