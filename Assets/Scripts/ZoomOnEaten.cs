using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ZoomOnEaten : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Canvas _canvas;

    [SerializeField]
    private Volume _volume;

    //[Range(0f, 1f)]
    [SerializeField]
    private ClampedFloatParameter _intensity, _distortion;

    private Vignette _vignette;
    private LensDistortion _lens;

    private ClampedFloatParameter _defaultIntensity, _defaultDistortion;


    public void Start()
    {
        _volume.profile.TryGet(out _vignette);
        _defaultIntensity = _vignette.intensity;

        _volume.profile.TryGet(out _lens);
        _defaultDistortion = _lens.intensity;
    }
    public void CenterOn(Vector2 target, float duration)
    {
        _animator.SetTrigger("Zoom");
        this.transform.position = new Vector3(target.x, target.y, -10);
        _canvas.enabled = false;
        _vignette.intensity = _intensity;
        _lens.intensity = _distortion;
        Invoke("Reset", duration);
    }
    
    public void Reset()
    {
        this.transform.position = new Vector3(0, 0, -10);
        _canvas.enabled = true;
         _vignette.intensity = _defaultIntensity;
        _lens.intensity = _defaultDistortion;
        _animator.SetTrigger("Reset");
    }
}
