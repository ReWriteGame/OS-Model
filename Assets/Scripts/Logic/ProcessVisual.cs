using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer ready;
    [SerializeField] private SpriteRenderer waiting;
    [SerializeField] private SpriteRenderer running;
    [SerializeField] private Text textValue;

    private Process process;
    private void Awake()
    {
        process = GetComponentInParent<Process>();
        ready.enabled = false;
        waiting.enabled = false;
        running.enabled = false;
    }

    void Update()
    {
        switch (process.CurrentState)
        {
            case ProcessState.Ready :
                ready.enabled = true;
                waiting.enabled = false;
                running.enabled = false;
                break;
            case ProcessState.Running :
                ready.enabled = false;
                waiting.enabled = false;
                running.enabled = true;
                break;
            case ProcessState.Waiting :
                ready.enabled = false;
                waiting.enabled = true;
                running.enabled = false;
                break;
        }

        textValue.text = $"{process.BurstTime + "/" + process.ExecutingTime}";
    }
}
