using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class ArdManager : MonoBehaviour, ILevelLocal
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
    private Animator animLevel;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
        EventManager.StartListening(GameData.Event.PlayerMove, PlayerMove);
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

    private void InputLevel()
    {
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
