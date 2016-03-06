using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using POPL.Planner;
using POPL.Utils;

public class Greet : Affordance {

	public Greet(string actorOneName, string actorTwoName) {

		this.initialize (actorOneName, actorTwoName);
	}

	public Greet() {
		
		this.initialize("actor1", "actor2");
	}

	void initialize(string actorOneName, string actorTwoName) {
		
		actor = actorOneName;
		actor2 = actorTwoName;
		actionName = this.GetType ().ToString();
		
		preconditions.Add(new Condition(actor, "InScene", true));
		preconditions.Add(new Condition(actor2, "InScene", true));
		
		effects.Add(new Condition(actor, actor2, "Knows", true));
		effects.Add(new Condition(actor2, actor, "Knows", true));
	}

	public override void disp() {
		
		Debug.Log (actor + " greets " + actor2);
	}
}
