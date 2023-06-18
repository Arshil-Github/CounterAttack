using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScrollRectSnap : MonoBehaviour
{

	float[] points;
	[Tooltip("how many screens or pages are there within the content (steps)")]
	public int screens = 1;
	[Tooltip("How quickly the GUI snaps to each panel")]
	public float snapSpeed;
	public float inertiaCutoffMagnitude;
	float stepSize;

	ScrollRect scroll;
	bool LerpH;
	float targetH;
	[Tooltip("Snap horizontally")]
	public bool snapInH = true;

	bool LerpV;
	float targetV;
	[Tooltip("Snap vertically")]
	public bool snapInV = true;

	public string controllTag;
	bool dragInit = true;
	int dragStartNearest;
	float horizontalNormalizedPosition;
	float verticalNormalizedPosition;

	public static event Action<int, int> OnEndReached;
	public static event Action<int, int, string> OnEndReachedWithTag;

	// Use this for initialization
	void Start()
	{
		Init();
		//		SnapToSelectedIndex(0);
	}

	void Init()
	{
		scroll = gameObject.GetComponent<ScrollRect>();
		scroll.inertia = true;

		if (screens > 0)
		{
			points = new float[screens];
			stepSize = (float)Math.Round(1 / (float)(screens - 1), 2);

			for (int i = 0; i < screens; i++)
			{
				points[i] = i * stepSize;
			}
		}
		else
		{
			points[0] = 0;
		}
	}

	void OnEnable()
	{

	}

	void Update()
	{
		horizontalNormalizedPosition = scroll.horizontalNormalizedPosition;
		verticalNormalizedPosition = scroll.verticalNormalizedPosition;

		if (LerpH)
		{
			scroll.horizontalNormalizedPosition = Mathf.Lerp(scroll.horizontalNormalizedPosition, targetH, snapSpeed * Time.deltaTime);
			if (Mathf.Approximately((float)Math.Round(scroll.horizontalNormalizedPosition, 2), targetH))
			{
				LerpH = false;
				int target = FindNearest(scroll.horizontalNormalizedPosition, points);
				//				Debug.LogError("Target : " + target);
				if (target == points.Length - 1)
				{
					if (OnEndReached != null)
					{
						OnEndReached(1, target);
					}
					if (OnEndReachedWithTag != null)
					{
						OnEndReachedWithTag(1, target, controllTag);
					}
				}
				else if (target == 0)
				{
					if (OnEndReached != null)
					{
						OnEndReached(-1, target);
					}
					if (OnEndReachedWithTag != null)
					{
						OnEndReachedWithTag(-1, target, controllTag);
					}
				}
				else
				{
					if (OnEndReached != null)
					{
						OnEndReached(0, target);
					}
					if (OnEndReachedWithTag != null)
					{
						OnEndReachedWithTag(0, target, controllTag);
					}
				}

			}
		}
		if (LerpV)
		{
			scroll.verticalNormalizedPosition = Mathf.Lerp(scroll.verticalNormalizedPosition, targetV, snapSpeed * Time.deltaTime);
			if (Mathf.Approximately(scroll.verticalNormalizedPosition, targetV))
			{
				LerpV = false;
			}
		}
	}

	public void DragEnd()
	{
		int target = FindNearest(scroll.horizontalNormalizedPosition, points);

		if (target == dragStartNearest && scroll.velocity.sqrMagnitude > inertiaCutoffMagnitude * inertiaCutoffMagnitude)
		{
			if (scroll.velocity.x < 0)
			{
				target = dragStartNearest + 1;
			}
			else if (scroll.velocity.x > 1)
			{
				target = dragStartNearest - 1;
			}
			target = Mathf.Clamp(target, 0, points.Length - 1);
		}

		if (scroll.horizontal && snapInH)
		{
			targetH = points[target];
			LerpH = true;
		}
		if (scroll.vertical && snapInV && scroll.verticalNormalizedPosition > 0f && scroll.verticalNormalizedPosition < 1f)
		{
			targetH = points[target];
			LerpH = true;
		}

		dragInit = true;
	}

	public void OnDrag()
	{
		if (dragInit)
		{
			if (scroll == null)
			{
				scroll = gameObject.GetComponent<ScrollRect>();
			}
			dragStartNearest = FindNearest(scroll.horizontalNormalizedPosition, points);
			dragInit = false;
		}

		LerpH = false;
		LerpV = false;
	}

	int FindNearest(float f, float[] array)
	{
		float distance = Mathf.Infinity;
		int output = 0;
		for (int index = 0; index < array.Length; index++)
		{
			if (Mathf.Abs(array[index] - f) < distance)
			{
				distance = Mathf.Abs(array[index] - f);
				output = index;
			}
		}
		return output;
	}

	public void DraggedOnLeft()
	{
		OnDrag();

		if (scroll.horizontal && snapInH && scroll.horizontalNormalizedPosition > -0.001f && scroll.horizontalNormalizedPosition < 1.001f)
		{
			//			Debug.Log("Before Press, LerpH : " + LerpH);
			if (dragStartNearest < points.Length - 1)
			{
				targetH = points[dragStartNearest + 1];
				LerpH = true;
			}
			else
			{
				targetH = points[dragStartNearest];
				LerpH = true;
			}
			//			Debug.Log("After Press, LerpH : " + LerpH);
		}
		if (scroll.vertical && snapInV && scroll.verticalNormalizedPosition > 0f && scroll.verticalNormalizedPosition < 1f)
		{

			if (dragStartNearest < points.Length - 1)
			{
				targetV = points[dragStartNearest + 1];
				LerpV = true;
			}
			else
			{
				targetV = points[dragStartNearest];
				LerpV = true;
			}
		}

		dragInit = true;
	}
	public void DraggedOnRight()
	{
		OnDrag();

		if (scroll.horizontal && snapInH && scroll.horizontalNormalizedPosition > -0.001f && scroll.horizontalNormalizedPosition < 1.001f)
		{
			if (dragStartNearest > 0)
			{
				targetH = points[dragStartNearest - 1];
				LerpH = true;
			}
			else
			{
				targetH = points[dragStartNearest];
				LerpH = true;
			}
		}
		if (scroll.vertical && snapInV && scroll.verticalNormalizedPosition > 0f && scroll.verticalNormalizedPosition < 1f)
		{
			if (dragStartNearest > 0)
			{
				targetV = points[dragStartNearest - 1];
				LerpV = true;
			}
			else
			{
				targetV = points[dragStartNearest];
				LerpV = true;
			}
		}

		dragInit = true;
	}

	public void SnapToSelectedIndex(int index)
	{
		if (points == null)
		{
			Init();
		}
		dragInit = false;
		LerpH = false;
		LerpV = false;
		targetH = points[index];
		LerpH = true;
		dragInit = true;
	}

}