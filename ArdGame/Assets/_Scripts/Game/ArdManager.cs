using Sirenix.OdinInspector;
using UnityEngine;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class ArdManager : MonoBehaviour, ILevelLocal
{
    [SerializeField]
    private float timeAfterWinning = 2f;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    public void InitScene()
    {
        Debug.Log("INIT ArdManager !!");
    }

    /// <summary>
    /// called when winning, after X seconde, go to next level !
    /// </summary>
    private void GameOver()
    {
        Invoke("NextLevel", timeAfterWinning);
    }

    private void NextLevel()
    {
        GameManager.Instance.SceneManagerLocal.PlayNext();
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
