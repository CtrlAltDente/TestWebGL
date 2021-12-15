using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _mainCanvases = new List<GameObject>();
    [SerializeField]
    private List<GameObject> _canvasObject = new List<GameObject>();

    public void EnableNextCanvas(int i)
	{

        DisableCanvas(i-1);
        EnableCanvas(i);
	}

    public void EnablePreviousCanvas(int i)
	{
        DisableCanvas(i+1);
        EnableCanvas(i);
    }

    private void EnableCanvas(int i)
	{
        _canvasObject[i].SetActive(true);
        _mainCanvases[i].SetActive(true);
    }

    private void DisableCanvas(int i)
	{
        _canvasObject[i].SetActive(false);
        _mainCanvases[i].SetActive(false);
    }
}
