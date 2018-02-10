using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

public class ChangeMaterials : MonoBehaviour {

    public string whichChar;
    public bool doInit = false;
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

	}
				
	/*public string getmymessage(GameObject who) {
		string temp = "This is my message for " + who.name;
		return temp;
	}*/
}
