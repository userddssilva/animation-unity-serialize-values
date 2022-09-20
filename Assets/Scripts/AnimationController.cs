using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

class MyVector
{
    public float X;
    public float Y;
    public float Z;
}

public class AnimationController : MonoBehaviour
{
    private Animation _animation;
    private AnimationClip _animationClip;
    private EditorCurveBinding[] _editorCurveBindings;
    private Dictionary<float, MyVector> _timeValues;

    [ContextMenu("Get Key Frame Method")]
    public void GetKeyFrames()
    {
        _animation = GetComponent<Animation>();
        _animationClip = _animation.clip;
        _editorCurveBindings = AnimationUtility.GetCurveBindings(_animationClip);
        _timeValues = new Dictionary<float, MyVector>();

        foreach (var binding in _editorCurveBindings)
        {
            var animationCurve = AnimationUtility.GetEditorCurve(_animationClip, binding);
            
            foreach (var keyFrame in animationCurve.keys)
            {
                if (!_timeValues.ContainsKey(keyFrame.time))
                {
                    _timeValues.Add(keyFrame.time, new MyVector());
                }

                if (binding.propertyName.Contains("LocalPosition.x"))
                {
                    _timeValues[keyFrame.time].X = keyFrame.value;
                }

                if (binding.propertyName.Contains("LocalPosition.y"))
                {
                    _timeValues[keyFrame.time].Y = keyFrame.value;
                }

                if (binding.propertyName.Contains("LocalPosition.z"))
                {
                    _timeValues[keyFrame.time].Z = keyFrame.value;
                }
            }
        }

        ShowDictionaryTimeValue();
    }

    private void ShowDictionaryTimeValue()
    {
        foreach (var time in _timeValues.Keys)
        {
            Debug.Log($"Time: {time} - X: {_timeValues[time].X}, Y: {_timeValues[time].Y} Z: {_timeValues[time].Z}");
        }
    }
}