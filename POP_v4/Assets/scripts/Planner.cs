using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using POPL.Utils;

namespace POPL.Planner
{
	public class Planner{

		List<Tuple<Affordance, Affordance>> constraints = new List<Tuple<Affordance, Affordance>>();
		Dictionary<Affordance, List<Affordance>> orderingConsts = new Dictionary<Affordance, List<Affordance>>();
		List<Affordance> actions = new List<Affordance>();
		List<CausalLink> causalLinks = new List<CausalLink>();
		Stack<Tuple<Condition, Affordance>> agenda = new Stack<Tuple<Condition, Affordance>>();
		List<Affordance> allPossibleActions = new List<Affordance>();

		public Planner() { }

		void createAllPossibleActions () {

			allPossibleActions.Add (new EnterScene("Bear1"));
			allPossibleActions.Add (new EnterScene("Bear2"));
			allPossibleActions.Add (new Greet("Bear1", "Bear2"));
			allPossibleActions.Add (new Greet("Bear2", "Bear1"));
			allPossibleActions.Add (new Argue("Bear1", "Bear2"));
			allPossibleActions.Add (new Argue("Bear2", "Bear1"));
			allPossibleActions.Add (new Apologize("Bear1", "Bear2"));
			allPossibleActions.Add (new Apologize("Bear2", "Bear1"));

		}

		void instantiatePlan(Affordance start, Affordance goal) {

			constraints.Add (Tuple.New(start, goal));
			addToOrderingConstraints (start, goal);
			actions.Add (start);
			actions.Add (goal);
			foreach (Condition p in goal.getPreconditions()) {

				agenda.Push(Tuple.New(p, goal));
			}
			createAllPossibleActions ();
		}

		bool preConditionAlreadySatisfied(Tuple<Condition, Affordance> g, out Affordance satAct) {

			satAct = null;

			foreach(Affordance act in actions) {
				List<Condition> actEffects = act.getEffects();
				foreach(Condition effect in actEffects) {
					if(effect.Equals(g.First)) {
						satAct = act;
						return true;
					}
				}
			}
			return false;
		}

		bool checkActionsForPrecondition(Tuple<Condition, Affordance> g, out Affordance satAct) {
			
			satAct = null;

			//g.First.disp ();

			foreach(Affordance act in allPossibleActions) {
				List<Condition> actEffects = act.getEffects();
				foreach(Condition effect in actEffects) {
					//Debug.Log ("allPossibleActions - " + effect.condition);
					//effect.disp();
					if(effect.Equals(g.First)) {
						//Debug.LogError ("true that -- " + effect.condition);
						//effect.disp();
						satAct = act;
						allPossibleActions.Remove(act);
						return true;
					}
				}
			}
			return false;
		}

		bool resolveConflicts(CausalLink cl, Affordance act) {

			foreach (Condition effect in act.getEffects()) {
				if(!(cl.act1.getActionInstance().Equals(act.getActionInstance())) 
				   && !(cl.act2.getActionInstance().Equals(act.getActionInstance())) 
				   && (effect.negation(cl.p))) {

					Debug.Log("resolveConflicts : ");
					act.disp();
					cl.disp();
					if(cl.act2.isGoal()) {
						constraints.Add(Tuple.New(act, cl.act1));
						addToOrderingConstraints(act, cl.act1);
					} else if(!cl.act1.isStart()) {
						constraints.Add(Tuple.New(cl.act2, act));
						addToOrderingConstraints(cl.act2, act);
					}
				}
			}
			return true;
		}

		public bool computePlan(Affordance start, Affordance goal) {

			instantiatePlan (start, goal);
			do {
				Tuple<Condition, Affordance> subG = agenda.Pop();
				Affordance act;
				if(!preConditionAlreadySatisfied(subG, out act)) {

					if (checkActionsForPrecondition(subG, out act)) {

						actions.Add(act);
						constraints.Add(Tuple.New(start, act));
						addToOrderingConstraints(start, act);
						foreach(CausalLink cl in causalLinks) {

							resolveConflicts(cl, act);
						}

						foreach(Condition p in act.getPreconditions()) {

							agenda.Push(Tuple.New(p, act));
						}

					} else {
						Debug.LogError("Plan Failed for condition - ");
						subG.First.disp();
						showActions ();
						return false;
					}
				}

				constraints.Add(Tuple.New(act, subG.Second));
				addToOrderingConstraints(act, subG.Second);
				CausalLink cLink = new CausalLink(act, subG.First, subG.Second);
				causalLinks.Add(cLink);

				foreach(Affordance a in actions) {

					if (!a.isStart())
						resolveConflicts(cLink, a);
				}

				//Debug.Log("In While loop!!");
			} while (agenda.Count() != 0);

			constraints = Utils.Utils.removeDuplicateConstraints (constraints);
			Debug.LogError("Goal Reached!!");
			return true;
		}

		public void addToOrderingConstraints(Affordance key, Affordance value) {

			if (orderingConsts.ContainsKey(key)){
				if(!orderingConsts[key].Contains(value))
					orderingConsts[key].Add(value);
			} else {
				List<Affordance> listActions = new List<Affordance> ();
				listActions.Add (value);
				orderingConsts.Add (key, listActions);
			}
		}

		public void showActions() {

			foreach (Affordance act in actions)
				act.disp ();
		}

		public void showConstraints() {

			foreach (Tuple<Affordance, Affordance> constraint in constraints) {
				Debug.Log("Constraint : ");
				constraint.First.disp();
				constraint.Second.disp();
			}
		}

		public void showOrderingConstraints() {

			foreach (Affordance key in orderingConsts.Keys) {
				Debug.LogWarning("Constraint : ");
				key.disp();
				List<Affordance> values = orderingConsts[key];
				Debug.Log("Happens before : ");
				foreach(Affordance act in values)
					act.disp();
			}
		}
	}
}
