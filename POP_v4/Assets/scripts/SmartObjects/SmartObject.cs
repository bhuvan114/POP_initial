using UnityEngine;
using System.Collections;

public class SmartObject : MonoBehaviour {

	public POPL.Utils.Constants.ActorType type;
	string objectName;

	void Awake () {

		objectName = this.name;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
