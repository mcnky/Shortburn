using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFreezable
{
    void Freeze();
}




public class FreezeFan : MonoBehaviour, IFreezable
{
    [SerializeField] float freezeTime = 2;
    [SerializeField] private float speed = 2;
    [SerializeField] private AnimationCurve curve;
    public static float mult = 1f;
    
    void Update()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z += speed * mult;
        transform.rotation = Quaternion.Euler(rotation);
    }



    public void Freeze()
    {
        StartCoroutine(FreezeCoroutine());
    }


    private IEnumerator FreezeCoroutine()
    {
        float progress = 1;
        while (progress > 0)
        {
            progress -= Time.deltaTime / 0.2f;
            mult = curve.Evaluate(progress);
            yield return null;
        }
        yield return new WaitForSeconds(freezeTime);

        progress = 0;
        while (progress < 1)
        {
            progress += Time.deltaTime / 1f;
            mult = curve.Evaluate(progress);
            yield return null;
        }

    }


}
