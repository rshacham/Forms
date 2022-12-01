using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = GameManager.gameManager.getColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
