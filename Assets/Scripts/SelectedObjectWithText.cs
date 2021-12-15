using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedObjectWithText : MonoBehaviour
{
    [SerializeField]
    private List<Text> _description = new List<Text>();

	[SerializeField]
    private List<GameObject> _object = new List<GameObject>();

	public void SetActiveObject(int i)
	{
		foreach(var text in _description)
		{
			text.fontStyle = FontStyle.Normal;
		}
		_description[i].fontStyle = FontStyle.Bold;
	}

	private void Update()
	{
		CheckClick();
	}

	private void CheckClick()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f))
			{
				if (hit.transform.gameObject != null)
				{
					for (int i = 0; i < _object.Count; i++)
					{
						_description[i].fontStyle = FontStyle.Normal;
						if (hit.transform.gameObject == _object[i])
						{
							_description[i].fontStyle = FontStyle.Bold;
						}
					}
				}
			}
		}
		if (Input.touchCount > 0)
		{
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.GetTouch(0).position), out hit, 100f))
			{
				if (hit.transform.gameObject != null)
				{
					for(int i = 0;i < _object.Count; i++)
					{
						_description[i].fontStyle = FontStyle.Normal;
						if (hit.transform.gameObject == _object[i])
						{
							_description[i].fontStyle = FontStyle.Bold;
						}
					}
				}
			}
		}
	}
}
