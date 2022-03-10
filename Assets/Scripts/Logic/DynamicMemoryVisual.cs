using UnityEngine;
using UnityEngine.UI;

public class DynamicMemoryVisual : MonoBehaviour
{
    [SerializeField] private Text textValue;
    [SerializeField] private DynamicMemory memory;


    void Update()
    {
        int busyCells = 0;
        foreach (bool cell in memory.Memory)
            if (cell) busyCells++;
        textValue.text = $"{memory.Memory.Length + "/" + busyCells}";
    }
}
