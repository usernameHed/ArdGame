using UnityEngine;

public class ArduinoInput : SingletonMono<ArduinoInput>
{
	private AndroidJavaObject plugin;

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

	void Update ()
	{
		try
		{
			plugin.Call("onFrameUpdate");
            ArdManager.Instance.InputLevel(plugin.Call<string>("getValue"));
			//text.text = "Value: " + plugin.Call<string>("getValue");
		}
		catch(System.Exception e)
		{

		}
	}
}
