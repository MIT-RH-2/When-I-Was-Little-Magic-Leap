using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;


public class DynamicBeam : MonoBehaviour
{


    
public GameObject controller; 
private LineRenderer controllerBeam; 

public Color startColor; 
public Color endColor; 



    // Start is called before the first frame update
    void Start()
    {
        controllerBeam = GetComponent<LineRenderer>(); 
        controllerBeam.startColor = Color.green; 
        controllerBeam.startColor = Color.blue; 

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = controller.transform.position;
        transform.rotation = controller.transform.rotation; 

        RaycastHit hit; 
        
        
        if(Physics.Raycast(transform.position, transform.forward, out hit )) {

            controllerBeam.useWorldSpace = true; 
            controllerBeam.SetPosition(0, transform.position); 
            controllerBeam.SetPosition(1, hit.point);
        }

        else {
        
            controllerBeam.useWorldSpace = false; 
            controllerBeam.SetPosition(0,transform.position); 
            controllerBeam.SetPosition(1, Vector3.forward * 5); 
        }

    }
}
