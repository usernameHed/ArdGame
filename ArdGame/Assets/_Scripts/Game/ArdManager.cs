using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class ArdManager : SingletonMono<ArdManager>, ILevelLocal
{
    [SerializeField]
    private float timeAfterWinningForCamera = 1.5f;
    [SerializeField]
    private float timeAfterWinning = 2f;
    [SerializeField]
    private string levelText = "Level 1";

    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private CameraMove camMove;

    [SerializeField]
    private List<Door> doorList;

    [SerializeField]
    private Animator animLevel;

    private void OnEnable()
    {
        doorList.Clear();
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
        EventManager.StartListening(GameData.Event.PlayerMove, PlayerMove);
    }

    public void AddDoor(Door door)
    {
        doorList.Add(door);
    }

    public void InitScene()
    {
        Debug.Log("INIT ArdManager !!");
        text.text = levelText;
        camMove.enabled = false;
    }

    private void PlayerMove()
    {
        animLevel.Play("Out");
    }

    private void InputDoor()
    {
        float spinDoor = Input.GetAxis("Spin");
        bool fire1 = Input.GetButton("Fire1");
        bool fire2 = Input.GetButton("Fire2");

        for (int i = 0; i < doorList.Count; i++)
        {
            switch(doorList[i].doorType)
            {
                case Door.DoorType.Spin:
                    if (spinDoor != 0)
                        doorList[i].Move(spinDoor);
                    break;
                case Door.DoorType.Move1:
                    if (fire1)
                        doorList[i].Move(true);
                    break;
                case Door.DoorType.Move2:
                    if (fire2)
                        doorList[i].Move(true);
                    break;
            }
        }

        
    }

    private void InputLevel()
    {
        //go to first level !
        InputDoor();


        //go to first level !
        if (Input.GetButton("Restart"))
        {
            GameManager.Instance.SceneManagerLocal.PlayPrevious();
        }
        //quit application !
        if (Input.GetButton("Cancel"))
        {
            GameManager.Instance.SceneManagerLocal.Quit();
        }
    }

    /// <summary>
    /// called when winning, after X seconde, go to next level !
    /// </summary>
    private void GameOver()
    {
        Invoke("ActiveCamera", timeAfterWinningForCamera);
        Invoke("NextLevel", timeAfterWinning);
    }

    private void ActiveCamera()
    {
        camMove.enabled = true;
    }

    private void NextLevel()
    {
        GameManager.Instance.SceneManagerLocal.PlayNext();
    }

    private void Update()
    {
        InputLevel();
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
