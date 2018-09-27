using UnityEngine;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private GameObject collidersWall;       //link to walls

    private bool enabledScript = true;      //tell if this script should be active or not


    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    private void Start()
    {
        collidersWall.SetActive(false);             //desactive wall at start !
        enabledScript = true;               //active this script at start
    }

    /// <summary>
    /// called by OnTriggerEnter, when player enter in the hole
    /// </summary>
    private void GameOver()
    {
        SoundManager.Instance.PlayGingle();
        SoundManager.Instance.StopBallSound();
        enabledScript = false;
        collidersWall.SetActive(true);              //active the wall (for blocking player)
    }

    /// <summary>
    /// trigger the player, and trigger the event GameOver !
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (!enabledScript)
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            EventManager.TriggerEvent(GameData.Event.GameOver);
        }
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
