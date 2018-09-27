using UnityEngine;

public class ArduinoInput : MonoBehaviour
{
	private AndroidJavaObject plugin;
    private float fuckYouVeryMuch = 0.0F;

    private bool enableScript = false;

	void Start()
	{
        Init();
    }

    /// <summary>
    /// init arduino
    /// </summary>
    private void Init()
    {
        plugin = new AndroidJavaObject("com.project.unityotglib.OtgReader");
        AndroidJavaClass playerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject activity = playerClass.GetStatic<AndroidJavaObject>("currentActivity");
        plugin.CallStatic("Init", new object[1] { activity });
    }

    public void EnableScript()
    {
        enableScript = true;
    }

	void Update ()
	{
        if (!enableScript)
            return;


        try
		{
            plugin.Call("onFrameUpdate");
            string codeArduino = plugin.Call<string>("getValue");

            ArdManager.Instance.InputLevel(codeArduino);
			//text.text = "Value: " + plugin.Call<string>("getValue");
		}
		catch(System.Exception e)
		{
            //Debug.LogError(e);
		}
	}
}
