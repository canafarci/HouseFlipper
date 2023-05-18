using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackPositionCalculator : MonoBehaviour
{
    public Vector3 CalculatePosition(Stack<StackableItem> stack, StackableItem item)
    {
        float totalHeight = 0f;

        foreach (StackableItem si in stack)
        {
            totalHeight += si.ItemHeight;
        }

        return new Vector3(0f, totalHeight + item.ItemHeight / 2f, 0f);
    }
}
