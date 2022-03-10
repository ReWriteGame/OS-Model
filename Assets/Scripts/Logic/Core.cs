using System;
using System.Collections;
using UnityEngine;

public enum CoreState
{
    Busy,
    Free
}
public class Core : MonoBehaviour
{

    [SerializeField] private float timeOneBeat = 1;
    [SerializeField] private bool playOnAwake = true;
    [SerializeField] private CoreState currentCoreState = CoreState.Free;
    [SerializeField] private Process currentProcess;

    public void SetCurrentProcess(Process process)
    {
        if (currentCoreState == CoreState.Free)
        {
            currentProcess = process;
            currentCoreState = CoreState.Busy;
        }
    }

    private Coroutine coroutine;
    
    public CoreState CurrentCoreState => currentCoreState;

    
    void Start()
    {
        if (playOnAwake) StartWork();
    }

    public void StartWork()
    {
        if (coroutine == null)
            coroutine = StartCoroutine(CoreCor());
    }
    public void StopWork()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
    }

    private IEnumerator CoreCor()
    {
        while (true)
        {
            if (currentProcess != null)
            {
                currentProcess.Execute();

                if (currentProcess.CurrentState == ProcessState.Waiting)
                {
                    currentProcess = null;
                    currentCoreState = CoreState.Free;
                }
            }
            else
            {
                currentCoreState = CoreState.Free;
            }

            yield return new WaitForSeconds(timeOneBeat);
        }
    }

   
}