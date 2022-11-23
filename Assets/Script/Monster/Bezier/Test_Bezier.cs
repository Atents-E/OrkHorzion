using System.Collections;
using UnityEngine;
using UnityEditor;


public class Test_Bezier : MonoBehaviour
{
    public Transform target;

    //public float slerpTime = 0.5f;
    //float currentTime = 0;

    [Range(0,1)]
    public float testTime = 0.5f;

    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;
    public Vector3 p4;

    private void Awake()
    {
        p1 = transform.position;
        p2 = transform.position + transform.up * 6.0f;
        p3 = target.position + transform.up * 10.0f;
        p4 = target.position;

        //Vector3 dis = p1 - p3;
        //Vector3 vDis = p1 + (dis / 2);
        ////vDis.y += 20f;
        //Vector3 disz;
        //disz.z = transform.position.z - target.position.z;

        //vDis = new Vector3(vDis.x, vDis.y + 20f, disz.z);

        //p1 = transform.position;
        //p2 = vDis.normalized; //(vDis - p1).normalized;
        ////p2 = (-target.forward - transform.forward) + transform.up * 5.0f;
        //p3 = target.position;
    }

    private void Update()
    {
        testTime += Time.deltaTime * 0.7f;
        //p1 = transform.position;
        //p2 = transform.position + transform.up * 2.0f;
        //p3 = target.position + transform.up * 2.0f;
        //p4 = target.position;


        transform.position = BezierTest(p1, p2, p3, p4, testTime);
        //transform.position = BezierTest2(p1, p2, p3, testTime);
        //transform.position = BezierTest2(p1, p2, p3, testTime);

        //transform.position = Vector3.Slerp(transform.position, target.transform.position, 0.5f);

        //currentTime += Time.deltaTime;

        //if (currentTime >= slerpTime)
        //{
        //    currentTime = slerpTime;
        //}

        //testTime += Time.deltaTime * 0.01f;
        //if (testTime > 0.3)
        //{
        //    testTime += Time.deltaTime * 0.3f;
        //    if (testTime > 1)
        //    {
        //        Destroy(gameObject, 3.0f);
        //    }
        //}

        //float t = currentTime / slerpTime;
        //t = t * t * t * (t * (6f * t - 15f) + 10f);

        //float t = currentTime / slerpTime;
        //transform.position = Vector3.Slerp(transform.position, BezierTest(p1, p2, p3, p4, testTime), t);

        //transform.position = BezierTest(p1, p2, p3, p4, testTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.01f)
        {
            Destroy(this.gameObject,0.1f);
        }
    }

    // 3Â÷¿ø °î¼±
    public Vector3 BezierTest(Vector3 P_1, Vector3 P_2, Vector3 P_3, Vector3 P_4, float value)
    {
        Vector3 A = Vector3.Lerp(P_1, P_2, value);
        Vector3 B = Vector3.Lerp(P_2, P_3, value);
        Vector3 C = Vector3.Lerp(P_3, P_4, value);

        Vector3 D = Vector3.Lerp(A, B, value);
        Vector3 E = Vector3.Lerp(B, C, value);

        Vector3 F = Vector3.Lerp(D, E, value);
        return F;
    }

    // 2Â÷¿ø °î¼±
    public Vector3 BezierTest2(Vector3 P_1, Vector3 P_2, Vector3 P_3, float value)
    {
        Vector3 A = Vector3.Lerp(P_1, P_2, value);
        Vector3 B = Vector3.Lerp(P_2, P_3, value);

        Vector3 D = Vector3.Lerp(A, B, value);

        return D;
    }
}

[CanEditMultipleObjects]
[CustomEditor(typeof(Test_Bezier))]
public class Test2_Bezier : Editor
{
    private void OnSceneGUI()
    {
        Test_Bezier Generator = (Test_Bezier)target;

        Generator.p1 = Handles.PositionHandle(Generator.p1, Quaternion.identity);
        Generator.p2 = Handles.PositionHandle(Generator.p2, Quaternion.identity);
        Generator.p3 = Handles.PositionHandle(Generator.p3, Quaternion.identity);
        Generator.p4 = Handles.PositionHandle(Generator.p4, Quaternion.identity);

        Handles.DrawLine(Generator.p1, Generator.p2);
        Handles.DrawLine(Generator.p3, Generator.p4);

        int count = 50;
        for (float i = 0; i < count; i++)
        {
            float Value_Before = i / count;
            Vector3 Before = Generator.BezierTest(Generator.p1, Generator.p2, Generator.p3, Generator.p4, Value_Before);

            float Value_After = (i + 1) / count;
            Vector3 After = Generator.BezierTest(Generator.p1, Generator.p2, Generator.p3, Generator.p4, Value_After);

            Handles.DrawLine(Before, After);
        }
    }

}