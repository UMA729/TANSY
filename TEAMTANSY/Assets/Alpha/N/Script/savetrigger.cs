using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class savetrigger : MonoBehaviour
{
    private List<Vector2> positionHistory = new List<Vector2>();

    public int maxSavedPosition = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            SavePlayerPosition();
        }
    }

    void SavePlayerPosition()
    {
        positionHistory.Add(transform.position);

        if(positionHistory.Count > maxSavedPosition)
        {
            positionHistory.RemoveAt(0);
        }

        Debug.Log("Player Position Saved: " + transform.position);
    }

    public Vector2 GetSavePosition(int index)
    {
        if(index >= 0 && index < positionHistory.Count)
        {
            return positionHistory[index];
        }
        else
        {
            Debug.Log("Invalid index: " + index);
            return Vector2.zero;
        }
    }


    public void ClearHistory()
    {
        positionHistory.Clear();
    }
}
