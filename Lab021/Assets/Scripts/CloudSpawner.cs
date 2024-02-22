using UnityEngine;

public class CloudCrafter : MonoBehaviour
{
    [Header("Set in Inspector")]
    public int numClouds = 80; // Число облаков
    public GameObject cloudPrefab; // Шаблон для облаков

    public float cloudScaleMin = 0.1f; // Мин. масштаб каждого облака
    public float cloudScaleMax = 0.1f; // Макс. масштаб каждого облака
    public float cloudSpeedMult = 1f; // Коэффициент скорости облаков

    private GameObject[] cloudInstances;

    void Awake()
    {
        // Создать массив для хранения всех экземпляров облаков
        cloudInstances = new GameObject[numClouds];
        // Найти родительский игровой объект CloudAnchor
        GameObject anchor = GameObject.Find("CloudAnchor");
        // Создать в цикле заданное количество облаков
        GameObject cloud;
        for (int i = 0; i < numClouds; i++)
        {
            // Создать экземпляр cloudPrefab
            cloud = Instantiate<GameObject>(cloudPrefab);
            // Выбрать местоположение для облака
            Vector3 cPos = Vector3.zero;
            cPos.x = Random.Range(-20, 50);
            cPos.y = Random.Range(-20, 50);
            // Масштабировать облако
            float scaleU = Random.value;
            float scaleVal = Mathf.Lerp(cloudScaleMin, cloudScaleMax, scaleU);
            // Применить полученные значения координат и масштаба к облаку
    
            cloud.transform.position = cPos;
            cloud.transform.localScale = Vector3.one * scaleVal;
            // Сделать облако дочерним по отношению к anchor
            cloud.transform.SetParent(anchor.transform);

            cloudInstances[i] = cloud; // Добавить экземпляр облака в массив
        }
    }

    void Update()
    {
        // Обойти в цикле все созданные облака
        foreach (GameObject cloud in cloudInstances)
        {
            // Получить масштаб и координаты облака
            Vector3 cPos = cloud.transform.position;
            // Увеличить скорость для ближних облаков
            cPos.x -= Time.deltaTime * cloudSpeedMult;
            // Если облако сместилось слишком далеко влево...
            if (cPos.x <=-50)
            {
                // Переместить его далеко вправо
                cPos.x = 50;
            }
            // Применить новые координаты к облаку
            cloud.transform.position = cPos;
        }
    }
}