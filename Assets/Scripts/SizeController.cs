using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{
	public class SizeController : MonoBehaviour
	{
		[SerializeField]
		private float _sizingSpeed;
		[SerializeField]
		[Range(1f, 2f)]
		private float _sizingValue;

		private Vector3 _minSizing;
		private Vector3 _maxSizing;

		private Vector3 _size;

		private void Awake()
		{
			GetStartSize();
		}

		public void IncreaseSize()
		{
			if (CurrentSizeLessThanMaximum())
			{
				_size += new Vector3(_sizingSpeed, _sizingSpeed, _sizingSpeed) * Time.deltaTime;
				transform.localScale = _size;
			}
		}

		public void DecreaseSize()
		{
			if (CurrentSizeLargerThanMinimum())
			{
				_size -= new Vector3(_sizingSpeed, _sizingSpeed, _sizingSpeed) * Time.deltaTime;
				transform.localScale = _size;
			}
		}

		public void SetStartSize(Vector3 size)
		{
			_minSizing = size;
			_size = transform.localScale;
			_maxSizing = new Vector3(_minSizing.x * _sizingValue, _minSizing.y * _sizingValue, _minSizing.z * _sizingValue);
		}

		private void GetStartSize()
		{
			_minSizing = transform.localScale;
			_size = transform.localScale;
			_maxSizing = new Vector3(_minSizing.x * _sizingValue, _minSizing.y * _sizingValue, _minSizing.z * _sizingValue);
		}

		private bool CurrentSizeLessThanMaximum()
		{
			if (transform.localScale.x < _maxSizing.x && transform.localScale.y < _maxSizing.y && transform.localScale.z < _maxSizing.z)
			{
				return true;
			}
			return false;
		}

		private bool CurrentSizeLargerThanMinimum()
		{
			if (transform.localScale.x > _minSizing.x && transform.localScale.y > _minSizing.y && transform.localScale.z > _minSizing.z)
			{
				return true;
			}
			return false;
		}
	}
}