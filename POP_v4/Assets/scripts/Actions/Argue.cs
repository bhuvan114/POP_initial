using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using POPL.Planner;
using POPL.Utils;

public class Argue : Affordance {
		
	public Argue(string actorOneName, string actorTwoName) {
		
		this.initialize (actorOneName, actorTwoName);
	}

	public Argue() {
		
		this.initialize("actor1", "actor2");
	}

	void initialize(string actorOneName, string actorTwoName) {
		
		actor = actorOneName;
		actor2 = actorTwoName;
		actionName = this.GetType ().ToString();
		
		preconditions.Add(new Condition(actor, "InScene", true));
		preconditions.Add(new Condition(actor2, "InScene", true));
		preconditions.Add(new Condition(actor2, actor, "Knows", true));
		preconditions.Add(new Condition(actor, actor2, "Knows", true));
		
		effects.Add(new Condition(actor, "FeelsBad", true));
		effects.Add(new Condition(actor2, actor, "IsAngryAt", true));
	}

	public override void disp() {
		
		Debug.Log (actor + " argues with " + actor2);
	}
}
