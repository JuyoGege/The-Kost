using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour {

    public GameObject OpenPanel = null;

    private bool _isInsideTrigger = false;

    public Animator _animator;

	public string OpenText = "Press 'E' to open";

	public string CloseText = "Press 'E' to close";

	private bool _isOpen = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _isInsideTrigger = true;
            OpenPanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _isInsideTrigger = false;
            OpenPanel.SetActive(false);
        }
    }

    private bool IsOpenPanelActive
    {
        get
        {
            return OpenPanel.activeInHierarchy;
        }
    }

    // Update is called once per frame
    void Update () {
        if (IsOpenPanelActive && _isInsideTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
				_isOpen = !_isOpen;

				_animator.SetBool("open", _isOpen);
            }
        }
    }
}
