using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriangleObject : ColorfulObject
{

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Movement>())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
