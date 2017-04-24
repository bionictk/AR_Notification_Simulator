using UnityEngine;
using System.Collections;

public class ConditionManager : MonoBehaviour {
    public enum _event { idle, urgent, normal, low };
    public enum _location { classroom, car, home };
    public enum _timeofday { morning, lunch, afternoon, evening };
    public enum _useractivity { sitting, standing, walking, running };
    public enum _cognitiveload { low, normal, high };
    public enum _emotion { neutral, angry, happy };


    public _event event_state;
    public _location location_state;
    public _timeofday timeofday_state;
    public _useractivity useractivity_state;
    public _cognitiveload cognitiveload_state;
    public _emotion emotion_state;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
