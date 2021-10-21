using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject target;

    void LateUpdate()
    { 
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
        
        if (target != null)
        {
            this.transform.position = new Vector3(target.transform.position.x, this.transform.position.y, this.transform.position.z);
        }
    }
}
