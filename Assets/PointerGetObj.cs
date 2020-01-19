using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerGetObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MagicLeapTools.Pointer pointer = gameObject.GetComponent<MagicLeapTools.Pointer>(); 
        if (pointer.Target != null)  {
            Debug.Log("got game object" ); 
            Debug.Log("got game object" + pointer.Target.tag); 
        } else {
            Debug.Log("NA"); 
        }
        
    }





}
