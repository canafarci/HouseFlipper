using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisCollisionChecker : MonoBehaviour
{
    public bool CollidingWithOtherBlocks = false;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("TetrisDraggable"))
        {
            CollidingWithOtherBlocks = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("TetrisDraggable"))
        {
            CollidingWithOtherBlocks = false;
        }
    }

    public IEnumerator DelayedCheckCollision()
    {
        yield return new WaitForSeconds(0.2f);
        if (CollidingWithOtherBlocks)
            Destroy(gameObject);
    }
}
