using UnityEngine;
using System.Collections;

public class QueueObj : MonoBehaviour {
	
	public GameObject actorObj;
	public CharFuncs actorFunc;
	public GameObject targetObj;
	public Vector3 target;
	public actiontype action;
	public enum actiontype {
		move,
		rotate,
		speak,
		pickup,
		putdown,
		point,
		intermission,
		other
	};
	public static int curMsg = 0;
	public int msgNum;
	
	public QueueObj(GameObject a, GameObject t, Vector3 tp, actiontype at) {
		actorObj = a;
		if (at != actiontype.intermission) {Debug.Log ("actor obj="+a.name);}
		targetObj = t;
		target = tp;
		action = at;
		if (at != actiontype.intermission) {
			actorFunc = GlobalObjs.getCharFunc(actorObj);
			//Debug.Log ("actor func="+actorFunc.thisChar.name);
		}
		msgNum = curMsg;
		curMsg++;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
}
