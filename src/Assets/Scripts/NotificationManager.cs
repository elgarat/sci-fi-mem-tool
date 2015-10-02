/*
 * 	@author: Podgornov Matvey
 * 	Event manager
 * 	v 0.1
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationManager : MonoBehaviour {
	public Dictionary<string, List<Component>> listeners_dictionary = new Dictionary<string, List<Component>>();

	public void AddListener(Component game_object, string notification_name){
		if (listeners_dictionary.ContainsKey(notification_name))
			listeners_dictionary.Add(notification_name, new List<Component>());

		listeners_dictionary [notification_name].Add (game_object);
	}

	public void RemoveListener(Component game_object, string notification_name){
		if (!listeners_dictionary.ContainsKey (notification_name))
			return;

		for (int i=listeners_dictionary[notification_name].Count-1; i>=0; i--) {
			if (listeners_dictionary[notification_name][i].GetInstanceID() == game_object.GetInstanceID())
				listeners_dictionary[notification_name].RemoveAt(i);
		}

	}
	
	public void PostNotification(Component game_object, string notification_name){
		if (!listeners_dictionary.ContainsKey (notification_name))
			return;

		foreach (Component listener in listeners_dictionary[notification_name]) {
			listener.SendMessage(notification_name, game_object, SendMessageOptions.DontRequireReceiver);
		}
	}

	public void ClearListeners(){
		listeners_dictionary.Clear();
	}

	public void RemoveRedundancies(){
		Dictionary<string, List<Component>> tmp_listeners_dictionary = new Dictionary<string, List<Component>>();

		foreach (KeyValuePair<string, List<Component>> item in listeners_dictionary) {
			for(int i = item.Value.Count-1; i>=0; i--){
				if (item.Value[i] == null)
					item.Value.RemoveAt(i);
			}

			if(item.Value.Count > 0)
				tmp_listeners_dictionary.Add (item.Key, item.Value);
		}

		listeners_dictionary = tmp_listeners_dictionary;
	}

	void OnLevelLoaded(){
		RemoveRedundancies ();
	}
}
