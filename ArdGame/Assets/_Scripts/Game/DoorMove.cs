using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : Door
{
    [SerializeField]
    private Animator animator;

    public bool isOpen = false;
    public bool isPlayerInside = false;
    public bool isClosed = true;

    private bool isOpenning = false;

    public override void Move(bool input)
    {
        if (input && isOpen == false && isOpenning == false && isClosed)
        {
            Debug.Log("move DoorMove " + doorType);
            animator.SetTrigger("Open");
            isOpenning = true;
        }
        else if (!input && isOpen && !isPlayerInside)
        {
            animator.SetTrigger("Close");
            isOpen = false;
            isOpenning = false;
            isClosed = false;
        }
    }

    public void OpenFinish()
    {
        isOpenning = false;
        isOpen = true;
        isClosed = false;
    }

    public void CloseFinish()
    {
        isClosed = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
