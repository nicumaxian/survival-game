﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseGameObject : MonoBehaviour {
	private IList<RegisteredTimer> _actions = new List<RegisteredTimer> ();

	public void StartTimer(Action action,float interval){
		_actions.Add (new RegisteredTimer (action,interval));
	}

	// Update is called once per frame
	public void Update () {
		for(int i = 0; i < _actions.Count; i++){
			_actions [i].Step (Time.deltaTime);
		}
	}

	private class RegisteredTimer {
		private readonly Action _action;
		private readonly float _interval;

		private float _ellapsed;

		public RegisteredTimer (Action action, float interval)
		{
			this._action = action;
			this._interval = interval;
		}

		public void Step(float ellapsedSinceLastCheck){
			_ellapsed += ellapsedSinceLastCheck;

			if (_ellapsed >= _interval) {
				_ellapsed = 0;
				Debug.Log ("Interval of " + _interval + " seconds reached. Invoking action");
				_action ();
			}
		}
	}
}
