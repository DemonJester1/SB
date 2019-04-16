using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	private int FramesPerSec;
	private float frequency = 1.0f;
	private string fps;

	// Use this for initialization
	void Start () {
		StartCoroutine(FPS());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator FPS() {
		for(;;){
			// Capture frame-per-second
			int lastFrameCount = Time.frameCount;
			float lastTime = Time.realtimeSinceStartup;
			yield return new WaitForSeconds(frequency);
			float timeSpan = Time.realtimeSinceStartup - lastTime;
			int frameCount = Time.frameCount - lastFrameCount;

			// Display it

			fps = string.Format("FPS: {0}" , Mathf.RoundToInt(frameCount / timeSpan));
		}
	}


	void OnGUI(){
		GUI.Label(new Rect(Screen.width - 100,10,150,20), fps);
	}

	public void LoadLevel(){
		SceneManager.LoadScene ("Level");
	}
}
