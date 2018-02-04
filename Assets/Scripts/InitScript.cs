using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
//using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;

public class InitScript : MonoBehaviour {
	
	public static InitScript Instance;
	//string txtmuch = "45";
	string txtmuchx = "3";
	string txtmuchy = "5";
	string txtx = "2";
	string txty = "1";
	string txtsay = "Your gambols, your songs, your flashes of merriment, that were wont to set the table on a roar?";// Not one now to mock your own grinning? Quite chop-fallen.  Now get you to my lady's chamber, and tell her, let her paint an inch thick, to this favour she must come. Make her laugh at that.";
	string txtforward = "3";
	string participantID = "";
	static bool betweenScenes = false;
	static string scene;
	static int scenecount = 0;
	string runfirst;
	string runsecond;
	
	public static float a;
	public static float b;
	public static int c = 0;
	public static GameObject what;
	public static Vector3 atwhere;
	
	public GameObject prefabobj;
	public GameObject prefabchar;
	public GameObject prefabmark;
	//public Material prefabarm;
	
	// for mode dropdowns
	private Vector2 scrollViewVector = Vector2.zero;
	private Vector2 fileViewVector = Vector2.zero;
	private Vector2 scriptViewVector = Vector2.zero;
	private Vector2 actionViewVector = Vector2.zero;
	private Vector2 charViewVector = Vector2.zero;
	private Vector2 targetViewVector = Vector2.zero;
	private Rect dropDownRect = new Rect(775, 25, 125, 200);
	private Rect dropDownFileRect = new Rect(125, 65, 125, 200); // Choose character input file
	private Rect dropDownScriptRect = new Rect(450,25,125,200); // Choose script file
	private Rect dropDownActionRect = new Rect(450, 200, 125, 200); // Choose action 
	private Rect dropDownCharRect = new Rect(125, 200, 125, 200); // Choose actor or character 125, 390, 100, 30
	private Rect dropDownTargetRect = new Rect(775, 200, 125, 200); // Choose target pawn, mark, or char
    private static string[] mlist = {"Choose Mode:","Baseline", "Random", "NLP", "Rules", "FDG"};
	private static string[] filelist = {};
	private static string[] fullfilelist = {};
	private static string[] actionlist = {"Choose Action:", "Walk", "Look", "Pickup", "Putdown", "Speak", "Point", "WalkToPt", "Print", "Follow"};
	private static string[] charlisttext = {};
	private static string[] targetlisttext = {};
	private static GameObject[] charlist = {};
	private static GameObject[] targetlist = {};
	static string logFile;
    static int indexNumber;
	static int fileindexNumber;
	static int scriptindexNumber;
	static int actionindexNumber;
	static int charindexNumber;
	static int targetindexNumber;
    bool show = false;
	bool fileshow = false;
	bool scriptshow = false;
	bool actionshow = false;
	bool charshow = false;
	bool targetshow = false;
	public enum playmodes { practice, baseline, random, nlp, rules, fdg };
	public static playmodes mode = playmodes.practice;
	public static bool runshort = false;
	
	float timer = 0.0f;
	float timerMax = 1.0f; // reset to 5 when working
	bool starting = false;
	
	// for file reading
	static char quote = System.Convert.ToChar (34);
	//StreamWriter[] charFiles = null;
	public static bool started = false;
	static string path = @"";
/*	static string inputFileName = Application.dataPath + @"//Files//InputFile.txt";
	static string bmlFileName = Application.dataPath + @"//Files//BMLFile.txt";
	static string miniinputFileName = Application.dataPath + @"//Files//miniInputFile.txt";
	static string minibmlFileName = Application.dataPath + @"//Files//miniBMLFile.txt";
*/	public static StringReader inputFile = null;
	static StringReader playscript = null;
	static string playscripttext = null;
	static StringReader numLines = null;
	static string dataPath = "";
	
	static bool initializing = true;
	
	// variables for legend
	public  Texture hamletT;
	public  Texture horatioT;
	public  Texture gravediggerT;
	public  Texture gravediggertwoT;
	public  Texture lanternT;
	public  Texture shovelT;
	public  Texture skull1T;
	public  Texture skull2T;
	public  Texture legendBkgrd;
	public  Texture scriptBkgrd;
	
	public static Texture legendTexture;
	
	public GUIStyle alertstyle; // for alert message box
	public GUIStyle boxstyle; // for highlight box
	public GUIStyle scriptstyle; // for playscript text
	
	public float startx1 = 5f;
	public float startx2 = 110f;
	public float starty = 80f;
	public float widthtext = 90f;
	public float widthimg = 85f;
	public float heighttext = 30f;
	public float heightimg = 135f;
	public float startximg1 = 5f;
	public float startximg2 = 100f;
	public float startyimg = 100f;
	public float spacing;
	public float linex = .7f;
	public float liney = .5f;
	public Material mat;
	
	static bool intermission = false;
	static Color mycolor;
	static float itimer = 0.0f;
	static float itimerMax = 7.0f;
	public static int inum = -1;
	static Texture2D mytexture;
	static Texture2D mytexture2;
	static Texture2D mytexture3;
	static GUIStyle newstyle;
	static GUIStyle buttonstyle;
	

	
	static bool wait = false;
	static float wtimer = 0.0f;
	static float wtimerMax = 1.0f;
	
	public static bool alldone = false;
	static float atimer = 0.0f;
	static float atimerMax = 1.0f;
	
	static float pauseamt = 0.0f;
	static float pausemax = 5.0f;
	static float pauseforces = 30.0f;
	
	static bool movementfound = false;
	
	void Awake() {
		Instance = this;
//		WWW www3 = new WWW(Resources.Load ("Textures/blanklegend") );
		//legendTexture = www3.texture;
		legendTexture = Resources.Load ("Textures/blanklegend") as Texture;
		
//		WWW www3 = new WWW("file://" + Application.dataPath + "/Resources/Textures/blanklegend.png");
//				        yield return www3;
		//		        legendTexture = www3.texture;
		
		dataPath = Application.dataPath;
		
		/* #if UNITY_EDITOR
            Material m = AssetDatabase.LoadAssetAtPath("Assets/Materials/BLUEMat.mat", typeof(Material)) as Material;
            m = AssetDatabase.LoadAssetAtPath("Assets/Materials/PURPLEMat.mat", typeof(Material)) as Material;
            m = AssetDatabase.LoadAssetAtPath("Assets/Materials/REDMat.mat", typeof(Material)) as Material;
            m = AssetDatabase.LoadAssetAtPath("Assets/Materials/GREENMat.mat", typeof(Material)) as Material;
			#endif*/
		DateTime timeSpan = System.DateTime.Now;
		participantID = timeSpan.ToString("MMddHHmmssfff");
		
	}
	// Use this for initialization
	void Start () {
		spacing = heightimg+heighttext+5f;
		mycolor = GUI.backgroundColor;
		mytexture = new Texture2D(Screen.width, Screen.height); // orange
		int y = 0;
        while (y < mytexture.height) {
            int x = 0;
            while (x < mytexture.width) {
                //Color color = ((x & y) ? Color.white : Color.gray);
                mytexture.SetPixel(x, y, new Color(255f/255f, 127f/255f, 0f/255f));//new Color(51f/255f, 178f/255f, 146f/255f)); // turquoise
                ++x;
            }
            ++y;
        }
        mytexture.Apply();
		mytexture2 = new Texture2D(Screen.width, Screen.height); // brown
		y = 0;
        while (y < mytexture2.height) {
            int x = 0;
            while (x < mytexture2.width) {
                //Color color = ((x & y) ? Color.white : Color.gray);
                mytexture2.SetPixel(x, y, new Color(89f/255f, 64f/255f, 39f/255f));//new Color(51f/255f, 178f/255f, 146f/255f)); // turquoise
                ++x;
            }
            ++y;
        }
        mytexture2.Apply();
		
		mytexture3 = new Texture2D(Screen.width, Screen.height); // yellow
		y = 0;
		while (y < mytexture3.height) {
			int x=0;
			while (x < mytexture3.width) {
				mytexture3.SetPixel(x, y, Color.yellow);
				++x;
			}
			++y;
		}
		mytexture3.Apply();
		
		newstyle = new GUIStyle();
		newstyle.normal.background = mytexture;
		newstyle.fontSize = 30;
		newstyle.normal.textColor = Color.black;
		
		buttonstyle = new GUIStyle();
		buttonstyle.normal.background = (Texture2D)scriptBkgrd;
		buttonstyle.normal.textColor = Color.white;
		
		
		// initialize list of files for loading characters and pawns
		/*fullfilelist = System.IO.Directory.GetFiles(Application.dataPath + @"//Files//");
		Debug.Log (fullfilelist);
		filelist = new string[fullfilelist.Length+1];
		filelist[0] = "Select File:";
		// remove extra filesystem info from list - remember that the fullfilelist will use fileindex-1
		for (var i=0;  i<fullfilelist.Length; i++) {
			filelist[i+1] = fullfilelist[i].Replace(Application.dataPath + @"//Files/", "");
			Debug.Log (fullfilelist[i]);
			Debug.Log (filelist[i+1]);
		}*/
		
		// **************************************
		// Auto-load first set of files for practice
		// **************************************
		
		legendTexture = Resources.Load ("Textures/ErnestLegend") as Texture;
		
		/*
		// initialize click log file
		using(System.IO.StreamWriter myFile = new System.IO.StreamWriter(dataPath + @"//Logs//"+logFile+"click.log", true)) {
			
			myFile.WriteLine("Time\tMouse Click X\tMouse Click Y\tName of Object Clicked On\tFloor Location X\tFloor Location Z\tWhat Option Chosen from Menu");
				
		}
		
		// initialize character position log files
		foreach(GameObject c in GlobalObjs.listOfCharObj) {
			using(System.IO.StreamWriter myFile = new System.IO.StreamWriter(dataPath + @"//Logs//"+logFile+c.name+".log", true)) {
				
				myFile.WriteLine("Time\tX Position\tZ Position\tY Rotation Euler Angle");
					
			}
				
		}*/
		
	}
	
	// Update is called once per frame
	void Update () {
		
		
		// update box positions for playscript
		if (GlobalObjs.currentTopAllText != GlobalObjs.targetTopAllText) {
			// increment both the currentTopAllText and the boxTop by delta time amount
			float incamt = 25f * Time.deltaTime;
			if (GlobalObjs.currentTopAllText - incamt < GlobalObjs.targetTopAllText) {
				incamt = -1*(GlobalObjs.targetTopAllText - GlobalObjs.currentTopAllText);
			}
			GlobalObjs.currentTopAllText -= incamt;
			GlobalObjs.boxTop -=incamt;
		}
		//Debug.Log("curTop = "+GlobalObjs.currentTopAllText+", targetTop="+GlobalObjs.targetTopAllText+", boxTop="+GlobalObjs.boxTop+", boxHeight="+GlobalObjs.boxHeight);
		
		if (starting) {
			
			timer += Time.deltaTime;
			if (timer >= timerMax) {
				// ready to start
				betweenScenes = true;
				RunPlay ();
				timer = 0.0f;
				starting = false;
			}
		}
		//if (started && GlobalObjs.globalQueue.Count == 0) {
		//	callNextStep();
		//}
		if (intermission) {
			itimer += Time.deltaTime;
			if (itimer > itimerMax) {
				intermission = false;
				wait = true;
				itimer = 0.0f;
				//Debug.Log ("Removing inum="+inum);
				//GlobalObjs.removeOne(inum);
				//inum = -1;
			}
		}
		if (wait) {
			wtimer +=Time.deltaTime;
			if (wtimer > wtimerMax) {
				Debug.Log ("Removing inum="+inum);
				GlobalObjs.removeOne(inum);
				inum = -1;	
				wtimer = 0.0f;
				wait = false;
			}
		}
		if (alldone) {
			atimer +=Time.deltaTime;
			if (atimer > atimerMax) {
				GlobalObjs.removeOne (inum);
				inum = -1;
				atimer = 0.0f;
				alldone = false;
				started = false;
			}
		}
	}
	
	
	
	void OnGUI() {
		
		
		
		
		//Debug.Log ("INITIALIZE:"+dropDownRect.x+","+dropDownRect.y);
/*		if (intermission) {
			// show blue screen for intermission with text
			//GUIStyle newstyle = new GUIStyle();
			//newstyle.normal.background = new Texture2D(Screen.width, Screen.height);
			//GUI.backgroundColor = Color.blue;
			//GUI.Box (new Rect(0,0,Screen.width, Screen.height), "", newstyle);
			if (indexNumber == 1 || indexNumber == 0) {
				GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), mytexture, ScaleMode.ScaleToFit, false, 0);
				GUI.Label (new Rect((Screen.width/2) - 130, (Screen.height/2) + 60, Screen.width, 50), "This screen is orange", newstyle);
			} else if (indexNumber == 2) {
				GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), mytexture3, ScaleMode.ScaleToFit, false, 0);
				GUI.Label (new Rect((Screen.width/2) - 130, (Screen.height/2) + 60, Screen.width, 50), "This screen is yellow", newstyle);
			} else {
				GUI.DrawTexture(new Rect(0,0,Screen.width, Screen.height), mytexture2, ScaleMode.ScaleToFit, false, 0);
				GUI.Label (new Rect((Screen.width/2) - 120, (Screen.height/2) + 60, Screen.width, 50), "This screen is brown", newstyle);
			}
			GUI.Label (new Rect((Screen.width/2) - 85, (Screen.height/2) - 120, Screen.width, 50), "INTERMISSION", newstyle);
			
			
		} else {*/
		
		
		// **************************************
		// LEGEND section
		// **************************************
			GUI.backgroundColor = mycolor;
			// legend
			GUI.BeginGroup(new Rect(1250, -2, 200, 845)); // 724 & 770
				GUI.Box (new Rect(-3,-3, 200,770), legendBkgrd);
				GUI.Label (new Rect(10, 50, 200, 770 ), new GUIContent(legendTexture));
				GUIStyle mystyle = new GUIStyle();
				mystyle.fontSize = 30;
				mystyle.normal.textColor = Color.white;
				GUI.Label (new Rect(20, startx1+20, widthtext*2, heighttext*2), "LEGEND", mystyle);
			
			GUI.EndGroup();
			//GUI.DrawTexture(new Rect(100,60, 50,50), hamletT, ScaleMode.ScaleToFit, true, 0);
			// end legend
		
		// **************************************
		// SCRIPT Section
		// **************************************
		// setup script section like legend section
				GUI.backgroundColor = Color.black;
				GUI.BeginGroup (new Rect(-3,640, 1445, 268)); // 500 & 730
					GUI.DrawTexture(new Rect(0,0,1448,268),scriptBkgrd,ScaleMode.StretchToFill,true);
					mystyle.fontSize = 20;
					mystyle.normal.textColor = Color.white;
					GUI.Label (new Rect(5,0,1000,100), "SCRIPT", mystyle);
				GUI.EndGroup ();
				
				/*mystyle.normal.textColor = Color.magenta;
				GUI.Label (new Rect(50, 100, 100, 30), "-100-------", mystyle);
				GUI.Label (new Rect(50, 200, 100, 30), "-200-------", mystyle);
				GUI.Label (new Rect(50, 300, 100, 30), "-300-------", mystyle);
				GUI.Label (new Rect(50, 400, 100, 30), "-400-------", mystyle);
				GUI.Label (new Rect(50, 500, 100, 30), "-500-------", mystyle);
				GUI.Label (new Rect(50, 600, 100, 30), "-600-------", mystyle);
				GUI.Label (new Rect(50, 700, 100, 30), "-700-------", mystyle);
				GUI.Label (new Rect(50, 800, 100, 30), "-800-------", mystyle);
		
				GUI.Label (new Rect(100, 50, 100, 30), "|100-------", mystyle);
				GUI.Label (new Rect(200, 50, 100, 30), "|200-------", mystyle);
				GUI.Label (new Rect(300, 50, 100, 30), "|300-------", mystyle);
				GUI.Label (new Rect(400, 50, 100, 30), "|400-------", mystyle);
				GUI.Label (new Rect(500, 50, 100, 30), "|500-------", mystyle);
				GUI.Label (new Rect(600, 50, 100, 30), "|600-------", mystyle);
				GUI.Label (new Rect(700, 50, 100, 30), "|700-------", mystyle);
				GUI.Label (new Rect(800, 50, 100, 30), "|800-------", mystyle);
				GUI.Label (new Rect(900, 50, 100, 30), "|900-------", mystyle);
				GUI.Label (new Rect(1000, 50, 100, 30), "|1000-------", mystyle);
				GUI.Label (new Rect(1100, 50, 100, 30), "|1100-------", mystyle);
				GUI.Label (new Rect(1200, 50, 100, 30), "|1200-------", mystyle);
				GUI.Label (new Rect(1300, 50, 100, 30), "|1300-------", mystyle);*/
			
		// **************************************
		// SCENE is running
		// **************************************
			if (started) {
				// show nothing
				if (GlobalObjs.ready) {
					if (GlobalObjs.hasspeech) {
						// show Speak GUI button
						GUI.backgroundColor = Color.yellow;
						
						if (GUI.Button (new Rect(1275, 660, 120, 30), "Speak Next Line")) {
							// say next line
							c = 0;
							GlobalObjs.ready = false;
							GlobalObjs.humanfunc.doSpeak (GlobalObjs.humanfunc.mynextline);
							GlobalObjs.hasspeech = false;
						}
					} else {
						GUI.backgroundColor = Color.yellow;
						if (GUI.Button (new Rect(1275, 660, 120, 30), "Next")) {
							// do next section / read line
							callNextStep();
						}
					}
				}
			
				// show the playscript
				GUI.BeginGroup(new Rect(0,660, 1440, 225));
					GUI.backgroundColor = Color.clear;
					GUI.Label(new Rect(5, GlobalObjs.currentTopAllText,1440, 2000), playscripttext, scriptstyle);
					GUI.backgroundColor = Color.yellow;
					GUI.Box(new Rect(0, GlobalObjs.boxTop, 1240, GlobalObjs.boxHeight), "", boxstyle); // make transparent yellow?
				
				GUI.EndGroup ();
			
				// show the warning message if true
				if (GlobalObjs.showwarning) {
					GUI.backgroundColor = Color.yellow;
					GUI.Box(new Rect(100, 640, 300, 23), "Miss Prism has an action for this part.", alertstyle);
				}
			
				// show context menu - 1, 2, 3, or 4 options
				if (c != 0) {
					GlobalObjs.ready = false;
					GUI.BeginGroup (new Rect(a, Screen.height - b, 100, 35*(c+1)));
						switch (c) {
							case 1:
								if(GUI.Button(new Rect(0,0, 100, 30), "Put Down")) {
									// put down object holding
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Put Down");
									GlobalObjs.humanfunc.doPutDown(what);
									// clear menu
									c = 0;
								}
								if(GUI.Button(new Rect(0,35, 100, 30), "Cancel")) {
									// cancel menu
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Cancel");
									c = 0;
									GlobalObjs.ready = true;
								}
						
								break;
							case 2:
								if(GUI.Button(new Rect(0,0, 100, 30), "Point At")) {
									// put down object holding
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Point At");
									if (what.tag == "Floor") {
										// create a temporary object at atwhere to point to
										GameObject mark = (GameObject)Instantiate (InitScript.Instance.prefabmark, new Vector3(atwhere.x, -0.9f, atwhere.z), new Quaternion(0,0,0,0));
										mark.name = "Delete Point At";
										mark.tag = "Delete";
										mark.renderer.material.color = Color.clear;
										GlobalObjs.humanfunc.doPoint (mark);
										
									} else {
										GlobalObjs.humanfunc.doPoint(what);
									}
									// clear menu
									c = 0;
								}
								if(GUI.Button(new Rect(0,35, 100, 30), "Look At")) {
									// put down object holding
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Look At");
									if (what.tag == "Floor") {
										// create a temporary object at atwhere to look at
										
										GameObject mark = (GameObject)Instantiate (InitScript.Instance.prefabmark, new Vector3(atwhere.x, -0.9f, atwhere.z), new Quaternion(0,0,0,0));
										mark.tag = "Delete";
										mark.name = "Delete Look At";
										mark.renderer.material.color = Color.clear;
										GlobalObjs.humanfunc.doRotate (mark.transform.position.x, mark.transform.position.z, mark);
									} else {
										GlobalObjs.humanfunc.doRotate(what.transform.position.x, what.transform.position.z, what);
									}
									// clear menu
									c = 0;
								}
								if(GUI.Button(new Rect(0,70, 100, 30), "Cancel")) {
									// cancel menu
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Cancel");
									c = 0;
									GlobalObjs.ready = true;
								}
								break;
							case 3:
								if(GUI.Button(new Rect(0,0, 100, 30), "Point At")) {
									// put down object holding
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Point At");
									if (what.tag == "Floor") {
										// create a temporary object at atwhere to point to
										// create a temporary object at atwhere to point to
										GameObject mark = (GameObject)Instantiate (InitScript.Instance.prefabmark, new Vector3(atwhere.x, -0.9f, atwhere.z), new Quaternion(0,0,0,0));
										mark.tag = "Delete";
										mark.name = "Delete Point At";
										mark.renderer.material.color = Color.clear;
										GlobalObjs.humanfunc.doPoint (mark);
									} else {
										GlobalObjs.humanfunc.doPoint(what);
									}
									// clear menu
									c = 0;
								}
								if(GUI.Button(new Rect(0,35, 100, 30), "Look At")) {
									// put down object holding
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Look At");
									if (what.tag == "Floor") {
										// create a temporary object at atwhere to look at
										
										GameObject mark = (GameObject)Instantiate (InitScript.Instance.prefabmark, new Vector3(atwhere.x, -0.9f, atwhere.z), new Quaternion(0,0,0,0));
										mark.tag = "Delete";
										mark.name = "Delete Look At";
										mark.renderer.material.color = Color.clear;
										GlobalObjs.humanfunc.doRotate (mark.transform.position.x, mark.transform.position.z, mark);
									
									} else {
										GlobalObjs.humanfunc.doRotate(what.transform.position.x, what.transform.position.z, what);
									}
									// clear menu
									c = 0;
								}
								if(GUI.Button(new Rect(0,70, 100, 30), "Go To")) {
									// put down object holding
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Go To");
									if (what.tag == "Floor") {
										// create a temporary object at atwhere to go to
										
										GameObject mark = (GameObject)Instantiate (InitScript.Instance.prefabmark, new Vector3(atwhere.x, -0.9f, atwhere.z), new Quaternion(0,0,0,0));
										mark.tag = "Delete";
										mark.name = "Delete Walk To";
										mark.renderer.material.color = Color.clear;
										GlobalObjs.humanfunc.doWalk (mark.transform.position.x, mark.transform.position.z, mark, false);
									
									} else {
										GlobalObjs.humanfunc.doWalk(what.transform.position.x, what.transform.position.z, what, false);
									}
									// clear menu
									c = 0;
								}
								if(GUI.Button(new Rect(0,105, 100, 30), "Cancel")) {
									// cancel menu
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Cancel");
									c = 0;
									GlobalObjs.ready = true;
								}
								break;
							case 4:
								if(GUI.Button(new Rect(0,0, 100, 30), "Point At")) {
									// put down object holding
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Point At");
									if (what.tag == "Floor") {
										// create a temporary object at atwhere to point to
										GameObject mark = (GameObject)Instantiate (InitScript.Instance.prefabmark, new Vector3(atwhere.x, -0.9f, atwhere.z), new Quaternion(0,0,0,0));
										mark.tag = "Delete";
										mark.name = "Delete Point At";
										mark.renderer.material.color = Color.clear;
										GlobalObjs.humanfunc.doPoint (mark);
									} else {
										GlobalObjs.humanfunc.doPoint(what);
									}
									// clear menu
									c = 0;
								}
								if(GUI.Button(new Rect(0,35, 100, 30), "Look At")) {
									// put down object holding
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Look At");
									if (what.tag == "Floor") {
										// create a temporary object at atwhere to look at
										
										GameObject mark = (GameObject)Instantiate (InitScript.Instance.prefabmark, new Vector3(atwhere.x, -0.9f, atwhere.z), new Quaternion(0,0,0,0));
										mark.tag = "Delete";
										mark.name = "Delete Look At";
										mark.renderer.material.color = Color.clear;
										GlobalObjs.humanfunc.doRotate (mark.transform.position.x, mark.transform.position.z, mark);
									
									} else {
										GlobalObjs.humanfunc.doRotate(what.transform.position.x, what.transform.position.z, what);
									}
									// clear menu
									c = 0;
								}
								if(GUI.Button(new Rect(0,70, 100, 30), "Go To")) {
									// put down object holding
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Go To");
									if (what.tag == "Floor") {
										// create a temporary object at atwhere to go to
										
										GameObject mark = (GameObject)Instantiate (InitScript.Instance.prefabmark, new Vector3(atwhere.x, -0.9f, atwhere.z), new Quaternion(0,0,0,0));
										mark.tag = "Delete";
										mark.name = "Delete Walk To";
										mark.renderer.material.color = Color.clear;
										GlobalObjs.humanfunc.doWalk (mark.transform.position.x, mark.transform.position.z, mark, false);
									
									} else {
										GlobalObjs.humanfunc.doWalk(what.transform.position.x, what.transform.position.z, what, false);
									}
									// clear menu
									c = 0;
								}
								if(GUI.Button(new Rect(0,105, 100, 30), "Go & Pickup")) {
									// put down object holding
										LogClicks (a, b, what, atwhere.x, atwhere.y, "Go & Pickup");
										GlobalObjs.humanfunc.doWalk (what.transform.position.x, what.transform.position.z, what, false);
										GlobalObjs.humanfunc.doPickup(what);
									
									// clear menu
									c = 0;
								}
								if(GUI.Button(new Rect(0,140, 100, 30), "Cancel")) {
									// cancel menu
									LogClicks (a, b, what, atwhere.x, atwhere.y, "Cancel");
									c = 0;
									GlobalObjs.ready = true;
								}
								break;
						}
					GUI.EndGroup ();
				}
			
			
		// **************************************
		// BETWEEN Scenes
		// **************************************
			} else {
				
				if (Application.loadedLevel == 0 && !betweenScenes) {
					// only show upon first starting
					
					// Instructions
					GUI.backgroundColor = Color.black;
					GUI.BeginGroup (new Rect(400,50, 800, 550)); // 500 & 730
						GUI.DrawTexture(new Rect(0,0,800,550),scriptBkgrd,ScaleMode.StretchToFill,true);
						mystyle.fontSize = 20;
						mystyle.normal.textColor = Color.white;
						GUI.Label (new Rect(5,0,1000,100), "INSTRUCTIONS:", mystyle);
						mystyle.fontSize = 12;
						string instructionText = "You will be acting as an actor to perform a scene.  You will be performing as Miss Prism, who is yellow.\n";
						instructionText = instructionText + "The play-script for the scene you are performing will appear in the 'SCRIPT' section below, \nand the legend for the characters and objects can be seen to the right.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "To perform in the scene, you will use the mouse to move, look, point, or pickup/putdown objects.  \nYou will click the 'Next' button below the 'Legend' area to start the next block of the play.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "To MOVE:\n";
						instructionText = instructionText + "Left-click your mouse on the spot, person, or object you wish to move to.  You will see a menu, choose 'Go To'.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "To LOOK:\n";
						instructionText = instructionText + "Left-click your mouse on the spot, person, or object you wish to look at.  You will see a menu, choose 'Look At'.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "To POINT:\n";
						instructionText = instructionText + "Left-click your mouse on the spot, person, or object you wish to point at.  You will see a menu, choose 'Point At'.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "To PICK-UP:\n";
						instructionText = instructionText + "Left-click your mouse on the object you wish to walk to and pick up.  You will see a menu, choose 'Go & Pickup'.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "To PUT-DOWN:\n";
						instructionText = instructionText + "Left-click your mouse on the object you wish to put down OR on MISS PRISM.  You will see a menu, choose 'Put Down'.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "To SPEAK:\n";
						instructionText = instructionText + "Click the button on the bottom right that says 'Speak Next Line'.  This button will only show when the current block is Miss Prism speaking.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "You will have one practice session to try these actions out before performing in the 'real' sessions.\n";
						//instructionText = instructionText + "You can practice as long as you feel is necessary, just click the 'Done Practicing' button when you are comfortable.\n";
						
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "Please let us know if you have any questions before you begin.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "When you are ready, please click the 'Start Session' button on the left.\n";

						GUI.Label (new Rect(5, 40, 550, 350), instructionText, mystyle);
					GUI.EndGroup ();
				
				}
			
			// Don't need file selector, just the LoadChars() function for each scene
			// Need the script index for loading the file
			
			/*
				// choose file to generate characters and pawns
				// pre-load file using this button
				if(GUI.Button(new Rect((dropDownFileRect.x - 100), dropDownFileRect.y, dropDownFileRect.width, 25), ""))
		        {
		            if(!fileshow)
		            {
		                fileshow = true;
		            }
		            else
		            {
		                fileshow = false;
		            }
		        }
		        if(fileshow)
		        {
		             
					fileViewVector = GUI.BeginScrollView(new Rect((dropDownFileRect.x - 100), (dropDownFileRect.y + 25), dropDownFileRect.width, dropDownFileRect.height),fileViewVector,new Rect(0, 0, dropDownFileRect.width, Mathf.Max(dropDownFileRect.height, (filelist.Length*25))));
		            GUI.Box(new Rect(0, 0, dropDownFileRect.width, Mathf.Max(dropDownFileRect.height, (filelist.Length*25))), "");           
		            for(int index = 0; index < filelist.Length; index++)
		            {               
		                if(GUI.Button(new Rect(0, (index*25), dropDownFileRect.height, 25), ""))
		                {
		                    fileshow = false;
		                    fileindexNumber = index;
							Debug.Log("FileIndex="+fileindexNumber);
							
		                }
		                GUI.Label(new Rect(5, (index*25), dropDownFileRect.height, 25), filelist[index]);               
		            }
		            GUI.EndScrollView();   
				}
		        else
		        {
					//Debug.Log ("in file show else");
		            GUI.Label(new Rect((dropDownFileRect.x - 95), dropDownFileRect.y, 300, 25), filelist[fileindexNumber]);
		        }
				
				if (GUI.Button (new Rect(25,25,150,30), "Load character file")) {
					LoadChars();
					
				}
				// end of choosing file to generate characters and pawns
				
				
				// choose file for script
				if(GUI.Button(new Rect((dropDownScriptRect.x - 100), dropDownScriptRect.y, dropDownScriptRect.width, 25), ""))
		        {
		            if(!scriptshow)
		            {
		                scriptshow = true;
		            }
		            else
		            {
		                scriptshow = false;
		            }
		        }
		        if(scriptshow)
		        {
		             
					scriptViewVector = GUI.BeginScrollView(new Rect((dropDownScriptRect.x - 100), (dropDownScriptRect.y + 25), dropDownScriptRect.width, dropDownScriptRect.height),scriptViewVector,new Rect(0, 0, dropDownScriptRect.width, Mathf.Max(dropDownScriptRect.height, (filelist.Length*25))));
		            GUI.Box(new Rect(0, 0, dropDownScriptRect.width, Mathf.Max(dropDownScriptRect.height, (filelist.Length*25))), "");           
		            for(int index = 0; index < filelist.Length; index++)
		            {               
		                if(GUI.Button(new Rect(0, (index*25), dropDownScriptRect.height, 25), ""))
		                {
		                    scriptshow = false;
		                    scriptindexNumber = index;
							Debug.Log("FileIndex="+scriptindexNumber);
							
		                }
		                GUI.Label(new Rect(5, (index*25), dropDownScriptRect.height, 25), filelist[index]);               
		            }
		            GUI.EndScrollView();   
				}
		        else
		        {
		            GUI.Label(new Rect((dropDownScriptRect.x - 95), dropDownScriptRect.y, 300, 25), filelist[scriptindexNumber]);
		        }
				
				// choose character to run
				if(GUI.Button(new Rect((dropDownCharRect.x - 100), dropDownCharRect.y, dropDownCharRect.width, 25), ""))
		        {
		            if(!charshow)
		            {
		                charshow = true;
		            }
		            else
		            {
		                charshow = false;
		            }
		        }
		        if(charshow)
		        {
		             
					charViewVector = GUI.BeginScrollView(new Rect((dropDownCharRect.x - 100), (dropDownCharRect.y + 25), dropDownCharRect.width, dropDownCharRect.height),charViewVector,new Rect(0, 0, dropDownCharRect.width, Mathf.Max(dropDownCharRect.height, (charlisttext.Length*25))));
		            GUI.Box(new Rect(0, 0, dropDownCharRect.width, Mathf.Max(dropDownCharRect.height, (charlisttext.Length*25))), "");           
		            for(int index = 0; index < charlisttext.Length; index++)
		            {               
		                if(GUI.Button(new Rect(0, (index*25), dropDownCharRect.height, 25), ""))
		                {
		                    charshow = false;
		                    charindexNumber = index;
							Debug.Log("FileIndex="+charindexNumber);
							
		                }
		                GUI.Label(new Rect(5, (index*25), dropDownCharRect.height, 25), charlisttext[index]);               
		            }
		            GUI.EndScrollView();   
				}
		        else
		        {

		            GUI.Label(new Rect((dropDownCharRect.x - 95), dropDownCharRect.y, 300, 25), (charlisttext.Length>0?charlisttext[charindexNumber]:"Choose Char:"));
		        }
				
				
				// choose action
				if(GUI.Button(new Rect((dropDownActionRect.x - 100), dropDownActionRect.y, dropDownActionRect.width, 25), ""))
		        {
		            if(!actionshow)
		            {
		                actionshow = true;
		            }
		            else
		            {
		                actionshow = false;
		            }
		        }
		        if(actionshow)
		        {
		             
					actionViewVector = GUI.BeginScrollView(new Rect((dropDownActionRect.x - 100), (dropDownActionRect.y + 25), dropDownActionRect.width, dropDownActionRect.height),actionViewVector,new Rect(0, 0, dropDownActionRect.width, Mathf.Max(dropDownActionRect.height, (actionlist.Length*25))));
		            GUI.Box(new Rect(0, 0, dropDownActionRect.width, Mathf.Max(dropDownActionRect.height, (actionlist.Length*25))), "");           
		            for(int index = 0; index < actionlist.Length; index++)
		            {               
		                if(GUI.Button(new Rect(0, (index*25), dropDownActionRect.height, 25), ""))
		                {
		                    actionshow = false;
		                    actionindexNumber = index;
							Debug.Log("FileIndex="+actionindexNumber);
							
		                }
		                GUI.Label(new Rect(5, (index*25), dropDownActionRect.height, 25), actionlist[index]);               
		            }
		            GUI.EndScrollView();   
				}
		        else
		        {
		            GUI.Label(new Rect((dropDownActionRect.x - 95), dropDownActionRect.y, 300, 25), actionlist[actionindexNumber]);
		        }
				
				
				// choose target
				if(GUI.Button(new Rect((dropDownTargetRect.x - 100), dropDownTargetRect.y, dropDownTargetRect.width, 25), ""))
		        {
		            if(!targetshow)
		            {
		                targetshow = true;
		            }
		            else
		            {
		                targetshow = false;
		            }
		        }
		        if(targetshow)
		        {
		             
					targetViewVector = GUI.BeginScrollView(new Rect((dropDownTargetRect.x - 100), (dropDownTargetRect.y + 25), dropDownTargetRect.width, dropDownTargetRect.height),targetViewVector,new Rect(0, 0, dropDownTargetRect.width, Mathf.Max(dropDownTargetRect.height, (targetlisttext.Length*25))));
		            GUI.Box(new Rect(0, 0, dropDownTargetRect.width, Mathf.Max(dropDownTargetRect.height, (targetlisttext.Length*25))), "");           
		            for(int index = 0; index < targetlisttext.Length; index++)
		            {               
		                if(GUI.Button(new Rect(0, (index*25), dropDownTargetRect.height, 25), ""))
		                {
		                    targetshow = false;
		                    targetindexNumber = index;
							Debug.Log("FileIndex="+targetindexNumber);
							
		                }
		                GUI.Label(new Rect(5, (index*25), dropDownTargetRect.height, 25), targetlisttext[index]);               
		            }
		            GUI.EndScrollView();   
				}
		        else
		        {
		            GUI.Label(new Rect((dropDownTargetRect.x - 95), dropDownTargetRect.y, 300, 25), (targetlisttext.Length>0?targetlisttext[targetindexNumber]:"Choose target"));
		        }
				
				// enter x
				// enter y
				GUI.Label (new Rect(925, 310, 250, 30), "Enter target coordinates:");
				txtx = GUI.TextField (new Rect(1100, 310, 40, 30), txtx, 4);
				txty = GUI.TextField (new Rect(1150, 310, 40, 30), txty, 4);
				
				// enter speech text
				GUI.Label (new Rect(925,350, 750, 30), "Enter speech text:");
				txtsay = GUI.TextArea (new Rect(925, 400, 300, 100), txtsay);
				
				// run action
				if (GUI.Button (new Rect(825, 200, 200, 30), "Click to do Action")) {
					RunAction();	
				}

				if (GUI.Button (new Rect(25, 545, 100, 30), "Stop")) {
					foreach(CharFuncs c in GlobalObjs.listOfChars) {
						c.doStopAll ();
					}
					//GlobalObjs.HamletFunc.doStopAll();
					Debug.Log ("Stopped everything");
				}

				if (GUI.Button (new Rect(25, 635, 100, 30), "Intermission")) {
					Debug.Log ("Start Intermission");
					intermission = true;
					QueueObj temp = new QueueObj(null, null, new Vector3(0,0,0), QueueObj.actiontype.intermission);
					inum = temp.msgNum;
					GlobalObjs.globalQueue.Add(temp);
					Debug.Log ("Starting inum="+inum);
				}
				*/
				
				//bool useBML = GUI.Toggle(new Rect(500, 30, 100, 30), BML, "Use BML File?");
			
			
			
		// **************************************
		// Launch URLs
		// **************************************
			// if between scenes, show launch urls
			if (betweenScenes) {
				
				//GUI.backgroundColor = Color.black;
				
				//GUI.BeginGroup (new Rect(40,50, 250, 250)); 
					//GUI.DrawTexture(new Rect(0,0,250,250),scriptBkgrd,ScaleMode.StretchToFill,true);
						
					
					// show participant id
					GUI.Label (new Rect(50, 100, 250, 30), "Participant ID:  " + participantID, buttonstyle);
					GUI.Label (new Rect(250, 100, 250, 30), "Scene:  " + Application.loadedLevel + (runfirst == "FDGScene"?((scenecount == 1)?"F":"N"):((scenecount == 1)?"N":"F")), buttonstyle);
					
					
					// show survey launch
				GUI.backgroundColor = Color.yellow;
					if (GUI.Button (new Rect(50, 150, 100, 30), "Launch Survey")) {
						// open survey & pass parameters -- update with real url & parameters & dynamic for which scene	
						//Scene s = SceneManager.GetActiveScene();
						Application.OpenURL("http://www.surveygizmo.com/s3/4124416/test?pID="+participantID+"&which="+Application.loadedLevel);//EditorApplication.currentScene);
					}
					// show app launch
				GUI.backgroundColor = Color.yellow;
					if (GUI.Button (new Rect(50, 200, 100, 30), "Launch TLX")) {
						// open TLX
						System.Diagnostics.Process.Start("Notes.app");
					}
					
				
				// Instructions
					GUI.backgroundColor = Color.black;
					GUI.BeginGroup (new Rect(400,50, 800, 550)); // 500 & 730
						GUI.DrawTexture(new Rect(0,0,800,550),scriptBkgrd,ScaleMode.StretchToFill,true);
						mystyle.fontSize = 20;
						mystyle.normal.textColor = Color.white;
						GUI.Label (new Rect(5,0,1000,100), "INSTRUCTIONS:", mystyle);
						mystyle.fontSize = 12;
						string instructionText = "Please complete the TLX survey and the SurveyGizmo survey for the scene you just performed.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						//instructionText = instructionText + "You can practice as long as you feel is necessary, just click the 'Done Practicing' button when you are comfortable.\n";
						
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "Once you have completed both surveys, please click the 'Start Session' button on the left.\n";
						instructionText = instructionText + "\n";
						instructionText = instructionText + "\n";

						GUI.Label (new Rect(5, 40, 550, 350), instructionText, mystyle);
					GUI.EndGroup ();
				//GUI.EndGroup ();
			} else {
					
		// **************************************
		// Enter Participant # only if first screen
		// **************************************
				
				//GUI.BeginGroup (new Rect(40,50, 250, 250)); 
					//GUI.DrawTexture(new Rect(0,0,250,250),scriptBkgrd,ScaleMode.StretchToFill,true);
					
					
						
					// randomly assign participant # based on datetime and machine?  Display for usage
					GUI.Label (new Rect(50, 100, 250, 30), "Participant ID:", buttonstyle);
					GUI.backgroundColor = Color.yellow;
					participantID = GUI.TextField (new Rect(150, 100, 200, 30), participantID, 15);
					
					
				//GUI.EndGroup ();
					
			}
			
		// **************************************
		// Start the Play - add logic for loading scenes?
		// **************************************
				GUI.backgroundColor = Color.yellow;
				
				if (GUI.Button (new Rect(50, 250, 100, 30), "Start Session")) {
					Debug.Log ("Starting Play "+Time.time+" - scenecount="+scenecount);	
					c = 0;
					if (scenecount == 0) {
						scene = "PracticeScene";
						scenecount++; 
						// set which to run first using random
						int myrand = UnityEngine.Random.Range(0,2);
						if (myrand == 0) {
							runfirst = "FDGScene";
							runsecond = "NLPScene";
						} else {
							runfirst = "NLPScene";
							runsecond = "FDGScene";
						}
						//betweenScenes = true;
						// assume practice scene is already loaded
						LoadChars ();
						//RunPlay();
						starting = true;
						timer = 0.0f;
					} else {
						switch (scenecount) {
							case 1:
								scene = runfirst;
								break;
							case 2:
								scene = runsecond;
								break;
						}
						Debug.Log ("Loading scene = "+scene);
						scenecount++;
						// load corresponding scene
						Application.LoadLevel(scene);
					
					}
					
				}
				
			/*
				// shows dropdown to choose what to run
				//Debug.Log ("button1:"+(dropDownRect.x-100)+","+dropDownRect.y);
				if(GUI.Button(new Rect((dropDownRect.x - 100), dropDownRect.y, dropDownRect.width, 25), ""))
		        {
		            if(!show)
		            {
		                show = true;
		            }
		            else
		            {
		                show = false;
		            }
		        }
		        if(show)
		        {
					//Debug.Log ("scrollviewvect:"+scrollViewVector);
					//Debug.Log ("scroll:"+(dropDownRect.x-100)+","+(dropDownRect.y+25));
		            scrollViewVector = GUI.BeginScrollView(new Rect((dropDownRect.x - 100), (dropDownRect.y + 25), dropDownRect.width, dropDownRect.height),scrollViewVector,new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (mlist.Length*25))));
		            GUI.Box(new Rect(0, 0, dropDownRect.width, Mathf.Max(dropDownRect.height, (mlist.Length*25))), "");           
		            for(int index = 0; index < mlist.Length; index++)
		            {               
		                if(GUI.Button(new Rect(0, (index*25), dropDownRect.height, 25), ""))
		                {
		                    show = false;
		                    indexNumber = index;
							Debug.Log("Index="+indexNumber);
							if (index == 0 || index == 1) {
								newstyle.normal.background = mytexture;
								newstyle.normal.textColor = Color.black;
							} else {
								newstyle.normal.background = mytexture2;
								newstyle.normal.textColor = Color.white;
							}
		                }
		                GUI.Label(new Rect(5, (index*25), dropDownRect.height, 25), mlist[index]);               
		            }
		            GUI.EndScrollView();   
		        }
		        else
		        {
					//Debug.Log ("selected:"+(dropDownRect.x-95)+","+dropDownRect.y);
		            GUI.Label(new Rect((dropDownRect.x - 95), dropDownRect.y, 300, 25), mlist[indexNumber]);
		        }
			*/
			}
		//}
       
	}
	
	public static void LogPositions() {
		
		// uses logfilename & character name to append to a logfile initiated at runplay
		Debug.Log ("Current Level="+Application.loadedLevel);
		DateTime timeSpan = System.DateTime.Now;
 		string timeText = timeSpan.ToString();//string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		
		foreach(GameObject c in GlobalObjs.listOfCharObj) {
			using(System.IO.StreamWriter myFile = new System.IO.StreamWriter(dataPath + @"//Logs//"+logFile+c.name+".log", true)) {
				
				myFile.WriteLine(timeText+"\t"+c.transform.position.x+"\t"+c.transform.position.z+"\t"+c.transform.eulerAngles.y);
					
			}
				
		}
	}
	
	public static void LogClicks(float x, float y, GameObject g, float w, float z, string option) {
		Debug.Log ("Current Level="+Application.loadedLevel);
		DateTime timeSpan = System.DateTime.Now;
 		string timeText = timeSpan.ToString();//string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
		using(System.IO.StreamWriter myFile = new System.IO.StreamWriter(dataPath + @"//Logs//"+logFile+"click.log", true)) {
			
			myFile.WriteLine(timeText+"\t"+x+"\t"+y+"\t"+((g == null)?("none"):(g.name))+"\t"+w+"\t"+z+"\t"+option);
				
		}
	}
		

	
	void RunPlay() {
		// check Mode & run based on that setting
		// use indexNumber, where 1=Baseline, 2=BML, etc
		
		//Debug.Log ("Run in mode #"+indexNumber);
		starting = false;
		started = true;
		string myfilename;
		string myshortname;
		TextAsset scriptFile;
		TextAsset nfFile;
		switch (Application.loadedLevel) {
		//switch (EditorApplication.currentScene) {
		//case "Assets/Scenes/FDGScene.unity": // nlp version - no forces
		case 1:
			myfilename =  @"Files/ErnestScriptNLP";
			myshortname = "FDG";
			indexNumber = 4;
			scriptFile = (TextAsset)Resources.Load (@"Files/ErnestScriptText");
			nfFile = (TextAsset)Resources.Load (@"Files/ErnestNumLines");
			break;
		case 0:
		//case "Assets/Scenes/PracticeScene.unity": // practice - do no movement
			myfilename =  @"Files/Practice";
			myshortname = "Practice";
			indexNumber = 0;
			scriptFile = (TextAsset)Resources.Load (@"Files/PracticeScript");
			nfFile = (TextAsset)Resources.Load (@"Files/PracticeNumLines");
			break;
		case 2:
		//case "Assets/Scenes/NLPScene.unity":
			myfilename =  @"Files/ErnestScriptNLP";
			myshortname = "NLP";
			indexNumber = 5;
			scriptFile = (TextAsset)Resources.Load (@"Files/ErnestScriptText");
			nfFile = (TextAsset)Resources.Load (@"Files/ErnestNumLines");
			break;
		default:
			myfilename =  @"Files/ErnestScriptNLP";
			myshortname = "Other";
			indexNumber = 4;
			scriptFile = (TextAsset)Resources.Load (@"Files/ErnestScriptText");
			nfFile = (TextAsset)Resources.Load (@"Files/ErnestNumLines");
			break;
			
		}
		TextAsset textFile = (TextAsset)Resources.Load (myfilename);
		inputFile = new StringReader (textFile.text);
		playscript = new StringReader(scriptFile.text);
		playscripttext = playscript.ReadToEnd();
		numLines = new StringReader(nfFile.text);
		initializing = true;
			
		
		DateTime timeSpan = System.DateTime.Now;
 		string timeText = timeSpan.ToString("MM-dd-yy-HH-mm-ss-fff");
		logFile = participantID+"-"+timeText+"-"+myshortname+"-"; // name log files after participant
		
		// initialize click log file
		using(System.IO.StreamWriter myFile = new System.IO.StreamWriter(dataPath + @"//Logs//"+logFile+"click.log", true)) {
			
			myFile.WriteLine("Time\tMouse Click X\tMouse Click Y\tName of Object Clicked On\tFloor Location X\tFloor Location Z\tWhat Option Chosen from Menu");
				
		}
		
		// initialize character position log files
		foreach(GameObject c in GlobalObjs.listOfCharObj) {
			Debug.Log ("char name for log="+c.name);
			using(System.IO.StreamWriter myFile = new System.IO.StreamWriter(dataPath + @"//Logs//"+logFile+c.name+".log", true)) {
				
				myFile.WriteLine("Time\tX Position\tZ Position\tY Rotation Euler Angle");
					
			}
				
		}
		
		Debug.Log ("Filename="+myfilename);
		// make sure a file was loaded and a file was selected for the script
		
		// need to add logic to do different actions based on mode chosen!!
		switch (indexNumber) {
		case 0: // baseline -- by default if don't choose or if click choose mode

			mode = playmodes.practice;
			newstyle.normal.background = mytexture;
			newstyle.normal.textColor = Color.black;
			logFile = logFile + "practice-";
			/*if (runshort) {
				inputFile = File.OpenText (miniinputFileName);
				GlobalObjs.Hamlet.transform.position = new Vector3(4.2f, 0f, 37f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, -1f, 0f, 0f);
				GlobalObjs.Horatio.transform.position = new Vector3(.9f, 0f, 35.3f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, .9f, 0f, -.5f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(16f, 0f, 34f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, -1f, 0f, -.1f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(54f, 0f, 49.9f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, .7f, 0f, -.7f);
				
				GlobalObjs.GraveDiggerTwoFunc.doPickup(GlobalObjs.Lantern);
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
			} else {
				inputFile = File.OpenText(inputFileName);
			}*/
			break;
		case 2: // random
			mode = playmodes.random;
			newstyle.normal.background = mytexture3;
			newstyle.normal.textColor = Color.black;
			logFile = logFile + "random-";
			/*if (runshort) {
				// place chars randomly
				GlobalObjs.Hamlet.transform.position = new Vector3(-7.9f, 0f, .8f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, -1f, 0f, -1f);
				GlobalObjs.Horatio.transform.position = new Vector3(-33f, 0f, 44.8f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, .8f, 0f, .6f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(18.1f, 0f, 4.5f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, -.5f, 0f, .9f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(6f, 0f, -4f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, 1f, 0f, .1f);
				
				GlobalObjs.GraveDiggerFunc.doPickup(GlobalObjs.Skull2);
				GlobalObjs.HamletFunc.doPickup(GlobalObjs.Skull1);
				
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
				inputFile = File.OpenText (minibmlFileName);
			} else {
				inputFile = File.OpenText (bmlFileName);
			}*/
			break;
		case 1: //baseline
			mode = playmodes.baseline;
			newstyle.normal.background = mytexture;
			newstyle.normal.textColor = Color.black;
			logFile = logFile + "baseline-";
			/*if (runshort) {
				// place chars
				GlobalObjs.Hamlet.transform.position = new Vector3(4.2f, 0f, 37f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, -1f, 0f, 0f);
				GlobalObjs.Horatio.transform.position = new Vector3(.9f, 0f, 35.3f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, .9f, 0f, -.5f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(16f, 0f, 34f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, -1f, 0f, -.1f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(54f, 0f, 49.9f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, .7f, 0f, -.7f);
				
				GlobalObjs.GraveDiggerTwoFunc.doPickup(GlobalObjs.Lantern);
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
				inputFile = File.OpenText(miniinputFileName);
			} else {
				inputFile = File.OpenText (inputFileName);
			}*/
			break;
		case 3: // nlp
			mode = playmodes.nlp;
			newstyle.normal.background = mytexture2;
			newstyle.normal.textColor = Color.white;
			logFile = logFile + "nlp-";
			/*if (runshort) {
				// place chars
				GlobalObjs.Hamlet.transform.position = new Vector3(-6.8f, 0f, 42.4f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, .6f, 0f, -.8f);
				GlobalObjs.Horatio.transform.position = new Vector3(-5f, 0f, 39.1f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, 1f, 0f, .2f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(14.3f, 0f, 34.1f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, .8f, 0f, .6f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(49.1f, 0f, 50.2f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, -.6f, 0f, .8f);
				
				GlobalObjs.GraveDiggerTwoFunc.doPickup(GlobalObjs.Lantern);
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
				inputFile = File.OpenText(minibmlFileName);
			} else {
				inputFile = File.OpenText (bmlFileName);
			}*/
			break;
		case 4:
			mode = playmodes.rules;
			logFile = logFile + "rules-";
			/*if (runshort) {
				// place chars
				GlobalObjs.Hamlet.transform.position = new Vector3(-6.8f, 0f, 42.4f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, .6f, 0f, -.8f);
				GlobalObjs.Horatio.transform.position = new Vector3(-5f, 0f, 39.1f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, 1f, 0f, .2f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(14.3f, 0f, 34.1f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, .8f, 0f, .6f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(49.1f, 0f, 50.2f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, -.6f, 0f, .8f);
				
				GlobalObjs.GraveDiggerTwoFunc.doPickup(GlobalObjs.Lantern);
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
				inputFile = File.OpenText(minibmlFileName);
			} else {
				inputFile = File.OpenText (bmlFileName);
			}*/
			break;
		case 5:
			mode = playmodes.fdg;
			logFile = logFile + "fdg-";
			/*if (runshort) {
				// place chars
				GlobalObjs.Hamlet.transform.position = new Vector3(-6.8f, 0f, 42.4f);
				GlobalObjs.Hamlet.transform.rotation = new Quaternion(0f, .6f, 0f, -.8f);
				GlobalObjs.Horatio.transform.position = new Vector3(-5f, 0f, 39.1f);
				GlobalObjs.Horatio.transform.rotation = new Quaternion(0f, 1f, 0f, .2f);
				GlobalObjs.GraveDigger.transform.position = new Vector3(14.3f, 0f, 34.1f);
				GlobalObjs.GraveDigger.transform.rotation = new Quaternion(0f, .8f, 0f, .6f);
				GlobalObjs.GraveDiggerTwo.transform.position = new Vector3(49.1f, 0f, 50.2f);
				GlobalObjs.GraveDiggerTwo.transform.rotation = new Quaternion(0f, -.6f, 0f, .8f);
				
				GlobalObjs.GraveDiggerTwoFunc.doPickup(GlobalObjs.Lantern);
				GlobalObjs.Shovel.transform.position = GlobalObjs.Grave.transform.position + new Vector3(0f, .5f, 0f);
				//GlobalObjs.Shovel.transform.position.y = .5f;
				
				inputFile = File.OpenText(minibmlFileName);
			} else {
				inputFile = File.OpenText (bmlFileName);
			}*/
			
			// set everyone's prior rotateTo for their audience facing position
			foreach(CharFuncs c in GlobalObjs.listOfChars) {
				c.lastrotateTo = new Vector3(c.thisChar.transform.position.x, 0, 90);
				// set everyone's prior target?
				//c.moveTo = c.thisChar.transform.position;
			}
			
			
			Forces.createForceGraph ();
			
			Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Forces & movement found");
			movementfound = false;
			Forces.setupForces ();
			Forces.g.printall();
			Forces.recalculate ();
			// apply forces to forcequeue
		//}
		//if (mode == playmodes.fdg) { // does this have to fire if no movement was found??  I say no...???
			// apply all forcequeue items to character funcs
			Forces.unsetupForces();
			Forces.applyChanges();
			
			//pausesome ();
			
			break;
		}
		// pause
		//started = true;
		pausesome ();
//		callNextStep ();
	}
	
	public static void pausesome() {
		Debug.Log ("pausing");
		if (pauseamt >= pausemax) {
			Debug.Log ("pauseamt enough");
			// show the button for next instead of calling next step?
			GlobalObjs.ready = true;
			//		callNextStep();
		} else {
			pauseamt += Time.deltaTime;
			//pausesome ();
		}
	}
	
	public static void callNextStep() {
		Debug.Log ("Calling Next Step");
		LogPositions();
		GlobalObjs.ready = false;
		
		string curLine = null;// = inputFile.ReadLine ();
		string[] parsedLine = null;
		bool firstiteration = true;
		
		// update script stuff here
		string nLLine = null;
		string[] parsenum = null;
		
		nLLine = numLines.ReadLine ();
	if (nLLine != null) {
		parsenum = nLLine.Split ('\t'); // 0 has # of lines, 1 has show message or not
		
		if (parsenum[1] == "Y") {
			GlobalObjs.showwarning = true;
		} else {
			GlobalObjs.showwarning = false;
		}
		
		if (!initializing) {
			// don't set these unless it's not the first time	
			GlobalObjs.boxTop = GlobalObjs.boxTop + GlobalObjs.boxHeight + 18f; // set this to the right spot
			GlobalObjs.targetTopAllText = GlobalObjs.targetTopAllText - GlobalObjs.boxHeight - 18f; // move it up
			//GlobalObjs.currentTopAllText = 0f; // don't change this - do it only in the update
			GlobalObjs.boxHeight = int.Parse(parsenum[0]) * 18f; // set to the current height
		} else {
			// initialize
			GlobalObjs.targetTopAllText = 0f;
			GlobalObjs.currentTopAllText = 0f;
			GlobalObjs.boxTop = 2f;
			GlobalObjs.boxHeight = int.Parse(parsenum[0]) * 18f;
			initializing = false;
		}
		
		if (parsenum[2] == "Y") {
			// has speech, so set the nextline and hasspeech vars
			GlobalObjs.hasspeech = true;
			GlobalObjs.humanfunc.mynextline = parsenum[3];
		} else {
			GlobalObjs.hasspeech = false;
			GlobalObjs.humanfunc.mynextline = "";
		}
		
	}
		
		while (firstiteration || (curLine != null && parsedLine[0] != "N")) {
			firstiteration = false;
       		curLine = inputFile.ReadLine ();
			Debug.Log ("*****"+curLine);
	        if (curLine != null) {
	           
	//            currentMessageNum++;
	            parsedLine = curLine.Split ('\t');
	            Debug.Log ("CJT LINE="+curLine);
				//Debug.Log ("First item=" +parsedLine[0]);
	            switch (parsedLine [1]) {
	                case "MOVE":
	                    //Debug.Log ("CJT MESSAGE="+parsedLine [1] + " " + parsedLine [2] + " CJT" + currentMessageNum + " " + parsedLine [3]);
	                    //vhmsg.SendVHMsg ("vrExpress", parsedLine [1] + " " + parsedLine [2] + " CJT" + currentMessageNum + " " + parsedLine [3]);
		                //Debug.Log ("Doing movement for "+parsedLine[2]+" doing:"+parsedLine[4]);	
					
					
					// if human step, skip
					if (parsedLine[2] == GlobalObjs.human.name) {
						// skip this line!!!
					} else {
						if (mode == playmodes.random) {
							doRandomMvmt(parsedLine[2], parsedLine[4]);
						} else {
							parseMovement(parsedLine[2], parsedLine[4]);    
						}
					}
						break;
	                case "SPEAK":
	                    //if (parsedLine [1] == actor) {
	                        // find the speech tags & display only that text, start listening for enter key or mouse click?   
	                    //    showtext = findSpeech (parsedLine [3]);
	                    //} else {
	                        // else send the message to be spoken by the character
	                    //Debug.Log ("CJT MESSAGE="+parsedLine [1] + " " + parsedLine [2] + " CJT" + currentMessageNum + " " + parsedLine [3]);
						
					if (parsedLine[2] == GlobalObjs.human.name) {
						// skip this line!!
					} else {
					
						CharFuncs who = GlobalObjs.getCharFunc(parsedLine[2]);
						string saywhat = findSpeech(parsedLine[4]);
						Debug.Log (who.name+" says: "+saywhat);
						who.doSpeak (saywhat);
						if (mode == playmodes.random) {
							int temp = UnityEngine.Random.Range (0,2);
							if (temp == 1) {
								doRandomMvmt(parsedLine[2], parsedLine[4]);
							}
						}
	                    //vhmsg.SendVHMsg ("vrSpeak", parsedLine [1] + " " + parsedLine [2] + " CJT" + currentMessageNum + " " + parsedLine [3]);
	                    //}
	                    if (mode == playmodes.rules || mode == playmodes.fdg || mode == playmodes.practice) {
	                    	// add rule to get everyone to look at the speaker
							Debug.Log ("Adding Look at Speaker "+who.thisChar.name);
	                    	foreach (CharFuncs c in GlobalObjs.listOfChars) {
	                    		if (c.onstage() && c != who && !c.isHuman) {
	                    			c.doRotate(who.thisChar.transform.position.x, who.thisChar.transform.position.z, who.gameObject);
	                    		}
	                    	}
	                    }
					}
	                    break;
					case "BREAK":
						Debug.Log ("Start Intermission");
						intermission = true;
						QueueObj temp = new QueueObj(null, null, new Vector3(0,0,0), QueueObj.actiontype.intermission);
						inum = temp.msgNum;
						GlobalObjs.globalQueue.Add(temp);
						Debug.Log ("Starting inum="+inum);
						break;
					case "PRINT":
						Debug.Log (Time.time);
						Debug.Log ("Coordinates:");
						foreach(GameObject g in GlobalObjs.listOfCharObj) {
							Debug.Log (g.name+"="+g.transform.position+","+g.transform.rotation);
							if (g.transform.childCount != 0) {
								Debug.Log (g.name+" children=");
								for (int i=0; i< g.transform.childCount; i++) {
									Debug.Log (g.transform.GetChild(i).name);
								}
								Debug.Log ("End "+g.name+" children");
							}
						}
						/*Debug.Log ("Hamlet="+GlobalObjs.Hamlet.transform.position+","+GlobalObjs.Hamlet.transform.rotation);
						Debug.Log ("Horatio="+GlobalObjs.Horatio.transform.position+","+GlobalObjs.Horatio.transform.rotation);
						Debug.Log ("GraveDigger="+GlobalObjs.GraveDigger.transform.position+","+GlobalObjs.GraveDigger.transform.rotation);
						Debug.Log ("GraveDigger2="+GlobalObjs.GraveDiggerTwo.transform.position+","+GlobalObjs.GraveDiggerTwo.transform.rotation);
						if (GlobalObjs.Hamlet.transform.childCount != 0) {
							Debug.Log ("Hamlet children=");
							for (int i=0; i< GlobalObjs.Hamlet.transform.childCount; i++) {
								Debug.Log (GlobalObjs.Hamlet.transform.GetChild(i).name);
							}
							Debug.Log ("End Hamlet children");
						}
						if (GlobalObjs.Horatio.transform.childCount != 0) {
							Debug.Log ("Horatio children=");
							for (int i=0; i< GlobalObjs.Horatio.transform.childCount; i++) {
								Debug.Log (GlobalObjs.Horatio.transform.GetChild(i).name);
							}
							Debug.Log ("End Horatio children");
						}
						if (GlobalObjs.GraveDigger.transform.childCount != 0) {
							Debug.Log ("GraveDigger children=");
							for (int i=0; i< GlobalObjs.GraveDigger.transform.childCount; i++) {
								Debug.Log (GlobalObjs.GraveDigger.transform.GetChild(i).name);
							}
							Debug.Log ("End GraveDigger children");
						}
						if (GlobalObjs.GraveDiggerTwo.transform.childCount != 0) {
							Debug.Log ("GraveDiggerTwo children=");
							for (int i=0; i< GlobalObjs.GraveDiggerTwo.transform.childCount; i++) {
								Debug.Log (GlobalObjs.GraveDiggerTwo.transform.GetChild(i).name);
							}
							Debug.Log ("End GraveDiggerTwo children");
						}*/
						break;
	                default:
	                    // bad command, ignore
					Debug.Log ("Bad command");
	                    break;
	            }
	            //curLine = null;
	            //parsedLine = null;
	        } else {
	            // exit - nothing left to do
	            Debug.Log ("CJT MESSAGE=DONE!!");
	            inputFile.Close ();
				QueueObj temp = new QueueObj(null, null, new Vector3(0,0,0), QueueObj.actiontype.intermission);
						inum = temp.msgNum;
						GlobalObjs.globalQueue.Add(temp);
				alldone = true;
//	            started = false;
	            inputFile = null;
	            //currentMessageNum = 0;
	           // Application.Quit ();
				
		// **************************************
		// All done with this play, so do the next
		// **************************************
				
	        }

		} //while (curLine != null && parsedLine[0] != "N");
		
		if (mode == playmodes.fdg && movementfound) {
			Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Forces & movement found");
			movementfound = false;
			Forces.setupForces ();
			Forces.g.printall();
			Forces.recalculate ();
			// apply forces to forcequeue
		//}
		//if (mode == playmodes.fdg) { // does this have to fire if no movement was found??  I say no...???
			// apply all forcequeue items to character funcs
			Forces.unsetupForces();
			Forces.applyChanges();
		}
		
	}
	
	
	static string findSpeech(string xml) {
        string myText = null;
        int startPos = 0;
        int endPos = 0;
        startPos = xml.IndexOf("application/ssml+xml"+quote+">");
        endPos = xml.IndexOf("</speech>");
        myText = xml.Substring(startPos+22,endPos-startPos-22);
        return myText;
    }
	
	static void doRandomMvmt(string name, string xmltxt) {
		// do a random movement for the current character
		CharFuncs who = GlobalObjs.getCharFunc(name);
		Debug.Log (who.thisChar.name);
		
		float targetx = UnityEngine.Random.Range(-50,51); // x position
			
		float targety = UnityEngine.Random.Range(-5, 70); // y position
		
		int whichfunction = UnityEngine.Random.Range(0, 5); // which character function to run
		
		int objnum;
		if (whichfunction == 2 || whichfunction == 3) {
			objnum = UnityEngine.Random.Range(0, GlobalObjs.listOfPawnObj.Count); // can only pick up or put down one of these objects
		} else {
			objnum = UnityEngine.Random.Range(0, GlobalObjs.listOfAllObj.Count); // which person or object or location to look, rotate or point to
		}
		float temp = Mathf.Floor (UnityEngine.Random.Range(0,2)); 
		bool isobject = (temp == 1)?(true):(false); // whether to use the object or the position for target
		
		float temp2 = Mathf.Floor (UnityEngine.Random.Range(0,2)); // whether char is following or not -- only needed if isobject = true & object is a char
		bool following = (temp2 == 1)?(true):(false);
		bool ischar = false;
		GameObject whichobj = null;
		Debug.Log ("Objnum="+objnum+", whichfunc="+whichfunction+", isobj="+isobject);
		
		if (isobject || whichfunction >=2) {
			if (whichfunction ==2 || whichfunction ==3) {
				whichobj = GlobalObjs.listOfPawnObj[objnum];
				ischar = false;
				following = false;
			} else {
				whichobj = GlobalObjs.listOfAllObj[objnum];
				foreach (GameObject g in GlobalObjs.listOfCharObj) {
					if (g.name == whichobj.name) {
						ischar = true;
						break;
					}
				}
			}
			/*switch (objnum) {
				case 0:
					ischar = true;
					whichobj = GlobalObjs.Hamlet;
					break;
				case 1:
					ischar = true;
					whichobj = GlobalObjs.Horatio;
					break;
				case 2:
						ischar = true;
					whichobj = GlobalObjs.GraveDigger;
					break;
				case 3:
					ischar = true;
					whichobj = GlobalObjs.GraveDiggerTwo;
					break;
				case 4:
					ischar = false;
					whichobj = GlobalObjs.Skull1;
					following = false;
					break;
				case 5:
					ischar = false;
					whichobj = GlobalObjs.Skull2;
					following = false;
					break;
				case 6:
					ischar = false;
					whichobj = GlobalObjs.Lantern;
				following = false;
					break;
				case 7:
					ischar = false;
					whichobj = GlobalObjs.Shovel;
				following = false;
					break;
				case 8:
					ischar = false;
					whichobj = GlobalObjs.Center;
				following = false;
					break;
				case 9:
					ischar = false;
					whichobj = GlobalObjs.CenterRight;
				following = false;
					break;
				case 10:
					ischar = false;
					whichobj= GlobalObjs.CenterBackStage;
				following = false;
					break;
				case 11:
					ischar = false;
					whichobj = GlobalObjs.DownStage;
				following = false;
					break;
				case 12:
					ischar = false;
					whichobj = GlobalObjs.Grave;
				following = false;
					break;
				
				case 13:
					ischar = false;
					whichobj = GlobalObjs.StageLeft;
				following = false;
					break;
				case 14:
					ischar = false;
					whichobj = GlobalObjs.StageRight;
				following = false;
					break;
				case 15:
					ischar = false;
					whichobj = GlobalObjs.Steps;
				following = false;
					break;
				case 16:
					ischar = false;
					whichobj = GlobalObjs.Stool;
				following = false;
					break;
				case 17:
					ischar = false;
					whichobj = GlobalObjs.UpStage;
				following = false;
					break;
				default:
					ischar = false;
					whichobj = null;
				following = false;
					break;				
			}*/
		}
		Debug.Log ("Whichobj="+((whichobj == null)?("NULL"):(whichobj.name)));
				
		switch (whichfunction) { // check which function to call
			case 0:
				// walk
				if (isobject) {
					who.doWalk(whichobj.transform.position.x, whichobj.transform.position.z, whichobj, following);
				} else {
					who.doWalk(targetx, targety, null, following);
				}
				break;
			case 1:
						// rotate
				if (isobject) {
					who.doRotate(whichobj.transform.position.x, whichobj.transform.position.z, whichobj);
				} else {
					who.doRotate(targetx, targety, null);
				}		
				break;
			case 2:
				who.doPickup(whichobj);
						// pickup
				break;
			case 3:
						// putdown
				who.doPutDown(whichobj);
				break;
			case 4:
					// point
				who.doPoint(whichobj);
				break;
			
		}
	}
	
	static void parseMovement(string name, string xmltxt) {
		CharFuncs who = GlobalObjs.getCharFunc(name);
		Debug.Log (who.thisChar.name);
		//string action;
		float targetx = -1;
		float targety = -1;
		float targetx2 = -1;
		float targety2 = -1;
		float targetx3 = -1;
		float targety3 = -1;
		float targetx4 = -1;
		float targety4 = -1;
		GameObject target = null;
		bool following = false;
		
		string myText = null;
		int startPos = 0;
		int endPos = 0;
		string targetstr;
		bool hasrel = false;
		string relstr = null;
		
		if (xmltxt.Contains ("follow=")) {
			startPos = xmltxt.IndexOf ("follow="+quote);
			following = true;
		} else {
			startPos = xmltxt.IndexOf ("target="+quote);
		}
		Debug.Log (xmltxt);
		Debug.Log ("startPos="+startPos);
		//Debug.Log ("start="+startPos);
		//Debug.Log (xmltxt.Substring (startPos));
		endPos = xmltxt.Substring (startPos+8).IndexOf (quote);
		//Debug.Log (xmltxt.Substring(startPos)[7]);
		//Debug.Log (xmltxt.Substring(startPos)[7] == quote);
		//Debug.Log ("end="+endPos);
		targetstr = xmltxt.Substring(startPos+8, endPos);
		
		
		Debug.Log("Parsed target="+targetstr);
		if (targetstr.IndexOf ("/") >= 0) { // relative position to calculate
			string[] reltarget = targetstr.Split (' ');
			// reltarget[0] = relative position
			// reltarget[1] = true target to lookup
			if (reltarget.Length > 1) {
				target = GlobalObjs.getObject (reltarget[1]);
			} else {
				target = null;
			}
			relstr = reltarget[0];
			hasrel = true;
			
		} else {
			if (targetstr.IndexOf(" ") > 0) {
				// this is a vector position, not an object
				string[] position = targetstr.Split (' ');
				bool success = float.TryParse(position[0], out targetx);
				success = float.TryParse (position[1], out targety);
				if (position.Length > 2) {
					success = float.TryParse(position[2], out targetx2);
					success = float.TryParse(position[3], out targety2);
					if (position.Length > 4) {
						success = float.TryParse (position[4], out targetx3);
						success = float.TryParse (position[5], out targety3);
						if (position.Length > 6) {
							success = float.TryParse(position[6], out targetx4);
							success = float.TryParse(position[7], out targety4);
						}
					}
				}
				
			} else {
				// this is an object
				target = GlobalObjs.getObject(targetstr);
				
			}
		}
		
		// find out what action to take
		if (xmltxt.Contains ("pick-up") || xmltxt.Contains ("PICK-UP")) {
			Debug.Log ("Action=pickup");
			if (mode == playmodes.rules || mode == playmodes.fdg) {
            	// check if close enough to pick up the object, else add a locomotion before the pickup
            	if (who.getDist(target.transform.position) > 1) {
					Debug.Log ("Adding move to object for "+who.thisChar.name+ " to "+target.name);
            		who.doWalk(target.transform.position.x, target.transform.position.z, target, false);
            	}
            }
			who.doPickup(target);	
			if (hasrel) {
				// add additional logic for what the relative thing is
				if (relstr == "/FORWARD") { // change to switch & add more!
					// calculate moving towards audience 20 paces
					Vector3 dir = Quaternion.Euler (Vector3.forward) * new Vector3(0,0,20) + who.transform.position; 
					Debug.Log ("Current rotation="+who.transform.rotation+", New dir="+dir+": cur postn="+who.transform.position);
					
					who.doWalk(dir.x, dir.z, null, false);
					who.doPutDown(target);
				}
			}
			
		} else if (xmltxt.Contains("put-down") || xmltxt.Contains ("PUT-DOWN")) {
			Debug.Log ("Action=putdown");
			who.doPutDown(target);
		} else if (xmltxt.Contains ("locomotion") || xmltxt.Contains ("LOCOMOTION")) {
			Debug.Log ("Action=move");
			if(mode == playmodes.fdg) {
				movementfound = true;
			}
			if (target != null) {
				if (hasrel) { // check for relative walking
					if (relstr == "/AROUND") { // add more & use switch!!
						// calculate new positions - do relative to the object? 5 points from object 
						Int16 i = -1;
						Int16 j = -1;
						if (who.transform.position.x > target.transform.position.x) {
							i = -1;
						} else {
							i = 1;
						}
						/*if (who.transform.position.z > target.transform.position.z) {
							j = -1;
						} else {
							j = 1;
						}*/
						who.doWalk (target.transform.position.x, target.transform.position.z + 8, null, false);
						who.doWalk (target.transform.position.x+8*i, target.transform.position.z, null, false);
						who.doWalk (target.transform.position.x, target.transform.position.z - 8, null, false);
						who.doWalk (target.transform.position.x -8*i, target.transform.position.z, null, false);
//						who.doWalk (who.transform.position.x - (2*(who.transform.position.x - target.transform.position.x)), who.transform.position.z - (2*(who.transform.position.z - target.transform.position.z)), null, following);
//						who.doWalk (who.transform.position.x, who.transform.position.z, null, following);
						
						// make char look at audience since cannot figure out where was looking? 
						GameObject audience = GlobalObjs.getObject("AUDIENCE");
						who.doRotate(audience.transform.position.x, audience.transform.position.z, audience);
					
					} else if (relstr == "/LEFT") {
						Vector3 dir = (Vector3.left*20) + who.transform.position;//Quaternion.Euler (who.transform.TransformDirection(new Vector3(0,90,0))) * new Vector3(0,0,50) + who.transform.position;
						who.doWalk(dir.x, dir.z, null, false);
					} else if (relstr == "/OPPOSITE") {
						Vector3 towhere = new Vector3(-1.0f*who.transform.position.x, 0, who.transform.position.z);
						who.doWalk (towhere.x, towhere.z, null, false);
					}
				} else {
					if (mode == playmodes.rules || mode == playmodes.fdg) {
						checkUpstaging(who, target.transform.position.x, target.transform.position.z, target, following);
					} else {
						who.doWalk (target.transform.position.x, target.transform.position.z, target, following);
					}
				}
			} else {
				if (hasrel) { // check for relative walking
					if (relstr == "/LEFT") {
						Vector3 dir = (Vector3.left*20)  + who.transform.position;//Quaternion.Euler (who.transform.TransformDirection(new Vector3(0,90,0))) * new Vector3(0,0,50) + who.transform.position;
						who.doWalk(dir.x, dir.z, null, false);
					} else if (relstr == "/FORWARD") {
						Vector3 dir = (Vector3.forward*20) + who.transform.position;
						who.doWalk (dir.x, dir.z, null, false);
					}
				} else {
					if (mode == playmodes.rules || mode == playmodes.fdg) {
						if (GlobalObjs.listOfChars.Count > 2) { // only worry about upstaging when there is more than 2 characters
							checkUpstaging(who, targetx, targety, null, following);
						} else {
							who.doWalk (targetx, targety, null, following);
						}
					} else {
						who.doWalk (targetx, targety, null, following);
					}
				}
				if (targetx2 != -1) {
					if (mode == playmodes.rules || mode == playmodes.fdg) {
						checkUpstaging(who, targetx2, targety2, null, following);
					} else {
						who.doWalk (targetx2, targety2, null, following); // this one should get queued
					}
					if (targetx3 != -1) {
						
						if (mode == playmodes.rules || mode == playmodes.fdg) {
							checkUpstaging(who, targetx3, targety3, null, following);
						} else {
							who.doWalk (targetx3, targety3, null, following); // this one should get queued
						}
						
						if (targetx4 != -1) {
							if (mode == playmodes.rules || mode == playmodes.fdg) {
								checkUpstaging(who, targetx4, targety4, null, following);
							} else {
								who.doWalk (targetx4, targety4, null, following); // this one should get queued
							}
						}
					}
				}
			}
		} else if (xmltxt.Contains ("gaze") || xmltxt.Contains ("GAZE")) {
			Debug.Log ("Action=turn");
			if (target != null) {
				who.doRotate(target.transform.position.x, target.transform.position.z, target);
			} else {
				if (hasrel) { // calculate relative positions
					if (relstr == "/LEFT") { // add more & use switch!!
						//Vector3 dir = who.transform.position + ((
						//Vector3 endPos = Quaternion.AngleAxis(90, Vector3.up)*50*Vector3.forward;
							Vector3 dir = Quaternion.Euler (who.transform.TransformDirection(new Vector3(0,-90,0))) * new Vector3(0,0,50) + who.transform.position; 
						Debug.Log ("Current rotation="+who.transform.rotation+", New dir="+dir+": cur postn="+who.transform.position);
						who.doRotate (dir.x, dir.z, null);
					} else {
						if (relstr == "/RIGHT") { // add more & use switch!!
							//Vector3 dir = who.transform.position + ((
							//Vector3 endPos = Quaternion.AngleAxis(90, Vector3.up)*50*Vector3.forward;
								Vector3 dir = Quaternion.Euler (who.transform.TransformDirection(new Vector3(0,90,0))) * new Vector3(0,0,50) + who.transform.position; 
							Debug.Log ("Current rotation="+who.transform.rotation+", New dir="+dir+": cur postn="+who.transform.position);
							who.doRotate (dir.x, dir.z, null);
						}
					}
				} else {
					who.doRotate(targetx, targety, null);
				}
			}
		} else if (xmltxt.Contains ("POINT") || xmltxt.Contains ("point")) {
			Debug.Log ("Action=point");
			if (target != null) {
				if (mode == playmodes.rules || mode == playmodes.fdg || mode == playmodes.practice) {
	            	// add rule to get everyone to look at the point to target
					Debug.Log ("Adding Everyone Look at pointed object "+target.name);
	            	foreach (CharFuncs c in GlobalObjs.listOfChars) {
	            		if (c.onstage() && c != who && !c.isHuman) {
	            			c.doRotate(target.transform.position.x, target.transform.position.z, target);
	            		}
	            	}
	            } else {
					who.doPoint(target);
				}
			} else {
				Debug.Log ("Error no target");
			}
		} else {
			Debug.Log ("Error - unknown command");
		}
		
		
	}

	public static void checkUpstaging(CharFuncs who, float targetx2, float targety2, GameObject g, bool following) {
		// check if my target is closer to audience 
		// 		checkmovetarget for move -- if upstage, adjust based on precedence -- check all chars movements, not just current position!!
		foreach (CharFuncs c in GlobalObjs.listOfChars) {
			if (c.onstage() && c.thisChar.name != who.thisChar.name && !who.isHuman) { // only check onstage chars and non-same chars
				switch (who.compareImportance(c)) {
					case "More":
						// make sure who is closer to audience -- consider target, not current loc
						if (targety2 < c.getLastMovePostn().z) {
							// change targety2
							Debug.Log ("Moving "+who.thisChar.name+" closer to audience");
							targety2 = c.getLastMovePostn().z + 1;
						}
						break;
					case "Less":
						// make sure who is further from audience -- consider target, not current loc
						if (targety2 > c.getLastMovePostn().z) {
							// change targety2
							Debug.Log ("Moving "+who.thisChar.name+" farther from audience");
							targety2 = c.getLastMovePostn().z - 1;
						}
						break;
					default:
						// error, leave as-is
						break;
				}
			} // if not onstage, do nothing
		}
		// check to be sure not too close to another character first
		foreach (CharFuncs c in GlobalObjs.listOfChars) {
			if (c.onstage() && c.thisChar.name != who.thisChar.name && !who.isHuman) {
				if (who.getDist(c.getLastMovePostn()) < 3) { // is this really the distance???
					// move apart
					Debug.Log ("NEED TO SEPARATE!!!");
				}
			}
		}
		// go ahead and walk to new target
		who.doWalk(targetx2, targety2, g, following);
		
		
		// for each char onstage
		// 	if char = actor do nothing
		// 	else
		// 	if actor precedence < char precedence & char diet to aud < actor diet to aud
		// 		move actor target closer to audience
		// 	if actor precedence > char precedence & actor diet < char diet to aud
		// 		move actor target farther from audience
		// end for
		// for each char onstage
		// 	if char = actor do nothing
		// 	else
		// 	if diet from actor to char < 3 feet, then move apart in opposite path of where they are
		// end for
		// move actor
		// should I be tracking this for the non-actors too??
		
	}
	
	public static void LoadChars() {
		//if (fileindexNumber == 0) {
		//	Debug.Log ("ERROR - no file chosen");
		//} else {
		//	Debug.Log ("Loading file:" + fullfilelist[fileindexNumber-1]);	
		
		
			// Be sure to check that a file is selected!!!
			
			// Read file, then generate pawns and marks and characters & setup global variables
			// format: Type    Speed    Name    X    Z    R1    R2    R3    R4    Holding    Color    Importance    Voice
			// Type: C or P or M for type of object - S for speed
			// Speed: Slow, Med, Fast (only for S)
			// Name: uppercase name with no spaces
			// Start X Position
			// Start Z Position
			// Rotation 4 components
			// Holding Object: define prior to the character!!
			// Color: blue, purple, red, green, yellow, orange, brown, white
			// Importance: 1 to 8 for chars only saying 1 = highest priority char to lowest - only chars
			// Voice: Alex, Ralph, Bruce, Fred, more? Kathy, Vicki, Victoria, Agnes, Princess, Junior
			string tempfile =  @"Files/";
			if (scenecount == 1) {
				tempfile = tempfile + "PracticeInit";
			} else {
				tempfile = tempfile + "ErnestCharInit";
			}
			TextAsset textFile = (TextAsset)Resources.Load (tempfile);
			Debug.Log ("Text="+tempfile);
			StringReader charfile = new StringReader (textFile.text);
			string curLine = charfile.ReadLine ();
			string[] parsedLine = null; 
			string name = null;
			float startx;
			float startz;
			float rotation1;
			float rotation2;
			float rotation3;
			float rotation4;
			string objectheld;
			int priority;
			Color pcolor;
			bool success = false;
			//Debug.Log (curLine);
			
			GlobalObjs.priorityList.Capacity = 15;
			charlist = new GameObject[15];
			charlisttext = new string[15];
			targetlist = new GameObject[75];
			targetlisttext = new string[75];
			int cindex = 0;
			int oindex = 0;
			
			while (curLine != null) { // read each line
				parsedLine = curLine.Split ('\t');
				Debug.Log (curLine);
				Debug.Log (parsedLine[0]);
				switch (parsedLine[0]) {
					
					case "C":
					case "H":
						// define character
						name = parsedLine[2];
						success = float.TryParse (parsedLine[3], out startx);
						success = float.TryParse (parsedLine[4], out startz);
						success = float.TryParse (parsedLine[5], out rotation1);
						success = float.TryParse (parsedLine[6], out rotation2);
						success = float.TryParse (parsedLine[7], out rotation3); 
						success = float.TryParse (parsedLine[8], out rotation4);
						objectheld = parsedLine[9];
						// set color voice importance
						// need to know how many people so create right size char array for importance?
						
						GameObject person = (GameObject)Instantiate (InitScript.Instance.prefabchar, new Vector3(startx, 0, startz), new Quaternion(rotation1, rotation2, rotation3, rotation4));
						person.name = name;
						// get charfuncs & fire pickup if objectheld is defined
						CharFuncs personfunc = (CharFuncs) person.GetComponent (typeof(CharFuncs));
						
						
						// set voice
						personfunc.voice = parsedLine[12];
						// set material
					Debug.Log ("x"+parsedLine[10]+"x");
						personfunc.bodycolor = parsedLine[10];
						//personfunc.bodycolor = "BoxPersonColorOnly-GREEN";
/*						foreach(Material myMaterial in  Resources.FindObjectsOfTypeAll(typeof(Material))) {
				            //Debug.Log ("Material="+myMaterial.name);
				            if (myMaterial.name == parsedLine[10]) {
				                person.gameObject.renderer.material = myMaterial;
				                Debug.Log ("Found "+parsedLine[10]);
				            }
						}*/
						
						personfunc.armcolor = parsedLine[10].Replace ("Mat", "");
						Debug.Log ("y"+personfunc.armcolor+"y");
						// set myicon variable to image of appropriately colored icon from texture folder
						personfunc.myicon = Resources.Load("Textures/"+personfunc.armcolor+"icon") as Texture;
//        prefab = (GameObject) Resources.LoadAssetAtPath("Assets/Artwork/mymodel.fbx", typeof(GameObject))
					
        
						GlobalObjs.listOfChars.Add (personfunc);
						GlobalObjs.listOfCharObj.Add(person);
						GlobalObjs.listOfAllObj.Add(person);
					
						targetlisttext[oindex] = name;
						targetlist[oindex] = person;
						oindex++;
						// add to priority array
						success = int.TryParse (parsedLine[11], out priority);
						
						// check how big current priorityList is
						/*if (priority+1 > GlobalObjs.priorityList.Count) {
							// make bigger & save what is in there now
							
						}*/
					
						GlobalObjs.priorityList.Insert(priority, person);
						//person.gameObject.tag = "Person"; // don't need this?
					
						/*if (objectheld != null && objectheld != "null") {
							GameObject objfound = GlobalObjs.getObject(objectheld);
							personfunc.doPickup(objfound);
						}*/ // TODO - fix how to get them to pickup for initialization
							
						charlisttext[cindex] = name;
						charlist[cindex] = person;
						cindex++;
						if (parsedLine[0] == "H") {
							personfunc.isHuman = true;
							GlobalObjs.human = person;
							GlobalObjs.humanfunc = personfunc;
						}
						Debug.Log ("Created char="+name);
						break;
					case "P":
						// define pawn
						name = parsedLine[2];
						success = float.TryParse (parsedLine[3], out startx);
						success = float.TryParse (parsedLine[4], out startz);
							
					
						GameObject pawn = (GameObject)Instantiate (InitScript.Instance.prefabobj, new Vector3(startx, 0.5f, startz), new Quaternion(0,0,0,0));
						pawn.name = name;
						// TODO set material?
						switch(parsedLine[5]) {
							case "Blue":
								pcolor = Color.blue;
								break;
							case "Yellow":
								pcolor = Color.yellow;
								break;
							case "Orange":
								pcolor = new Color(255.0f/255.0f, 153.0f/255.0f, 51.0f/255.0f);
						Debug.Log ("ORANGE!!!");
								break;
							case "Green":
								pcolor = Color.green;
						Debug.Log ("GREEN!");
								break;
							case "Pink":
								pcolor = Color.magenta;
								break;
							case "Cyan":
								pcolor = Color.cyan;
								break;
							case "Grey":
							case "Gray":
								pcolor = Color.gray;
								break;
							case "Red":
								pcolor = Color.red;
								break;
							case "Purple":
								pcolor = new Color(76.0f/255.0f, 0.0f/255.0f, 153.0f/255.0f);//76 0 153
								break;
							case "Brown":
								pcolor = new Color(102.0f/255.0f, 51.0f/255.0f, 0.0f/255.0f);
								break;
							case "White":
								pcolor = Color.white;
								break;
						
							default:
								pcolor = Color.white;
								break;
							
						}
					//Debug.Log (parsedLine[5]);
					pawn.renderer.material.color = pcolor;
//11					pawn.renderer.material.shader = Shader.Find ("Diffuse");
						//rend.material.shader = Shader.Find("Specular");
       // rend.material.SetColor("_SpecColor", Color.red);
//11						pawn.renderer.material.SetColor("_Color", pcolor);
//						color = pcolor;
					
						
						GlobalObjs.listOfPawnObj.Add (pawn);
						GlobalObjs.listOfAllObj.Add (pawn);
						//pawn.gameObject.tag = "Pawn"; // don't need this?
						
						targetlisttext[oindex] = name;
						targetlist[oindex] = pawn;
						oindex++;
						Debug.Log ("Created pawn="+name);
						break;
					case "M":
						// define mark
						name = parsedLine[2];
						success = float.TryParse (parsedLine[3], out startx);
						success = float.TryParse (parsedLine[4], out startz);	
				//Debug.Log (name + "; ParsedLine="+parsedLine[4]);
						string ext = parsedLine[5]; // gives color for mark
						float a = -0.5f;
						if (ext != "Transparent") {
							a = -0.9f;
						}
					
						GameObject mark = (GameObject)Instantiate (InitScript.Instance.prefabmark, new Vector3(startx, a, startz), new Quaternion(0,0,0,0));
						mark.name = name;
				
						GlobalObjs.listOfAllObj.Add (mark);
						//mark.gameObject.tag = "Mark"; // don't need this?
						
						// don't need this line, need to apply texture instead
						if (ext == "Transparent") {
							mark.renderer.material.color = Color.clear;
						} else {
							//mark.renderer.material.color = Color.clear;
							Texture2D tempTexture = (Texture2D)Resources.Load("Textures/Xmark2"+ext)as Texture2D;
	 						mark.renderer.material.mainTexture=tempTexture;
						}
						
						targetlisttext[oindex] = name;
						targetlist[oindex] = mark;
						oindex++;
						Debug.Log ("Created mark="+name);
						break;
					case "S":
						// define speed of script
						switch (parsedLine[1]) {
							case "Slow": // TODO make slower
								CharFuncs.mspeed = 5;
								CharFuncs.timerMax = 2.0f;
								CharFuncs.rspeed = 50;
								CharFuncs.sspeed = 20f;
								CharFuncs.pointertimerMax = 2.0f;
								break;
							case "Med":
								CharFuncs.mspeed = 5;
								CharFuncs.timerMax = 2.0f;
								CharFuncs.rspeed = 50;
								CharFuncs.sspeed = 20f;
								CharFuncs.pointertimerMax = 2.0f;
								break;
							case "Fast": // TODO make faster
								CharFuncs.mspeed = 5;
								CharFuncs.timerMax = 2.0f;
								CharFuncs.rspeed = 50;
								CharFuncs.sspeed = 20f;
								CharFuncs.pointertimerMax = 2.0f;
								break;
							default:
								// do nothing
								break;
							
						}
						
						Debug.Log ("Set speed="+parsedLine[1]);
						break;
					case "T": // commenting out - not needed anymore since put at start
						//legendTexture = Resources.Load ("Textures/" + parsedLine[1]) as Texture;
						break;
					default:
						// nothing
						Debug.Log ("ERROR parsing file");
						break;
				}
				
				
				curLine = charfile.ReadLine ();
			}
			/*
			charlist = new GameObject[5];
			charlist[0] = null;
			charlist[1] = GlobalObjs.Hamlet;
			charlist[2] = GlobalObjs.Horatio;
			charlist[3] = GlobalObjs.GraveDigger;
			charlist[4] = GlobalObjs.GraveDiggerTwo;
			
			charlisttext = new string[5];
			charlisttext[0] = "Choose Character:";
			for (int i = 1; i < charlist.Length; i++) {
				charlisttext[i] = charlist[i].name;
			}
			
			targetlist = new GameObject[20];
			targetlist[0] = null;
			targetlist[1] = GlobalObjs.Hamlet;
			targetlist[2] = GlobalObjs.Horatio;
			targetlist[3] = GlobalObjs.GraveDigger;
			targetlist[4] = GlobalObjs.GraveDiggerTwo;
			targetlist[5] = GlobalObjs.Skull1;
			targetlist[6] = GlobalObjs.Skull2;
			targetlist[7] = GlobalObjs.Shovel;
			targetlist[8] = GlobalObjs.Lantern;
			//targetlist[8] = GlobalObjs.Box;
			targetlist[9] = GlobalObjs.Audience;
			targetlist[10] = GlobalObjs.Grave;
			targetlist[11] = GlobalObjs.StageRight;
			targetlist[12] = GlobalObjs.CenterBackStage;
			targetlist[13] = GlobalObjs.Center;
			targetlist[14] = GlobalObjs.CenterRight;
			targetlist[15] = GlobalObjs.DownStage;
			targetlist[16] = GlobalObjs.StageLeft;
			targetlist[17] = GlobalObjs.Steps;
			targetlist[18] = GlobalObjs.Stool;
			targetlist[19] = GlobalObjs.UpStage;
			
			targetlisttext = new string[20];
			targetlisttext[0] = "Choose Target:";
			for (int i = 1; i < targetlist.Length; i++) {
				targetlisttext[i] = targetlist[i].name;
			}
			*/
			charfile.Close ();
			GlobalObjs.priorityList.TrimExcess(); // removes blank areas
			
			targetlisttext[oindex] = "/LEFT";
						//targetlist[oindex] = mark;
						oindex++;
			targetlisttext[oindex] = "/FORWARD";
			oindex++;
			
			targetlisttext[oindex] = "/AROUND "+targetlisttext[0];
			oindex++;
			
			
		//}
	}
	
	public void RunAction() {
		// use the charlist, charindexNumber, actionlist, actionindexNumber, targetlist, targetindexNumber, txtx, txty, txtsay
		Debug.Log ("Running Action for "+charlist[charindexNumber].name);
		CharFuncs who = (CharFuncs) charlist[charindexNumber].GetComponent (typeof(CharFuncs));
		CharFuncs target = targetlist[targetindexNumber] != null?(CharFuncs) targetlist[targetindexNumber].GetComponent (typeof(CharFuncs)):null;
		float x;
		float y;
		bool success = float.TryParse (txtx, out x);
		success = float.TryParse (txty, out y);
		
		
		switch (actionindexNumber) {
		case 0:
		case 1:
			if (targetlisttext[targetindexNumber] == "/LEFT") {
				
				Vector3 dir = (Vector3.left*20)  + who.transform.position;//Quaternion.Euler (who.transform.TransformDirection(new Vector3(0,90,0))) * new Vector3(0,0,50) + who.transform.position;
				Debug.Log("WALKING LEFT:"+dir);
				who.doWalk(dir.x, dir.z, null, false);
			} else if (targetlisttext[targetindexNumber] == "/FORWARD") {
				
				Vector3 dir = (Vector3.forward*20)  + who.transform.position;//Quaternion.Euler (who.transform.TransformDirection(new Vector3(0,90,0))) * new Vector3(0,0,50) + who.transform.position;
				Debug.Log("WALKING FORWARD:"+dir);
				who.doWalk(dir.x, dir.z, null, false);
			} else if (targetlisttext[targetindexNumber] == "/AROUND TABLE") {
				Int16 i = -1;
				GameObject ptarget = targetlist[0];
						
						if (who.transform.position.x > ptarget.transform.position.x) {
							i = -1;
						} else {
							i = 1;
						}
					
						who.doWalk (ptarget.transform.position.x, ptarget.transform.position.z + 8, null, false);
						who.doWalk (ptarget.transform.position.x+8*i, ptarget.transform.position.z, null, false);
						who.doWalk (ptarget.transform.position.x, ptarget.transform.position.z - 8, null, false);
						who.doWalk (ptarget.transform.position.x -8*i, ptarget.transform.position.z, null, false);						
			} else {
				who.doWalk(x, y, targetlist[targetindexNumber], (target == null?false:true));
			}
			break;
		case 2:
			who.doRotate(x, y, targetlist[targetindexNumber]);
			break;
		case 3:
			who.doPickup (targetlist[targetindexNumber]);
			break;
		case 4:
			who.doPutDown (targetlist[targetindexNumber]);
			break;
		case 5:
			who.doSpeak(txtsay);
			break;
		case 6:
			who.doPoint (targetlist[targetindexNumber]);
			break;
		case 7:
			who.doWalk (x, y, null, false);
			break;
		case 8:
			foreach(GameObject g in GlobalObjs.listOfCharObj) {
				Debug.Log (g.name+"="+g.transform.position+","+g.transform.rotation);
				if (g.transform.childCount != 0) {
					Debug.Log (g.name+" children=");
					for (int i=0; i< g.transform.childCount; i++) {
						Debug.Log (g.transform.GetChild(i).name);
					}
					Debug.Log ("End "+g.name+" children");
				}
			}
			break;
		case 9:
			who.doWalk(x, y, targetlist[targetindexNumber], true);
			break;
			
			
			
			//"Choose Action:", "Walk", "Look", "Pickup", "Putdown", "Speak", "Point"
			
		}
		
				
	}
	
	
	void OnLevelWasLoaded(int level) {
		// general initializations
		Instance = this;

		
		dataPath = Application.dataPath;
		

		DateTime timeSpan = System.DateTime.Now;
		participantID = timeSpan.ToString("MMddHHmmssfff");
		
		spacing = heightimg+heighttext+5f;
		mycolor = GUI.backgroundColor;
		mytexture = new Texture2D(Screen.width, Screen.height); // orange
		int y = 0;
        while (y < mytexture.height) {
            int x = 0;
            while (x < mytexture.width) {
                //Color color = ((x & y) ? Color.white : Color.gray);
                mytexture.SetPixel(x, y, new Color(255f/255f, 127f/255f, 0f/255f));//new Color(51f/255f, 178f/255f, 146f/255f)); // turquoise
                ++x;
            }
            ++y;
        }
        mytexture.Apply();
		mytexture2 = new Texture2D(Screen.width, Screen.height); // brown
		y = 0;
        while (y < mytexture2.height) {
            int x = 0;
            while (x < mytexture2.width) {
                //Color color = ((x & y) ? Color.white : Color.gray);
                mytexture2.SetPixel(x, y, new Color(89f/255f, 64f/255f, 39f/255f));//new Color(51f/255f, 178f/255f, 146f/255f)); // turquoise
                ++x;
            }
            ++y;
        }
        mytexture2.Apply();
		
		mytexture3 = new Texture2D(Screen.width, Screen.height); // yellow
		y = 0;
		while (y < mytexture3.height) {
			int x=0;
			while (x < mytexture3.width) {
				mytexture3.SetPixel(x, y, Color.yellow);
				++x;
			}
			++y;
		}
		mytexture3.Apply();
		
		newstyle = new GUIStyle();
		newstyle.normal.background = mytexture;
		newstyle.fontSize = 30;
		newstyle.normal.textColor = Color.black;
		
		buttonstyle = new GUIStyle();
		buttonstyle.normal.background = (Texture2D)scriptBkgrd;
		buttonstyle.normal.textColor = Color.white;
		
		
		if (level == 1 || level == 2) {
				
				LoadChars ();
				//RunPlay();
				starting = true;
				timer = 0.0f;
				// update method picks this up and sets the stuff to start the play & set the files to use
		}
		
		// **************************************
		// Auto-load first set of files for practice
		// **************************************
		
		legendTexture = Resources.Load ("Textures/ErnestLegend") as Texture;
		
		// commenting out because logfile isn't defined yet - gets defined in runplay() function
		/*
		// initialize click log file
		using(System.IO.StreamWriter myFile = new System.IO.StreamWriter(dataPath + @"//Logs//"+logFile+"click.log", true)) {
			
			myFile.WriteLine("Time\tMouse Click X\tMouse Click Y\tName of Object Clicked On\tFloor Location X\tFloor Location Z\tWhat Option Chosen from Menu");
				
		}
		
		// initialize character position log files
		foreach(GameObject c in GlobalObjs.listOfCharObj) {
			using(System.IO.StreamWriter myFile = new System.IO.StreamWriter(dataPath + @"//Logs//"+logFile+c.name+".log", true)) {
				
				myFile.WriteLine("Time\tX Position\tZ Position\tY Rotation Euler Angle");
					
			}
				
		}*/
		
		
		// level specific initializations
		if (level == 2) { // NLP
            Debug.Log ("NLP Loaded");
		} else if (level == 1) { // FDG
			Debug.Log ("FDG Loaded");
		} else if (level == 0) { // Practice
			Debug.Log ("Practice Loaded");
		}
		// start was clicked for level 1 or 2, so in that case, go ahead and start / remove the prompts on the screen so no double click required
		
	}
	
	
}
