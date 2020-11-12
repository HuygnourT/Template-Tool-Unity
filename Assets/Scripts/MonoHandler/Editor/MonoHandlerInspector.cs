using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MonoHandler),true)]
public class MonoHandlerInspector : Editor {

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI ();
		MonoHandler mono = (MonoHandler)target;
		mono.GUIEditor ();
	 	
	}

	 
}
