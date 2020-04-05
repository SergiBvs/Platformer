using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBehaviour : MonoBehaviour {

	public enum ButtonType { GateOpener_OnPress, GateOpener_WhilePressed, PlatformMover_OnPress, PlatformMover_WhilePressed}
	public ButtonType bType;

	//Gate Opener
	public GameObject gateToOpen;

	//Platform Mover
	public GameObject platformToMove;

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
					//activar movimiento de la plataforma.
					//Desactivar buttonbehaviour
					break;
				case ButtonType.PlatformMover_WhilePressed:
					//Activar movimiento de la plataforma.
					break;

					
			}
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
					break;
				case ButtonType.PlatformMover_WhilePressed:
					//Desactivar movimiento de la plataforma.
					break;
			}
		}
	}
}
