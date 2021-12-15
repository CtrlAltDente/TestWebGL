using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
	public class HeadController : MonoBehaviour
	{
		[SerializeField]
		private GameObject _firstCanvas;
		[SerializeField]
		private CanvasManager _canvasManager;

		private SizeController _sizeController;
		private RotateController _rotateController;

		private BoxCollider _boxCollider;

		[SerializeField]
		private float _sizingSpeed;

		[SerializeField]
		private Transform _directionPoint;

		[SerializeField]
		private float _rotationSpeed;

		[SerializeField]
		private List<Transform> _headLayers = new List<Transform>();
		[SerializeField]
		private Slider _layersSlider;

		private void Awake()
		{
			_sizeController = GetComponent<SizeController>();
			_rotateController = GetComponent<RotateController>();
			_boxCollider = GetComponent<BoxCollider>();
		}

		private void Update()
		{
			CheckCanvasAndLayers();
		}

		private void OnMouseUpAsButton()
		{
			if (_firstCanvas.active)
			{
				_canvasManager.EnableNextCanvas(1);
				Debug.Log("Wqwedfaz");
				_firstCanvas.SetActive(false);
				_boxCollider.enabled = false;

				StartCoroutine(NormalizeSize());
				StartCoroutine(NormalizeRotation());
			}
		}

		private void CheckCanvasAndLayers()
		{
			if(_firstCanvas.active)
			{
				_boxCollider.enabled = true;
				ControlLayers();
			}
			else
			{
				_boxCollider.enabled = false;
			}
		}

		public void ControlLayers()
		{
			if(!_firstCanvas.active)
			{

				for(int i = 0;i<_headLayers.Count;i++)
				{
					_headLayers[i].localPosition = new Vector3(0.25f + _layersSlider.value * (float)i*0.5f,0f,0f);
				}
			}
			else
			{
				for (int i = 0; i < _headLayers.Count; i++)
				{
					_headLayers[i].localPosition = new Vector3(0.25f,0f,0f);
				}
			}
		}

		private IEnumerator NormalizeSize()
		{
			while (transform.localScale.x > 1f)
			{
				transform.localScale -= new Vector3(_sizingSpeed, _sizingSpeed, _sizingSpeed) * Time.deltaTime;
				yield return new WaitForEndOfFrame();
			}
			_sizeController.SetStartSize(transform.localScale);
		}

		private IEnumerator NormalizeRotation()
		{
			while (transform.rotation != Quaternion.Euler(0f, 0f, 0f))
			{
				transform.rotation = Quaternion.RotateTowards(transform.rotation, _directionPoint.rotation, _rotationSpeed * Time.deltaTime);
				yield return new WaitForEndOfFrame();
			}
			_rotateController.SetStartRotation(Vector3.zero);
		}
	}
}