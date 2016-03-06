using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using POPL.Planner;

namespace POPL.Utils
{
	public static class Utils {

		public static List<Tuple<Affordance, Affordance>> removeDuplicateConstraints(List<Tuple<Affordance, Affordance>> constraints) {

			List<Tuple<Affordance, Affordance>> uniqueConsts = new List<Tuple<Affordance, Affordance>>();
			foreach (Tuple<Affordance, Affordance> constraint in constraints) {
				//for(int i = constraints.Count-1; i>=0; i--) {

//					Debug.Log(i.ToString());
				//	if (constraints[i].First.getActionInstance().Equals(constraint.First.getActionInstance())
				//	    && constraints[i].Second.getActionInstance().Equals(constraint.Second.getActionInstance())) {
				//		constraints.RemoveAt(i);
				//	}
				//}
				bool contains = false;
				foreach (Tuple<Affordance, Affordance> uniqConst in uniqueConsts) {
					if (constraint.Equals(uniqConst)) {
						contains = true;
					}
				}

				if (!contains)
					uniqueConsts.Add(constraint);
			}

			return uniqueConsts;
		}

		public static void populateActionRelations() {

			IEnumerable<System.Type> actionTypes = System.Reflection.Assembly.GetExecutingAssembly ().GetTypes ()
				.Where (t => t.BaseType != null && t.BaseType == typeof(POPL.Planner.Affordance));

			//List<Action> actions = new List<Action> ();

			foreach (System.Type actType in actionTypes) {

				Constants.actions.Add((Affordance)System.Activator.CreateInstance(actType));

				//Debug.LogWarning(typeof(POPL.Planner.Action));

				//Debug.Log(actType.BaseType.ToString());
				//	Debug.Log(actType.ToString());

			}

			foreach (Affordance action in Constants.actions) {

				Constants.actionRelations.Add(action, getRelations(action, Constants.actions));
			}
		}

		static Dictionary<Condition, List<System.Type>> getRelations(Affordance action, List<Affordance> actions) {

			Dictionary<Condition, List<System.Type>> relations = new Dictionary<Condition, List<System.Type>> ();
			foreach (Condition preCond in action.getPreconditions()) {

				foreach(Affordance act in actions) {

					foreach(Condition effect in act.getEffects()) {

						if((effect.condition.Equals(preCond.condition)) && (effect.status == preCond.status)) {

							if (relations.ContainsKey(preCond)){
								if(!relations[preCond].Contains(act.GetType ()))
									relations[preCond].Add(act.GetType ());
							} else {
								List<System.Type> listActions = new List<System.Type> ();
								listActions.Add (act.GetType ());
								relations.Add (preCond, listActions);
							}

							//addToDictionary(preCond, act.GetType (), out relations);
						}
					}
				}
			}

			return relations;
		}

		/*static void addToDictionary(Condition key, System.Type value, out Dictionary<Condition, List<System.Type>> relations) {
			
			if (relations.ContainsKey(key)){
				if(!relations[key].Contains(value))
					relations[key].Add(value);
			} else {
				List<System.Type> listActions = new List<System.Type> ();
				listActions.Add (value);
				relations.Add (key, listActions);
			}
		}*/
	}
}