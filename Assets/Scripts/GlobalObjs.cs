using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GlobalObjs : MonoBehaviour
{
	public static GlobalObjs Instance;
	
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
	
	
	
	void Awake() {
		if (Instance != null) {
			Destroy(Instance); // only keep one version of this
		}
			Instance = this;

		
		
	}
	
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
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
		if (InitScript.started && GlobalObjs.globalQueue.Count == 0) {
			// read next set of lines
			//Debug.Log ("Calling next Step, no items in queue");
			// destroy temporary items:
			
			foreach(GameObject g in toDeleteList) {
				Destroy (g);
				//toDeleteList.Remove(g);
			}
			toDeleteList.Clear();
			
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
		
		
	}
	
	public static CharFuncs getCharFunc(string name) {
		name = name.ToUpper();
		foreach(CharFuncs c in listOfChars) {
			if (c.name == name) {
				return c;
			}
		}
		return null;
		
		
	}
	
	public static CharFuncs getCharFunc(GameObject o) {
		foreach(CharFuncs c in listOfChars) {
			if (c.name == o.name) {
				return c;
			}
		}
		//Debug.Log ("size of list of chars="+listOfChars.Count+", looking for "+o.name);
		return null;
		

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
			Debug.Log ("action="+GlobalObjs.globalQueue[removethis].action);
			Debug.Log ("work num="+((GlobalObjs.globalQueue[removethis].actorFunc ==null)?"null":"not null"));
			GlobalObjs.globalQueue[removethis].actorFunc.workingNum = -1;
		}
		Debug.Log ("Removed msg="+which+", item="+removethis);
		
		GlobalObjs.globalQueue.RemoveAt(removethis);
		//printQueue ("After removing one");

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
		//if (isfirst) {
			//isfirst = false;
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
		//}
		
		
		// reset all static variables!!
		 listOfChars = new List<CharFuncs>();
	 listOfCharObj = new List<GameObject>();
	 listOfAllObj = new List<GameObject>();
	 listOfPawnObj = new List<GameObject>();
	//human = null;
	//humanfunc = null;
	
	 priorityList = new List<GameObject>();
	
	globalQueue = new List<QueueObj>();
		isfirst = true;
		
		// scrolling script variables
	showwarning = false;
	targetTopAllText = 0f;
	currentTopAllText = 0f;
	boxTop = 0f;
	boxHeight = 0f;
	
	hasspeech = false;
	ready = false;
	toDeleteList = new List<GameObject>();
		
		InitScript.LoadChars ();
				//RunPlay();
		InitScript currentinit = Camera.main.GetComponent<InitScript>();
				currentinit.starting = true;
				currentinit.timer = 0.0f;
		
	}
	
}

