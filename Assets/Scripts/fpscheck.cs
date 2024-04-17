using UnityEngine;
using System.Collections;

public class fpscheck : MonoBehaviour
{
    private int framesPerSec;
    private float frequency = 1.0f;
    private string fps;

    private IEnumerator Start()
    {
        while (true)
        {
            int lastFrameCount = Time.frameCount;
            float lastTime = Time.realtimeSinceStartup;
            yield return new WaitForSeconds(frequency);
            float timeSpan = Time.realtimeSinceStartup - lastTime;
            int frameCount = Time.frameCount - lastFrameCount;
            fps = $"FPS: {Mathf.RoundToInt(frameCount / timeSpan)}";
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 100, 10, 150, 20), fps);
    }
}