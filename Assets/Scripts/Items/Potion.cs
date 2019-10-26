using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    public AnimationCurve path;

    float distance = 2;
    float timeToTarget = .75f;

    //public void StartO
    public IEnumerator UpdateThrowPosition(Vector2 dir)
    {
        Vector2 startingPos = transform.position;
        Vector2 endingPos = (Vector2)transform.position + dir.normalized * distance;
        float currentTimePassed = 0;

        while(currentTimePassed < timeToTarget)
        {
            if (!gameObject)
                break;
            float p = Mathf.Clamp01(currentTimePassed / timeToTarget);
            transform.position = Vector2.Lerp(startingPos, endingPos, p);
            transform.position += new Vector3(0, path.Evaluate(p),0);
            transform.localScale = Vector3.Lerp(Vector3.one, new Vector3(1.5f, 1.5f, 1), path.Evaluate(p));
            yield return new WaitForSeconds(.1f);
            currentTimePassed += .1f;
        }
        if (!gameObject)
        {
            transform.position = endingPos;

        }
    }
}
