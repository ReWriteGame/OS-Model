using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMemory : MonoBehaviour
{
    [SerializeField]private Process[] processes;
    [SerializeField]private int memorySize;
    [SerializeField] private bool[] memory;

    public bool[] Memory => memory;

    public Process[] Processes => processes;

    private void Awake()
    {
        memory = new bool[memorySize];

       
    }

    public void SetProcessesInMemory()
    {

        for (int i = 0; i < processes.Length; i++)
        {
            if (processes[i].positionInMemory == -1)
            {
                int freeSpace = SearchFreeSpace(processes[i].Size);
                if (freeSpace == -1)
                {
                    processes[i].GetComponent<Destroyable>().Destroy();
                    break;
                }
                
                processes[i].positionInMemory = freeSpace;

                for (int ii = freeSpace; ii < (freeSpace + processes[i].Size); ii++)
                {
                    memory[ii] = true;
                }


            }
        }

    }

    private int SearchFreeSpace(int size)
    {
        for (int i = 0; i < memory.Length; i++)
        {
            for (int ii = 0,freeCell = 0; ii < size; ii++)
            {
                if ((i + ii) < memory.Length && !memory[i + ii])
                {
                    freeCell++;
                    if (freeCell == size) return i;
                }
            }
        }
        return -1;
    }

    private void CheckState()
    {
        foreach (Process process in processes)
        {
            if (process.CurrentState == ProcessState.Finished)
            {
                for (int i = 0; i < process.Size; i++)
                {
                    memory[process.positionInMemory + i] = false;
                }

                process.GetComponent<Destroyable>().Destroy();
            }
            
        }
    }


    public void GetAllProcesses()
    {
        processes = GetComponentsInChildren<Process>();
     
    }

    private void FixedUpdate()
    {
        GetAllProcesses();

        SetProcessesInMemory();
        CheckState();
    }
}
