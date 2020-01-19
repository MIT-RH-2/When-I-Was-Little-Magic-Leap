using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{

	public Material myMat; 
    // Start is called before the first frame update
    void Start()
    {

    	SetMaterial(myMat); 

        
    }


    public void SetMaterial(Material newMaterial)
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = newMaterial;
    }


   
}
