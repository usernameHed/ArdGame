using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        Spin = 1,
        Move1 = 2,
        Move2 = 3,
    }

    [SerializeField]
    public DoorType doorType = DoorType.Spin;

    private void Start()
    {
        ArdManager.Instance.AddDoor(this);
    }

    public virtual void Move(float input)
    {
        //Debug.Log("move parent");
    }

    public virtual void Move(bool input)
    {
        //Debug.Log("move parent");
    }
}
