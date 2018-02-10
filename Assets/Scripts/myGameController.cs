using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

public class myGameController : MonoBehaviour {
	
	public static myGameController Instance;
	
	public static bool superDone = false;
	
	public static string participantID = "";
	public static bool betweenScenes = false;
	public static string scene;
	public static int scenecount = 0;
	public static string runfirst;
	public static string runsecond;
	public static string scenemode {
		get { return scenemode;} 
		set {
			switch (value) {
					
				case "N":
					runfirst = "NLPScene";
					runsecond = "FDGScene";
					break;
				case "F":
				default:
					runfirst = "FDGScene";
					runsecond = "NLPScene";
					break;
			}
				
			
		}
	}
	
	public static string logFile;
	
	public static string dataPath = "";
	
	public Texture legendBkgrd;
	public Texture scriptBkgrd;
	
	public static Texture legendTexture;
	public static Texture2D mytexture;
	
	public GUIStyle alertstyle; // for alert message box
	public GUIStyle boxstyle; // for highlight box
	public GUIStyle scriptstyle; // for playscript text
	
	//public GUIStyle newstyle;
	public GUIStyle buttonstyle;
	
	// keep track of clicks
	public static bool SurveyClicked = false;
	public static bool TLXClicked = false;
	
	void Awake() {
		DontDestroyOnLoad (this); // this will stay throughout the entire game / set of scenes	
		
		Instance = this;
		
		legendTexture = Resources.Load ("Textures/blanklegend") as Texture;
		dataPath = Application.dataPath;
		
		DateTime timeSpan = System.DateTime.Now;
		participantID = timeSpan.ToString("MMddHHmmssfff");
		
		// setup which scene and group we are going to run as
		scene = "PracticeScene"; 
		// set which to run first using random
		int myrand = UnityEngine.Random.Range(0,4); // gives 0, 1, 2, 3
		Debug.Log ("Random = "+myrand);
		if (myrand % 2 == 0) { // 0 or 2
			runfirst = "FDGScene";
			runsecond = "NLPScene";
		} else { // 1 or 3
			runfirst = "NLPScene";
			runsecond = "FDGScene";
		}
		
	}
	
	// Use this for initialization
	void Start () {
		
		buttonstyle = new GUIStyle();
		buttonstyle.normal.background = (Texture2D)scriptBkgrd;
		buttonstyle.normal.textColor = Color.white;
		
		legendTexture = Resources.Load ("Textures/ErnestLegend") as Texture;
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnLevelWasLoaded(int level) {
		
		if (level == 1 || level == 2) {
				Debug.Log ("***************onlevelwasloaded for level="+level);
				//InitScript currentinit = Camera.main.GetComponent<InitScript>();
				
				//InitScript.LoadChars ();
				//RunPlay();
				//currentinit.starting = true;
				//currentinit.timer = 0.0f;
				// update method picks this up and sets the stuff to start the play & set the files to use
		}
		
		
		
		
		// level specific initializations
		if (level == 2) { // NLP
            Debug.Log ("NLP Loaded");
		} else if (level == 1) { // FDG
			Debug.Log ("FDG Loaded");
		} else if (level == 0) { // Practice
			Debug.Log ("Practice Loaded");
		}
		
		
	}
	
}
