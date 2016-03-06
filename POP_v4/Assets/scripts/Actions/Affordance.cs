using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using POPL.Utils;

namespace POPL.Planner
{
	public class Affordance 
	{
		protected bool start = false;
		protected bool goal = false;
		protected string actor = null;
		protected string actor2 = null;
		protected string actionName = "";
		protected List<Condition> preconditions = new List<Condition>();
		protected List<Condition> effects = new List<Condition>();

		public Affordance() { }

		public List<Condition> getPreconditions() {

			return preconditions;
		}

		public List<Condition> getEffects() {
			
			return effects;
		}

		public void addPrecondition(Condition cond) {

			preconditions.Add (cond);
		}

		public void addEffects(Condition cond) {

			effects.Add (cond);
		}

		public void setStart() {

			if (actionName.Equals(""))
				actionName = "START";
			start = true;
		}

		public void setGoal() {

			if (actionName.Equals(""))
				actionName = "GOAL";
			goal = true;
		}

		public bool isStart() {

			return start;
		}

		public bool isGoal() {

			return goal;
		}

		public void setName(string name) {

			actionName = name;
		}

		public string getName() {

			return actionName;
		}

		public string getActionInstance() {
			
			return actor + actionName + actor2;
		}

		public bool Equals(Affordance act) {

			return getActionInstance().Equals (act.getActionInstance());
		}

		public virtual void disp() { 
		
			Debug.Log (actor + actionName + actor2);
		}

	}
}