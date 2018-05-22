using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModal : MonoBehaviour
{

	public GameObject modalToOpen;
	public GameObject modalToClose;

	public void OpenModal()
	{
		modalToClose.SetActive(false);
		modalToOpen.SetActive(true);
	}
}
