using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

public class FpsRandom : MonoBehaviour
{
    [SerializeField] private Text FpsText;
    float time;
    float cooltime = 1f;
    private void Start()
    {
        FpsText.text = "FPS : " + 146.1f;
    }
    private void Update()
    {
        time += Time.deltaTime;
        if (time > cooltime)
        {
            float baseNum = 145.5f;
            float random = Random.Range(0, 10) * 0.1f; // 0.0 ~ 0.9
            float randomNum = baseNum + random + Random.Range(0, 4);
            FpsText.text = "FPS : " + randomNum;
            time = 0f;
        }
    }
}

