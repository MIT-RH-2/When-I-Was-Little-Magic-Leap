using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class PointerGetObj : MonoBehaviour
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
