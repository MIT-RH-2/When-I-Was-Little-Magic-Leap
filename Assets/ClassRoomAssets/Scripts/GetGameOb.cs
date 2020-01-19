using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicLeapTools; 

public class GetGameOb : MonoBehaviour
{
	 public Pointer pointer;
    // Start is called before the first frame update
    void Start()
    {

    	pointer = GetComponent<Pointer>();


        
    }

    // Update is called once per frame
    void Update()
    {
    	Debug.Log("Pointer is pointing at" + pointer.Target); 
    	
    }
}
