
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
            Debug.Log("got game object" + pointer.Target.tag.ToString()); 
            TagFound(pointer.Target.tag.ToString()); 
        } else {
            Debug.Log("nothing found"); 
        }
        
    }



    void TagFound(string obTag) {
        Debug.Log("This is the ob tag!" + obTag); 

switch (obTag)
    {
        case "globe":
            Debug.Log("Globe");
            storyText.text = "A globe! In elementary school, you never really knew what one did. You just thought it was a stationary ball. You'd stand next to it and get it spinning as fast as you could...until your teacher asked you to stop!"; 
            break;
        case "backpack":
            storyText.text = "Remember your childhood backpack? Nothing much was in it because there wasn't a lot of homework back then. But you did keep snackbars hidden in the pockets in case you got hungry on the bus home."; 
            break;
        case "notebook":
            storyText.text = "School days were long and taking notes was hard! Lots of writing for little hands can be quite tiring. You got really good at doodling bunnies and flowers in the margin to encourage yourself to keep writing.";            
            break;
        case "pencil":
            storyText.text = "Your subject in elementary school was art. With art, the world was limitless. You could draw anything you wanted! Dragons, castles, you winning the World Cup. Imagination ran wild!";
            break;
        case "calculator":
            storyText.text = "The best discovery you and your friends made was learning that you could spell words with your calculator! Math lessons got a lot more fun when you found new ways to say 07734 to your friends";
            break;
        default:
            Debug.Log ("Other");
            break;
    }

    }


}

