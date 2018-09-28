using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class ArdManager : SingletonMono<ArdManager>, ILevelLocal
{
    public TextMeshProUGUI debugArd;

    [FoldoutGroup("input")]
    public bool enableKeyboard = true;
    [SerializeField, FoldoutGroup("input"), Range(0.0f, 1.0f)]
    private float margeX = 0.05f;
    [SerializeField, FoldoutGroup("input"), Range(0.0f, 1.0f)]
    private float margeY = 0.05f;

    private float margeRotateDoorr = 0.2f;

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
        Debug.Log("ici on ajoute une porte " + door.doorType);
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

    /// <summary>
    /// called by arduino OR keyboard
    /// </summary>
    private void InputDoor(float spinDoor, bool fire1, bool fire2)
    {
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
                    else
                        doorList[i].Move(false);
                    break;
                case Door.DoorType.Move2:
                    if (fire2)
                        doorList[i].Move(true);
                    else
                        doorList[i].Move(false);
                    break;
            }
        }
    }

    [Button]
    private void AdruionoMove(int x, int y, int r, bool button1, bool button2)
    {
        MoveBall(x, y);
        MoveDoor(r, button1, button2);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    [Button]
    private void MoveBall(int x, int y)
    {
        //get value from 0 to 2, then -1: get value from -1 to 1;
        float xMove = (x * 2.0f / 1024.0f) - 1;
        float yMove = (y * 2.0f / 1024.0f) - 1;

        xMove = -xMove;

        if (Mathf.Abs(xMove) < margeX)
            xMove = 0;
        if (Mathf.Abs(yMove) < margeY)
            yMove = 0;
        
        Debug.Log("Value Move: " + xMove + " " + yMove);
        PlayerController.Instance.InputPlayerArduino(xMove, yMove);
    }

    [Button]
    private void MoveDoor(int r, bool button1, bool button2)
    {
        float rotate = (r * 2.0f / 1024.0f) - 1;
        if (Mathf.Abs(rotate) < margeRotateDoorr)
            rotate = 0;

        Debug.Log("Value Door: " + rotate + " " + button1 + " " + button2);
        InputDoor(rotate, button1, button2);
    }

    /// <summary>
    /// called each frame
    /// </summary>
    /// <param name="arduinoCode"></param>
    [Button]
    public void InputLevel(string arduinoCode)
    {
        if (debugArd)  
           debugArd.text = arduinoCode;

        string xCode = "";  //fill X data (0000 - 1024)
        string yCode = "";  //fill Y data (0000 - 1024)
        string rotationDoorCode = "";  //fill rotation (0000 - 1024) 
        for (int i = 0; i < 4; i++)
        {
            xCode += arduinoCode[i];
        }
        for (int i = 4; i < 8; i++)
        {
            yCode += arduinoCode[i];
        }
        for (int i = 8; i < 12; i++)
        {
            rotationDoorCode += arduinoCode[i];
        }
        bool button1 = arduinoCode[12] == '1';
        bool button2 = arduinoCode[13] == '1';

        MoveBall(xCode.ToInt(0), yCode.ToInt(0));               //move ball
        MoveDoor(rotationDoorCode.ToInt(0), button2, button1);  //move door
    }

    /// <summary>
    /// handle input of level with keyboard
    /// </summary>
    private void InputLevel()
    {
        //go to first level !
        if (enableKeyboard)
            InputDoor(Input.GetAxis("Spin"), Input.GetButton("Fire1"), Input.GetButton("Fire2"));


        //go to first level !
        if (Input.GetButton("Restart"))
        {
            RestartGame();
        }
        //quit application !
        if (Input.GetButton("Cancel"))
        {
            QuitGame();
        }
    }

    /// <summary>
    /// called by game, or 
    /// </summary>
    public void RestartGame()
    {
        GameManager.Instance.SceneManagerLocal.PlayPrevious();
    }

    /// <summary>
    /// called by game, or 
    /// </summary>
    public void QuitGame()
    {
        GameManager.Instance.SceneManagerLocal.Quit();
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
