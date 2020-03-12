using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDetectorScript : MonoBehaviour
{

    public float angleOfSensors = 10f;
    public float rangeOfSensors = 10f;
    protected Vector3 initialTransformUp;
    protected Vector3 initialTransformFwd;
    public float strength;
    public float angleToClosestObj;
    public int numObjects;
    public bool debug_mode;
    public double double_strength;
    public float teta;
    public float mi;
    public double E = 2.7182818284590451;
    public double gaussian_strength;
    // Start is called before the first frame update
    void Start()
    {

        initialTransformUp = this.transform.up;
        initialTransformFwd = this.transform.forward;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // YOUR CODE HERE
        ObjectInfo anObject;
        anObject = GetClosestWall();
        if (anObject != null)
        {
            angleToClosestObj = anObject.angle;
            strength = 1.0f / (anObject.distance + 1.0f);
        }
        else
        { // no object detected
            strength = 0;
            angleToClosestObj = 0;
        }

    }

    public float GetAngleToClosestObstacle()
    {
        return angleToClosestObj;
    }

    public float GetLinearOuput()
    {
        return strength;
    }

    public virtual float GetGaussianOutput()
    {
        // META 2
        double aux1; // 1/(teta)*sqrt(2pi)
        double aux2; // -(1/2)*( (strength-mi)/ 2 ) ^2 

        aux1 = 1;
        aux2 = -(1 / 2) * Math.Pow((strength - mi) / teta, 2);
        gaussian_strength = aux1 * Math.Pow(E, aux2);
        strength = (float)gaussian_strength;

        return strength;

        throw new NotImplementedException();
    }

    public virtual float GetLogaritmicOutput()
    {
        // YOUR CODE HERE
        //META 2
        double_strength = (double)strength;
        double_strength = -Math.Log(double_strength);
        strength = (float)double_strength;
        return strength;
        throw new NotImplementedException();
    }

    public ObjectInfo[] GetVisibleWall()
    {
        return (ObjectInfo[])GetVisibleObjects("Wall").ToArray();
    }

    public ObjectInfo GetClosestWall()
    {
        ObjectInfo[] a = (ObjectInfo[])GetVisibleObjects("Wall").ToArray();
        if (a.Length == 0)
        {
            return null;
        }
        return a[a.Length - 1];
    }

    public List<ObjectInfo> GetVisibleObjects(string objectTag)
    {
        RaycastHit hit;
        List<ObjectInfo> objectsInformation = new List<ObjectInfo>();

        for (int i = 0; i * angleOfSensors < 360f; i++)
        {
            if (Physics.Raycast(this.transform.position, Quaternion.AngleAxis(-angleOfSensors * i, initialTransformUp) * initialTransformFwd, out hit, rangeOfSensors))
            {

                if (hit.transform.gameObject.CompareTag(objectTag))
                {
                    if (debug_mode)
                    {
                        Debug.DrawRay(this.transform.position, Quaternion.AngleAxis((-angleOfSensors * i), initialTransformUp) * initialTransformFwd * hit.distance, Color.red);
                    }
                    ObjectInfo info = new ObjectInfo(hit.distance, angleOfSensors * i + 90);
                    objectsInformation.Add(info);
                }
            }
        }

        objectsInformation.Sort();

        return objectsInformation;
    }

}
