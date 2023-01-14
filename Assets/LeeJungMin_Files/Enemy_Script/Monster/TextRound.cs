using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextRound : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textRoundCount;
    [SerializeField]
    WaveSystem wave;

    private void Update()
    {
        textRoundCount.text = $"Round {wave.CurrentWave} / {wave.MaxWave}";
    }

}
