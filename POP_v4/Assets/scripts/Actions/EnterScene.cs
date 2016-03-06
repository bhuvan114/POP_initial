using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using POPL.Planner;
using POPL.Utils;

public class EnterScene : Affordance {

	
	public EnterScene(string actorName) {
		
		this.initialize (actorName);
	}

	public EnterScene() {

		this.initialize("actor");
	}

	void initialize(string actorName) {
		
		actor = actorName;
		actionName = this.GetType ().ToString();
		
		preconditions.Add(new Condition(actor, "InScene", false));
		effects.Add(new Condition(actor, "InScene", true));
	}

	public override void disp() {
		
		Debug.Log (actor + " enters scene");
	}
}
