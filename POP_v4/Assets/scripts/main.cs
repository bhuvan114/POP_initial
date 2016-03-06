using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using POPL.Planner;
using POPL.Utils;

public class main : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Affordance start = new Affordance ();
		start.setStart ();
		start.addEffects (new Condition ("Bear1", "InScene", false));
		start.addEffects (new Condition ("Bear2", "InScene", false));
		Apologize goal = new Apologize ("Bear1", "Bear2");
		goal.setGoal ();
		Planner popPlanner = new Planner ();
		popPlanner.computePlan (start, goal);

		popPlanner.showActions ();
		//popPlanner.showConstraints ();
		popPlanner.showOrderingConstraints ();

		//Utils.populateActionRelations ();

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
