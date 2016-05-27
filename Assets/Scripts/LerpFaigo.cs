using UnityEngine;
using System.Collections;

public enum LerpType
{
	Sin,
	Cos,
	Exp,
	Smooth,
	SmoothMod
};

public class LerpFaigo : MonoBehaviour {
	public Transform waypoint1;
	public Transform waypoint2;
	public float lerpTime;
	public LerpType myLerp;

	private float currentLerp;
	private Vector3 startPosition;
	private Vector3 actualDestination;
	// Use this for initialization
	void Start () {
		transform.position = waypoint1.position;
		actualDestination = waypoint2.position;
		currentLerp = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position == actualDestination) {
			actualDestination = (actualDestination == waypoint1.position) ? waypoint2.position : waypoint1.position;
			startPosition = transform.position;
			currentLerp = 0;
		}
		if (currentLerp < lerpTime) {
			currentLerp += Time.deltaTime;
		} else {
			currentLerp = lerpTime;
		}

		float perc = currentLerp / lerpTime;
		switch (myLerp) {
		case LerpType.Sin:
			perc = Mathf.Sin(perc * Mathf.PI * 0.5f);
			break;
		case LerpType.Cos:
			perc = 1f - Mathf.Cos(perc * Mathf.PI * 0.5f);
			break;
		case LerpType.Exp:
			perc = perc*perc;
			break;
		case LerpType.Smooth:
			perc = perc*perc * (3f - 2f*perc);
			break;
		case LerpType.SmoothMod:
			perc = perc*perc*perc * (perc * (6f*perc - 15f) + 10f);
			break;
		}
		perc = 1f - Mathf.Cos(perc * Mathf.PI * 0.5f);
		transform.position = Vector3.Lerp(startPosition, actualDestination, perc);
	}
}
