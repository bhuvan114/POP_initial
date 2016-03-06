using UnityEngine;
using System.Collections;

using POPL.Planner;

namespace POPL.Utils
{
	public class Condition {

		public string actor1;
		public string actor2;
		public string condition;
		public bool status;

		public Condition(string actorName, string conditionName, bool conditionStatus) {

			condition = conditionName;
			actor1 = actorName;
			actor2 = "";
			status = conditionStatus;
		}

		public Condition(string actorOneName, string actorTwoName, string conditionName, bool conditionStatus) {
			
			condition = conditionName;
			actor1 = actorOneName;
			actor2 = actorTwoName;
			status = conditionStatus;
		}

		public bool Equals(Condition c) {

			return ((actor1 == c.actor1) && (actor2 == c.actor2) && (condition == c.condition) && (status == c.status));
		}

		public bool negation(Condition c){

			return ((actor1 == c.actor1) && (actor2 == c.actor2) && (condition == c.condition) && (status != c.status));
		}

		public void disp() {
			Debug.Log ("condition - " + condition + ", " + actor1 + ", " + actor2 + ", " + status.ToString());
		}
	}

	public class CausalLink {

		public Affordance act1;
		public Affordance act2;
		public Condition p;

		public CausalLink(Affordance actor1, Condition preCond, Affordance actor2) {

			act1 = actor1;
			act2 = actor2;
			p = preCond;
		}

		public void disp() {

			Debug.Log (act1.getActionInstance() + p.condition + act2.getActionInstance());
		}
	}

	public class Tuple<T1, T2>
	{
		public T1 First { get; private set; }
		public T2 Second { get; private set; }
		internal Tuple(T1 first, T2 second)
		{
			First = first;
			Second = second;
		}

		public bool Equals (Tuple<T1, T2> obj)
		{
			return (First.Equals(obj.First) && Second.Equals(obj.Second));
		}
	}

	public static class Tuple
	{
		public static Tuple<T1, T2> New<T1, T2>(T1 first, T2 second)
		{
			var tuple = new Tuple<T1, T2>(first, second);
			return tuple;
		}
	}
}
