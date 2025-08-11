using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{

    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {
        
    }

    public void BasePoint(){
        Point();
    }

    protected virtual void Point(){

    }
}
