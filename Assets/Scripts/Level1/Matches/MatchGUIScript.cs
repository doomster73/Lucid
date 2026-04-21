using UnityEngine;
using System.Collections;

public class MatchGUIScript : MonoBehaviour {

	void Awake()
	{
		//marking this object to not be removed between scenes, this way it can store permanent information
		GameObject.DontDestroyOnLoad(gameObject);
	}
}
