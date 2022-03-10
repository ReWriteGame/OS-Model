using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum ProcessState
{
  Ready,
  Waiting,
  Running,
  Finished
}

public class Process : MonoBehaviour
{
  [SerializeField] public int positionInMemory = -1;

  
  [SerializeField] private uint id;
  [SerializeField] private string name;
  [SerializeField] private int size;


  [SerializeField] private ProcessState currentState = ProcessState.Ready;

  public int Size => size;
  public ProcessState CurrentState => currentState;
  public uint ExecutingTime => executingTime;
  public uint BurstTime => burstTime;
  
  [SerializeField] private uint executingTime;
  [SerializeField] private uint burstTime = 0;


  private void Awake()
  {
    StartCoroutine(SetNewTaskCor());
    GenerateTask();
    size = Random.Range(1,10);
    positionInMemory = -1;
  }

  public void Execute()
  {
    if (burstTime < executingTime) burstTime++;
    currentState = burstTime < executingTime ? ProcessState.Running : ProcessState.Waiting;
  }

  public void FinishWork()
  {
    currentState = ProcessState.Finished;
    StopCoroutine(SetNewTaskCor());
  }

  private void GenerateTask()
  {
    if (currentState == ProcessState.Ready || currentState == ProcessState.Waiting)
    {
      executingTime = (uint) Random.Range(1, 10);
      burstTime = 0;
    }
  }

  private IEnumerator SetNewTaskCor()
  {
    while (true)
    {
      if (CurrentState == ProcessState.Waiting)
      {
        GenerateTask();
        currentState = ProcessState.Ready;
      }
      yield return new WaitForSeconds(5);
    }
  }

 
}
