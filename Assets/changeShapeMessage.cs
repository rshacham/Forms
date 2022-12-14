using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class changeShapeMessage : MonoBehaviour
{
    [SerializeField] private PlayersManager playersManager;
    [SerializeField] private TextMeshProUGUI shapeMessage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playersManager.HasChangedShape)
        {
            shapeMessage.gameObject.SetActive(true);
        }
        if (playersManager.HasChangedShape)
        {
            shapeMessage.gameObject.SetActive(false);
        }
        
    }
}
