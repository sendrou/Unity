using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 80; // ����� �������
    public GameObject cloudPrefab; // ������ ��� �������

    public float cloudScaleMin = 0.1f; // ���. ������� ������� ������
    public float cloudScaleMax = 0.1f; // ����. ������� ������� ������
    public float cloudSpeedMult = 1f; // ����������� �������� �������

    private GameObject[] cloudInstances;

    void Awake()
    {
        // ������� ������ ��� �������� ���� ����������� �������
        cloudInstances = new GameObject[numClouds];
        // ����� ������������ ������� ������ CloudAnchor
        GameObject anchor = GameObject.Find("CloudAnchor");
        // ������� � ����� �������� ���������� �������
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            // ������� ��������� cloudPrefab
            cloud = Instantiate<GameObject>(cloudPrefab);
            // ������� �������������� ��� ������
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(-20, 50);
            cPos.y = Random.Range(-20, 50);
            // �������������� ������
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            // ��������� ���������� �������� ��������� � �������� � ������
    
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            // ������� ������ �������� �� ��������� � anchor
            cloud.transform.SetParent(anchor.transform);

            cloudInstances[i] = cloud; // �������� ��������� ������ � ������
        }
    }

    void Update()
    {
        // ������ � ����� ��� ��������� ������
        foreach (GameObject cloud in cloudInstances)
        {
            // �������� ������� � ���������� ������
            Vector3 cPos = cloud.transform.position;
            // ��������� �������� ��� ������� �������
            cPos.x -= Time.deltaTime * cloudSpeedMult;
            // ���� ������ ���������� ������� ������ �����...
            if (cPos.x <=-50)
            {
                // ����������� ��� ������ ������
                cPos.x = 50;
            }
            // ��������� ����� ���������� � ������
            cloud.transform.position = cPos;
        }
    }
}