using UnityEngine;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using POPL.Planner;

namespace POPL.Utils
{

	public class Constants : MonoBehaviour
	{
		public enum ActionType
		{
			EnterScene,
			Greet,
			Argue,
			Apologize
		};

		public enum ActorType
		{
			Player
		};

		//public NameValueCollection actorActionLookUp;
		public static Dictionary<Affordance, Dictionary<Condition, List<System.Type>>> actionRelations = new Dictionary<Affordance, Dictionary<Condition, List<System.Type>>>();
		public static List<Affordance> actions = new List<Affordance> ();

		public void Awake () {

			Utils.populateActionRelations ();
		}

		public void start ()
		{

			/*actorActionLookUp = new NameValueCollection();

			actorActionLookUp.Add (ActorType.Player.ToString(), ActionType.EnterScene.ToString());
			actorActionLookUp.Add (ActorType.Player.ToString(), ActionType.Greet.ToString());
			actorActionLookUp.Add (ActorType.Player.ToString(), ActionType.Argue.ToString());
			actorActionLookUp.Add (ActorType.Player.ToString(), ActionType.Apologize.ToString());*/
		}

	}
}