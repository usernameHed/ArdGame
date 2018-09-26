using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLinkAnimator : MonoBehaviour
{
    [SerializeField]
    private DoorMove door;

    public void OpenFinish()
    {
        door.OpenFinish();
    }

    public void CloseFinish()
    {
        door.CloseFinish();
    }
}
