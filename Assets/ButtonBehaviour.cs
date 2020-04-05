using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour {

	public enum ButtonType { GateOpener_OnPress, GateOpener_WhilePressed, PlatformMover_OnPress, PlatformMover_WhilePressed, Activator,}
	public ButtonType bType;

	public Sprite offSprite;
	public Sprite onSprite;

	//Gate Opener
	public GameObject gateToOpen;

	//Platform Mover
	public GameObject platformToMove;

	//Activator
	public GameObject thingToActivate;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.CompareTag("Player") || other.CompareTag("Box"))
		{
			switch (bType)
			{
				case ButtonType.GateOpener_OnPress:
					Destroy(gateToOpen);
					GetComponent<ButtonBehaviour>().enabled = false;
					break;
				case ButtonType.GateOpener_WhilePressed:
					gateToOpen.SetActive(false);
					break;
				case ButtonType.PlatformMover_OnPress:
					platformToMove.GetComponent<MovingPlatform>().enabled = true;
					GetComponent<ButtonBehaviour>().enabled = false;
					break;
				case ButtonType.PlatformMover_WhilePressed:
					platformToMove.GetComponent<MovingPlatform>().enabled = true;
					//Activar movimiento de la plataforma.
					break;
				case ButtonType.Activator:
					thingToActivate.SetActive(true);
					break;
			}

			GetComponent<SpriteRenderer>().sprite = onSprite;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player") || other.CompareTag("Box"))
		{
			switch (bType)
			{
				case ButtonType.GateOpener_WhilePressed:
					gateToOpen.SetActive(true);
					GetComponent<SpriteRenderer>().sprite = offSprite;
					break;
				case ButtonType.PlatformMover_WhilePressed:
					platformToMove.GetComponent<MovingPlatform>().enabled = false;
					GetComponent<SpriteRenderer>().sprite = offSprite;
					break;
			}
		}
	}
}
