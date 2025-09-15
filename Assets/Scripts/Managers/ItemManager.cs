using UnityEngine;

public class ItemManager : MonoBehaviour
{

    [SerializeField]
    private Animator _animator;

    private bool _open;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _open = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If it wasnt open and i click on the button, send the open animation
        if (!_open && Input.GetButton("Jump"))
        {
            _animator.SetTrigger("Open");
            _open = true;
        }

        if (_open && !Input.GetButton("Jump"))
        {
            _animator.SetTrigger("Close");
            _open = false;
        }
        //if it was open and i stop clicking, send the close animation
    }
}
