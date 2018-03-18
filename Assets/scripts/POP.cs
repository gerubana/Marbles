using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POP : MonoBehaviour {
    private Animator ani;

    public void popup()
    {
        ani = GetComponent<Animator>();
        StartCoroutine(WaitForAnimationToPlay());
        ani.SetBool("POP", true);

    }

    private IEnumerator WaitForAnimationToPlay()
    {
        ani = GetComponent<Animator>();
        yield return new WaitForSeconds(1);
        ani.SetBool("POP", false);
    }
}
