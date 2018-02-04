using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GlobalObjs : MonoBehaviour
{
	
	/*public static GameObject Hamlet = null;
	public static CharFuncs HamletFunc = null;
	public static GameObject Horatio = null;
	public static CharFuncs HoratioFunc = null;
	public static GameObject GraveDigger = null;
	public static CharFuncs GraveDiggerFunc = null;
	public static GameObject GraveDiggerTwo = null;
	public static CharFuncs GraveDiggerTwoFunc = null;
	GameObject[] templist = null;*/
	
	public static List<CharFuncs> listOfChars = new List<CharFuncs>();
	public static List<GameObject> listOfCharObj = new List<GameObject>();
	public static List<GameObject> listOfAllObj = new List<GameObject>();
	public static List<GameObject> listOfPawnObj = new List<GameObject>();
	public static GameObject human;
	public static CharFuncs humanfunc;
	
	public static List<GameObject> priorityList = new List<GameObject>();
	
	public static List<QueueObj> globalQueue = new List<QueueObj>();
	static bool isfirst = true;
	
	// scrolling script variables
	public static bool showwarning = false;
	public static float targetTopAllText = 0f;
	public static float currentTopAllText = 0f;
	public static float boxTop = 0f;
	public static float boxHeight = 0f;
	
	public static bool hasspeech = false;
	public static bool ready = false;
	public static List<GameObject> toDeleteList = new List<GameObject>();
	
	
//	public static Queue<QueueObj> globalQueue = new Queue<QueueObj>();
	/*public static GameObject Skull1 = null;
	public static GameObject Skull2 = null;
	public static GameObject Shovel = null;
	public static GameObject Lantern = null;
	public static GameObject Box = null;
	public static GameObject Audience = null;
	public static GameObject Grave = null;
	public static GameObject StageRight = null;
	public static GameObject CenterBackStage = null;
	public static GameObject Center = null;
	public static GameObject CenterRight = null;
	public static GameObject DownStage = null;
	public static GameObject StageLeft = null;
	public static GameObject Steps = null;
	public static GameObject Stool = null;
	public static GameObject UpStage = null;*/
	
	
	// Use this for initialization
	void Start () {
		if (isfirst) {
			isfirst = false;
//			#if UNITY_EDITOR
            Material m = Resources.Load("Materials/BLUEarmmat") as Material;
            m = Resources.Load("Materials/PURPLEarmmat") as Material;
            m = Resources.Load("Materials/REDarmmat") as Material;
            m = Resources.Load("Materials/GREENarmmat") as Material;
			m = Resources.Load("Materials/BROWNarmmat") as Material;
			m = Resources.Load("Materials/CYANarmmat") as Material;
			m = Resources.Load("Materials/ORANGEarmmat") as Material;
			m = Resources.Load("Materials/PEACHarmmat") as Material;
			m = Resources.Load("Materials/PINKarmmat") as Material;
			m = Resources.Load("Materials/YELLOWarmmat") as Material;
			//#endif
		}
		/*if (Hamlet == null) {
			templist = GameObject.FindGameObjectsWithTag("Hamlet");
			Hamlet = templist[0];
			HamletFunc = (CharFuncs) Hamlet.GetComponent (typeof(CharFuncs));
			listOfChars.Add(HamletFunc);
			templist = null;
		}
		if (Horatio == null) {
			templist = GameObject.FindGameObjectsWithTag("Horatio");
			Horatio = templist[0];
			HoratioFunc = (CharFuncs) Horatio.GetComponent (typeof(CharFuncs));
			listOfChars.Add(HoratioFunc);
			templist = null;
		}
		if (GraveDigger == null) {
			templist = GameObject.FindGameObjectsWithTag("GraveDigger");
			GraveDigger = templist[0];
			GraveDiggerFunc = (CharFuncs) GraveDigger.GetComponent (typeof(CharFuncs));
			listOfChars.Add(GraveDiggerFunc);
			templist = null;
		}
		if (GraveDiggerTwo == null) {
			templist = GameObject.FindGameObjectsWithTag("GraveDiggerTwo");
			GraveDiggerTwo = templist[0];
			GraveDiggerTwoFunc = (CharFuncs) GraveDiggerTwo.GetComponent (typeof(CharFuncs));
			listOfChars.Add(GraveDiggerTwoFunc);
			templist = null;
		}
		if (Skull1 == null) {
			templist = GameObject.FindGameObjectsWithTag ("Skull1");
			Skull1 = templist[0];
			templist = null;
		}
		if (Skull2 == null) {
			templist = GameObject.FindGameObjectsWithTag ("Skull2");
			Skull2 = templist[0];
			templist = null;
		}
		if (Lantern == null) {
			templist = GameObject.FindGameObjectsWithTag ("Lantern");
			Lantern = templist[0];
			templist = null;
		}
		if (Shovel == null) {
			templist = GameObject.FindGameObjectsWithTag ("Shovel");
			Shovel = templist[0];
			templist = null;
		}
		if (Audience == null) {
			templist = GameObject.FindGameObjectsWithTag ("Audience");
			Audience = templist[0];
			templist = null;
		}
		if (Grave == null) {
			templist = GameObject.FindGameObjectsWithTag ("Grave");
			Grave = templist[0];
			templist = null;
		}
		if (StageRight == null) {
			templist = GameObject.FindGameObjectsWithTag ("StageRight");
			StageRight = templist[0];
			templist = null;
		}
		if (CenterBackStage == null) {
			templist = GameObject.FindGameObjectsWithTag ("CenterBackStage");
			CenterBackStage = templist[0];
			templist = null;
		}
		if (Center == null) {
			templist = GameObject.FindGameObjectsWithTag ("Center");
			Center = templist[0];
			templist = null;
		}
		if (CenterRight == null) {
			templist = GameObject.FindGameObjectsWithTag ("CenterRight");
			CenterRight = templist[0];
			templist = null;
		}
		if (DownStage == null) {
			templist = GameObject.FindGameObjectsWithTag ("DownStage");
			DownStage = templist[0];
			templist = null;
		}
		if (StageLeft == null) {
			templist = GameObject.FindGameObjectsWithTag ("StageLeft");
			StageLeft = templist[0];
			templist = null;
		}
		if (Steps == null) {
			templist = GameObject.FindGameObjectsWithTag ("Steps");
			Steps = templist[0];
			templist = null;
		}
		if (Stool == null) {
			templist = GameObject.FindGameObjectsWithTag ("Stool");
			Stool = templist[0];
			templist = null;
		}
		if (UpStage == null) {
			templist = GameObject.FindGameObjectsWithTag ("UpStage");
			UpStage = templist[0];
			templist = null;
		}*/
		/*if (Box == null) {
			templist = GameObject.FindGameObjectsWithTag("Box");
			for (int i=0; i<templist.Length; i++) {
				if (templist[i].name == "Skull1") {
					Box = templist[i];
				}
			}
			//Box = templist[0];
			templist = null;
		}*/
		
		/*#if UNITY_EDITOR
            Material m = AssetDatabase.LoadAssetAtPath("Assets/Materials/Hamletmat.mat", typeof(Material)) as Material;
            m = Resources.LoadAssetAtPath("Assets/Materials/Horatiomat.mat", typeof(Material)) as Material;
            m = AssetDatabase.LoadAssetAtPath("Assets/Materials/GraveDiggermat.mat", typeof(Material)) as Material;
            m = AssetDatabase.LoadAssetAtPath("Assets/Materials/GraveDiggerTwomat.mat", typeof(Material)) as Material;
			#endif*/
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*if (Hamlet == null) {
			templist = GameObject.FindGameObjectsWithTag("Hamlet");
			Hamlet = templist[0];
			HamletFunc = (CharFuncs) Hamlet.GetComponent (typeof(CharFuncs));
			listOfChars.Add(HamletFunc);
			templist = null;
		}
		if (Horatio == null) {
			templist = GameObject.FindGameObjectsWithTag("Horatio");
			Horatio = templist[0];
			HoratioFunc = (CharFuncs) Horatio.GetComponent (typeof(CharFuncs));
			listOfChars.Add (HoratioFunc);
			templist = null;
		}
		if (GraveDigger == null) {
			templist = GameObject.FindGameObjectsWithTag("GraveDigger");
			GraveDigger = templist[0];
			GraveDiggerFunc = (CharFuncs) GraveDigger.GetComponent (typeof(CharFuncs));
			listOfChars.Add(GraveDiggerFunc);
			templist = null;
		}
		if (GraveDiggerTwo == null) {
			templist = GameObject.FindGameObjectsWithTag("GraveDiggerTwo");
			GraveDiggerTwo = templist[0];
			GraveDiggerTwoFunc = (CharFuncs) GraveDiggerTwo.GetComponent (typeof(CharFuncs));
			listOfChars.Add(GraveDiggerTwoFunc);
			templist = null;
		}
		if (Skull1 == null) {
			templist = GameObject.FindGameObjectsWithTag ("Skull1");
			Skull1 = templist[0];
			templist = null;
		}
		if (Skull2 == null) {
			templist = GameObject.FindGameObjectsWithTag ("Skull2");
			Skull2 = templist[0];
			templist = null;
		}
		if (Lantern == null) {
			templist = GameObject.FindGameObjectsWithTag ("Lantern");
			Lantern = templist[0];
			templist = null;
		}
		if (Shovel == null) {
			templist = GameObject.FindGameObjectsWithTag ("Shovel");
			Shovel = templist[0];
			templist = null;
		}
		if (Audience == null) {
			templist = GameObject.FindGameObjectsWithTag ("Audience");
			Audience = templist[0];
			templist = null;
		}
		if (Grave == null) {
			templist = GameObject.FindGameObjectsWithTag ("Grave");
			Grave = templist[0];
			templist = null;
		}
		if (StageRight == null) {
			templist = GameObject.FindGameObjectsWithTag ("StageRight");
			StageRight = templist[0];
			templist = null;
		}
		if (CenterBackStage == null) {
			templist = GameObject.FindGameObjectsWithTag ("CenterBackStage");
			CenterBackStage = templist[0];
			templist = null;
		}
		if (Center == null) {
			templist = GameObject.FindGameObjectsWithTag ("Center");
			Center = templist[0];
			templist = null;
		}
		if (CenterRight == null) {
			templist = GameObject.FindGameObjectsWithTag ("CenterRight");
			CenterRight = templist[0];
			templist = null;
		}
		if (DownStage == null) {
			templist = GameObject.FindGameObjectsWithTag ("DownStage");
			DownStage = templist[0];
			templist = null;
		}
		if (StageLeft == null) {
			templist = GameObject.FindGameObjectsWithTag ("StageLeft");
			StageLeft = templist[0];
			templist = null;
		}
		if (Steps == null) {
			templist = GameObject.FindGameObjectsWithTag ("Steps");
			Steps = templist[0];
			templist = null;
		}
		if (Stool == null) {
			templist = GameObject.FindGameObjectsWithTag ("Stool");
			Stool = templist[0];
			templist = null;
		}
		if (UpStage == null) {
			templist = GameObject.FindGameObjectsWithTag ("UpStage");
			UpStage = templist[0];
			templist = null;
		}*/
		/*if (Box == null) {
			templist = GameObject.FindGameObjectsWithTag("Box");
			for (int i=0; i<templist.Length; i++) {
				if (templist[i].name == "Skull1") {
					Box = templist[i];
				}
			}
			//Box = templist[0];
			templist = null;
		}*/
		
		if (InitScript.started && GlobalObjs.globalQueue.Count == 0) {
			// read next set of lines
			//Debug.Log ("Calling next Step, no items in queue");
			// destroy temporary items:
			
			foreach(GameObject g in toDeleteList) {
				Destroy (g);
				toDeleteList.Remove(g);
			}
			
			// have next button show
			ready = true;
//			InitScript.callNextStep();
		} 
	
	}
	
	public static GameObject getObject(string name) {
		name = name.ToUpper();
		foreach(GameObject g in listOfAllObj) {
			if(g.name == name) {
				return g;
			}
		}
		return null;
		
		/*
		//Debug.Log ("Getting object for:"+name);
		switch (name) {
		case "hamlet":
			return Hamlet;
			break;
		case "horatio":
			return Horatio;
			break;
		case "gravedigger":
			return GraveDigger;
			break;
		case "gravediggertwo":
			return GraveDiggerTwo;
			break;
		case "skull1":
			return Skull1;
			break;
		case "skull2":
			return Skull2;
			break;
		case "shovel":
			return Shovel;
			break;
		case "lantern":
			return Lantern;
			break;
		case "audience":
			return Audience;
			break;
		case "grave":
			return Grave;
			break;
		case "stageright":
			return StageRight;
			break;
		case "centerbackstage":
			return CenterBackStage;
			break;
		case "center":
			return Center;
			break;
		case "centerright":
			return CenterRight;
			break;
		case "downstage":
			return DownStage;
			break;
		case "stageleft":
			return StageLeft;
			break;
		case "steps":
			return Steps;
			break;
		case "stool":
			return Stool;
			break;
		case "upstage":
			return UpStage;
			break;
		default:
			return null;
			break;
		}*/
	}
	
	public static CharFuncs getCharFunc(string name) {
		name = name.ToUpper();
		foreach(CharFuncs c in listOfChars) {
			if (c.name == name) {
				return c;
			}
		}
		return null;
		/*
		switch (name) {
		case "hamlet":
			return HamletFunc;
			break;
		case "horatio":
			return HoratioFunc;
			break;
		case "gravedigger":
			return GraveDiggerFunc;
			break;
		case "gravediggertwo":
			return GraveDiggerTwoFunc;
			break;
		default:
			return null;
			break;
		}*/
		
	}
	
	public static CharFuncs getCharFunc(GameObject o) {
		foreach(CharFuncs c in listOfChars) {
			if (c.name == o.name) {
				return c;
			}
		}
		return null;
		
/*		switch (o.name) {
		case "Hamlet":
			return HamletFunc;
			break;
		case "Horatio":
			return HoratioFunc;
			break;
		case "GraveDigger":
			return GraveDiggerFunc;
			break;
		case "GraveDiggerTwo":
			return GraveDiggerTwoFunc;
			break;
		default:
			return null;
			break;
		}*/
	}
	
	public static void removeOne(int which) {
		int removethis = -1;
		for (int i=0; i< GlobalObjs.globalQueue.Count; i++) {
			if (GlobalObjs.globalQueue[i].msgNum == which) {
				// then remove this one
				removethis = i;
				break;
			}
		}
		if (GlobalObjs.globalQueue[removethis].action == QueueObj.actiontype.speak) {
			GlobalObjs.globalQueue[removethis].actorFunc.speakNum = -1;
		} else if (GlobalObjs.globalQueue[removethis].action == QueueObj.actiontype.point) {
			GlobalObjs.globalQueue[removethis].actorFunc.pointnum = -1;
		} else if (GlobalObjs.globalQueue[removethis].action == QueueObj.actiontype.intermission) {
			// do nothing
		} else {
			GlobalObjs.globalQueue[removethis].actorFunc.workingNum = -1;
		}
		Debug.Log ("Removed msg="+which+", item="+removethis);
		
		GlobalObjs.globalQueue.RemoveAt(removethis);
		//printQueue ("After removing one");
/*		if (GlobalObjs.globalQueue.Count == 0) {
			// read next set of lines
			Debug.Log ("Calling next Step, no items in queue");
			InitScript.callNextStep();
		} else {*/
			//Debug.Log ("*****Left in Queue:");
			//for (int j=0; j < GlobalObjs.globalQueue.Count; j++) {
			//	Debug.Log (GlobalObjs.globalQueue[j].msgNum+" - " +GlobalObjs.globalQueue[j].actorObj.name + " " + GlobalObjs.globalQueue[j].action.ToString());
			//}
			//Debug.Log ("*****End of Left in Queue");
//		}
		//Debug.Log ("REMOVED "+which);
	}
	
	public static void printQueue(string msg) {
		Debug.Log ("*****Left in Queue:"+msg);
			for (int j=0; j < GlobalObjs.globalQueue.Count; j++) {
				Debug.Log (GlobalObjs.globalQueue[j].msgNum+" - " +GlobalObjs.globalQueue[j].actorObj.name + " " + GlobalObjs.globalQueue[j].action.ToString()+ ": " + GlobalObjs.globalQueue[j].target.x + ", "+GlobalObjs.globalQueue[j].target.z);
			}
			Debug.Log ("*****End of Left in Queue"+msg);
	}
	
	public static Material getMaterial(string name) {
		foreach(Material myMaterial in  Resources.FindObjectsOfTypeAll(typeof(Material))) {
            //Debug.Log ("Material="+myMaterial.name);
            if (myMaterial.name == name+"armmat") {
				Debug.Log ("Found material="+name);
                return myMaterial;
                //Debug.Log ("Found "+findWhichMaterialmb1);
            }
        }
		Debug.Log ("Did not find material="+name);
		return null;
	}
	
	public static bool isPawn(GameObject o) {
		foreach(GameObject g in GlobalObjs.listOfPawnObj) {
			if (g.name == o.name) {
				return true;
			}
		}
		return false;
	}
	
	public static bool isPawn(string n) {
		foreach(GameObject g in GlobalObjs.listOfPawnObj) {
			if (g.name == n) {
				return true;
			}
		}
		return false;
	}
	
	public static bool isChar(string n) {
		foreach(GameObject g in GlobalObjs.listOfCharObj) {
			if (g.name == n) {
				return true;
			}
		}
		return false;
	}
	
	public static bool isChar(GameObject o) {
		foreach(GameObject g in GlobalObjs.listOfCharObj) {
			if (g.name == o.name) {
				return true;
			}
		}
		return false;
	}
	
	void Update() {
		// skip to next level
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			if (Input.GetKeyDown(KeyCode.S)) {
				Debug.Log ("CJT MESSAGE=DONE!!");
	            InitScript.inputFile.Close ();
				QueueObj temp = new QueueObj(null, null, new Vector3(0,0,0), QueueObj.actiontype.intermission);
						InitScript.inum = temp.msgNum;
						GlobalObjs.globalQueue.Add(temp);
				InitScript.alldone = true;
//	            started = false;
	            InitScript.inputFile = null;
			}
		}
		
		// exit application
		if (Input.GetKey("escape"))
            Application.Quit();
		
		// in case character got stuck, use Shift-Q to invoke the stop all function for all characters
		if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift)) {
			if (Input.GetKeyDown(KeyCode.Q)) {
					foreach(CharFuncs c in GlobalObjs.listOfChars) {
						c.doStopAll ();
					}
					Debug.Log ("Stopped everything");
			}
		}
		
		// find any mouse clicks
		if (Input.GetMouseButtonDown(0) && InitScript.c == 0 && InitScript.started){ // if left button pressed & no menu showing...
			// only act upon clicks inside window (not legend or script areas)
			if (Input.mousePosition.x > 1250 || Input.mousePosition.y < Screen.height - 640) { // ignore
			} else {
		     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		     RaycastHit hit;
		     if (Physics.Raycast(ray, out hit)){
			       // the object identified by hit.transform was clicked
			       // do whatever you want
					Debug.Log ("Clicked on "+hit.transform.gameObject.name+" type="+hit.transform.gameObject.tag);
					InitScript.a = Input.mousePosition.x;
					InitScript.b = Input.mousePosition.y;
					InitScript.what = hit.transform.gameObject;
					InitScript.atwhere = hit.point;
					switch (hit.transform.gameObject.tag) {
						case "Pawn":
							// show menu with go to, point to, look at, pick up OR put down if human holding it
							// make sure if someone else is holding this pawn we add a put down for that character
							InitScript.c = 4;
							if (GlobalObjs.human.transform.GetChildCount() > 0) {
								if (GlobalObjs.human.transform.GetChildCount() > 1) {
									if (GlobalObjs.human.transform.GetChild (0).gameObject.name == hit.transform.gameObject.name || GlobalObjs.human.transform.GetChild (1).gameObject.name == hit.transform.gameObject.name) {
										InitScript.c = 1;
									}
								} else {
									if (GlobalObjs.human.transform.GetChild (0).gameObject.name == hit.transform.gameObject.name) {
										InitScript.c = 1;
									}
								}
							}
							
							break;
							
						case "Person":
							// show menu with go to, point to, look at
							//CharFuncs personfunc = (CharFuncs) hit.transform.GetComponent (typeof(CharFuncs));
							// if self, don't show any menu unless holding something show put down
							InitScript.c = 3;//or 1
							if (hit.transform.gameObject.name == GlobalObjs.human.name) { // if click on self, show 0 or 1 if holding
								if (GlobalObjs.human.transform.GetChildCount() > 0) {
									if (GlobalObjs.human.transform.GetChildCount() > 1) {
											// has to be pointing and holding something
											InitScript.c = 1;
											if (GlobalObjs.human.transform.GetChild (0).gameObject.name != "ArmPrefab") {
												InitScript.what = GlobalObjs.human.transform.GetChild(0).gameObject;
											} else {
												InitScript.what = GlobalObjs.human.transform.GetChild(1).gameObject;
											}
										
									} else {
										if (GlobalObjs.human.transform.GetChild (0).gameObject.name != "ArmPrefab") {
											InitScript.c = 1;
											InitScript.what = GlobalObjs.human.transform.GetChild (0).gameObject;
										}
									}
								} else {
									InitScript.c = 0;
								}
							}
							break;
							
						case "Mark":
							// show menu with go to, point to, look at
							InitScript.c = 3;
							break;
							
						case "Floor":
							// show menu with go to, point to, look at -- hide go to if beyond limits of scene?
							InitScript.c = 3; // or 2
							if (hit.point.x > 45 || hit.point.x < -45) {
								InitScript.c = 2;
							} else {
								if (hit.point.z > 60 || hit.point.z < 0) {
									InitScript.c = 2;
								}
							}
							break;
							
						default:
							// do nothing
							InitScript.c = 0;
							break;
						
					}
					
					
		     }
		}
		
	}
	}
	
	void OnLevelWasLoaded(int level) {
		if (isfirst) {
			isfirst = false;
//			#if UNITY_EDITOR
            Material m = Resources.Load("Materials/BLUEarmmat") as Material;
            m = Resources.Load("Materials/PURPLEarmmat") as Material;
            m = Resources.Load("Materials/REDarmmat") as Material;
            m = Resources.Load("Materials/GREENarmmat") as Material;
			m = Resources.Load("Materials/BROWNarmmat") as Material;
			m = Resources.Load("Materials/CYANarmmat") as Material;
			m = Resources.Load("Materials/ORANGEarmmat") as Material;
			m = Resources.Load("Materials/PEACHarmmat") as Material;
			m = Resources.Load("Materials/PINKarmmat") as Material;
			m = Resources.Load("Materials/YELLOWarmmat") as Material;
			//#endif
		}
		
	}
	
}

