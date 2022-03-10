using System;
using UnityEngine;

public class CPU : MonoBehaviour
{
    [SerializeField]private Core[] cores;
    [SerializeField]private DynamicMemory dynamicMemory;
    
    void Awake()
    {
        cores = GetComponentsInChildren<Core>();
    }

    public void SetTaskAllCPU()
    {
        foreach (Process process in dynamicMemory.Processes)
        {
            /*if(process.CurrentState == ProcessState.Ready)        
                SetProcess(process);*/
            
            SetProcess(FindShortProcess());
        }
        
       
    }

    private Process FindShortProcess()
    {
        Process shortProcess = null;
        foreach (Process process in dynamicMemory.Processes)
        {
            if (process.CurrentState == ProcessState.Ready)
            {
                shortProcess = process;
                break;
            }
        }
        
        foreach (Process process in dynamicMemory.Processes)
        {
            if (process.CurrentState == ProcessState.Ready)
            {
                if (process.ExecutingTime < shortProcess.ExecutingTime)
                    shortProcess = process;
            }
        }
        return shortProcess;
    }

    private void SetProcess(Process process)
    {
        foreach (Core core in cores)
        {
            if (core.CurrentCoreState == CoreState.Free)
            {
                core.SetCurrentProcess(process);
                break;
            }
        }
    }

    private void FixedUpdate()
    {
        foreach (Core core in cores)
        {
            if (core.CurrentCoreState == CoreState.Free)
            {
                SetTaskAllCPU();
                break;
            }
        }
    }
}
