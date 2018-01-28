using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class ChangeMaterials : MonoBehaviour {

    public string whichChar;
    public static bool doInit = false;
	int runtest = 0;

	// Use this for initialization
	void Start () {
        if (!doInit) {
            doInit = true;
//            #if UNITY_EDITOR
            Material m = Resources.Load ("Materials/BLUEMat") as Material;
            m = Resources.Load("Materials/PURPLEMat") as Material;
            m = Resources.Load("Materials/REDMat") as Material;
            m = Resources.Load("Materials/GREENMat") as Material;
			m = Resources.Load("Materials/BROWNMat") as Material;
			m = Resources.Load("Materials/CYANMat") as Material;
			m = Resources.Load("Materials/ORANGEMat") as Material;
			m = Resources.Load("Materials/PEACHMat") as Material;
			m = Resources.Load("Materials/PINKMat") as Material;
			m = Resources.Load("Materials/PURPLEMat") as Material;
			m = Resources.Load("Materials/YELLOWMat") as Material;
			//#endif
        }
        //Debug.Log("Starting Coloring for "+whichChar+"!!!!");
        if (whichChar == "") {
            whichChar = this.name;
            //Debug.Log("Changed name to:"+this.name);
        }
        string findWhichMaterial = "BLUEMat";

		GameObject mbody = this.gameObject;

        if (!mbody) Debug.Log ("Couldn't find body for "+whichChar);

        // calculate the material name to be searched for
        /*switch (whichChar) {
            case "GraveDigger":
            findWhichMaterial = "PURPLEMat";
            //Debug.Log ("Changed GraveDigger coloring");
            break;
            case "GraveDiggerTwo":
            findWhichMaterial = "REDMat";
            //Debug.Log ("Changed GraveDiggerTwo coloring");
            break;
            case "Hamlet":
            findWhichMaterial = "BLUEMat";
            //Debug.Log ("Changed Hamlet coloring");
            break;
            case "Horatio":
            findWhichMaterial = "GREENMat";
            //Debug.Log ("Changed Horatio coloring");
            break;
            default:
            // do nothing
            Debug.Log ("No match!");
            break;

        }*/
		
		// use bodycolor on this object to set findWhichMaterial
		
		CharFuncs cf = (CharFuncs) mbody.GetComponent (typeof(CharFuncs));
		findWhichMaterial = cf.bodycolor;

        foreach(Material myMaterial in  Resources.FindObjectsOfTypeAll(typeof(Material))) {
            //Debug.Log ("Material="+myMaterial.name);
            if (myMaterial.name == findWhichMaterial) {
                mbody.renderer.material = myMaterial;
                //Debug.Log ("Found "+findWhichMaterialmb1);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
//		if (runtest < 2 && whichChar == "Hamlet") {
//			Debug.Log ("Hamlet's name is="+GlobalObjs.Hamlet.name);
//			//CharFuncs cf = (CharFuncs) GlobalObjs.Hamlet.GetComponent (typeof(CharFuncs));
//		//	GlobalObjs.HamletFunc.doRotate(45);
//			//GlobalObjs.doRotate(GlobalObjs.Hamlet, 45);
//			ChangeMaterials c = (ChangeMaterials) GlobalObjs.Hamlet.GetComponent (typeof(ChangeMaterials));
//			//Debug.Log (c.getmymessage(GlobalObjs.Hamlet));
//			runtest++;
//		}
	}
				
	/*public string getmymessage(GameObject who) {
		string temp = "This is my message for " + who.name;
		return temp;
	}*/
}
