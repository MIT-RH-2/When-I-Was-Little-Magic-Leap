<<<<<<< HEAD:Assets/PointerGetObj.cs
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PointerGetObject : MonoBehaviour
{

    public Text storyText; 



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
            Debug.Log("got game object" + pointer.Target.tag.ToString()); 
            TagFound(pointer.Target.tag.ToString()); 
        } else {
            Debug.Log("NA"); 
        }
        
    }



    void TagFound(string obTag) {
        Debug.Log("This is the ob tag!" + obTag); 

switch (obTag)
    {
        case "globe":
            Debug.Log("One");
            break;
        case "backpack":
            Debug.Log("Two");
            Debug.Log("Two");
            break;
        default:
            Debug.Log ("Other");
            break;
    }


    }


}
=======
﻿using System.Collections;
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
>>>>>>> aecfbf9c125e1f5ab0765331dd7fec0f114045df:Assets/Scripts/PointerGetObj.cs
