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
		private float _rotationSpeed;
		[SerializeField]
		private float _closingLayersSpeed;

		[SerializeField]
		private Transform _directionPoint;

		[SerializeField]
		private List<Transform> _headLayers = new List<Transform>();
		[SerializeField]
		private Slider _layersSlider;

		[SerializeField]
		private float _startLayerPositionX;
		[SerializeField]
		private float _spaceBetweenLayers;


		private void Awake()
		{
			_sizeController = GetComponent<SizeController>();
			_rotateController = GetComponent<RotateController>();
			_boxCollider = GetComponent<BoxCollider>();
		}

		public void EnableOrDisableBoxCollider()
		{
			if (_firstCanvas.activeInHierarchy)
			{
				_boxCollider.enabled = true;
			}
			else
			{
				_boxCollider.enabled = false;
			}
		}

		public void OpenLayers()
		{
			if (!_firstCanvas.activeInHierarchy)
			{

				for (int i = 0; i < _headLayers.Count; i++)
				{
					_headLayers[i].localPosition = new Vector3(_startLayerPositionX + _layersSlider.value * (float)i * _spaceBetweenLayers, 0f, 0f);
				}
			}
			else
			{
				for (int i = 0; i < _headLayers.Count; i++)
				{
					_headLayers[i].localPosition = new Vector3(_startLayerPositionX, 0f, 0f);
				}
			}
		}

		public void CloseLayers()
		{
			StartCoroutine(ConnectLayers());
		}

		private void OnMouseUpAsButton()
		{
			if (_firstCanvas.activeInHierarchy)
			{
				_canvasManager.EnableNextCanvas(1);
				_layersSlider.value = 0f;
				Debug.Log("Wqwedfaz");
				_firstCanvas.SetActive(false);
				EnableOrDisableBoxCollider();

				StartCoroutine(NormalizeSize());
				StartCoroutine(NormalizeRotation());
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

		private IEnumerator ConnectLayers()
		{
			while (true)
			{
				if (Vector3.Distance(_headLayers[_headLayers.Count - 1].localPosition, new Vector3(_startLayerPositionX, 0f, 0f)) > 0.01f)
				{
					for (int i = 0; i < _headLayers.Count; i++)
					{
						_headLayers[i].localPosition = Vector3.MoveTowards(_headLayers[i].localPosition, new Vector3(_startLayerPositionX, 0f, 0f), _closingLayersSpeed * Time.deltaTime);
					}
				}
				else
				{
					break;
				}
				yield return new WaitForEndOfFrame();
			}
		}
	}
}