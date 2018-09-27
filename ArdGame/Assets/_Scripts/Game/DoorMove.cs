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

    /// <summary>
    /// open or close (set trigger of animation)
    /// </summary>
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

    /// <summary>
    /// called when animation Open is finished (called by animator)
    /// </summary>
    public void OpenFinish()
    {
        isOpenning = false;
        isOpen = true;
        isClosed = false;
    }

    /// <summary>
    /// called when animation Close is finished (called by animator)
    /// </summary>
    public void CloseFinish()
    {
        isClosed = true;
    }

    /// <summary>
    /// define if player is inside or not player (has to be with tag Player)
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    /// <summary>
    /// define if player is inside or not player (has to be with tag Player)
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
        }
    }
}
