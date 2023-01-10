using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropeSegmentPrefab; // Шаблон Rope Segment для создания новых звеньев
    List<GameObject> ropeSegments = new List<GameObject>();
    public bool isIncreasing { get; set; }
    public bool isDecreasing { get; set; }
    public Rigidbody2D connectedObject; // Объект твёрдого тела, к которому следует
                                        // присоединить конец веревки.
    public float maxRopeSegmentLength = 1.0f; // Максимальная длина звена верёвки
                                              // (если потребуется удлинить верёвку больше,
                                              // чем на эту величину, будет создано новое
                                              // звено.
    public float ropeSpeed = 4.0f; // Как быстро должны создаваться новые звенья верёвки?
    
    LineRenderer lineRenderer; // Визуализатор, отображающий верёвку

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        ResetLength(); // Сбросить состояние верёвки в исходное
    }

    // Удаляет все звенья и создаёт новое
    public void ResetLength()
    {
        foreach (var item in ropeSegments)
        {
            Destroy(item);
        }

        ropeSegments = new List<GameObject>();

        isDecreasing = false;
        isIncreasing = false;

        CreateRopeSegment();
    }

    // Добавляет новое звено верёвки к верхнему концу
    void CreateRopeSegment()
    {
        // Создать новое звено
        GameObject segment = (GameObject)Instantiate(
            ropeSegmentPrefab,
            this.transform.position,
            Quaternion.identity);

        // Сделать звено потомком объекта this
        // и сохранить его мировые координаты
        segment.transform.SetParent(this.transform, true);

        // Получить твёрдое тело звена
        Rigidbody2D segmentBody = segment.GetComponent<Rigidbody2D>();

        // Получить длину сочленения из звена
        SpringJoint2D segmentJoint = segment.GetComponent<SpringJoint2D>();

        // Ошибка, если шаблон звена не имеет 
        // твёрдого тела или пружинного сочленения - нужны оба
        if (segmentBody == null || segmentJoint == null)
        {
            Debug.LogError("Rope segment body prefab has no " +
                "Rigidbody2D and/or SpringJoint2D!");

            return;
        }

        // Теперь, после всех проверок, можно добавить
        // новое звено в начало списка звеньев
        ropeSegments.Insert(0, segment);

        // Если это "первое" звено, его нужно соединить
        // с ногой гномика
        if(ropeSegments.Count == 1)
        {
            if (connectedObject != null)
            {
                // Соединить звено с сочленением несущего объекта
                SpringJoint2D connectedObjectJoint = connectedObject.GetComponent<SpringJoint2D>();
                connectedObjectJoint.connectedBody = segmentBody;
                connectedObjectJoint.distance = 0.1f;

                segmentJoint.distance = maxRopeSegmentLength;
            }
        }
        else
        {
            // Это не первое звено, его нужно соединить с 
            // предыдущим звеном

            // Получить второе звено
            GameObject nextSegment = ropeSegments[1];

            // Получить сочленение для соединения
            SpringJoint2D nextSegmentJoint = nextSegment.GetComponent<SpringJoint2D>();

            // Присоединить сочленение к новому звену
            nextSegmentJoint.connectedBody = segmentBody;

            // Установить начальную длину сочленения нового звена
            // равной 0 - она увеличится автоматически.
            segmentJoint.distance = 0.0f;
        }

        // Соединить новое звено с опорой для верёвки (то есть с объектом this)
        segmentJoint.connectedBody = this.GetComponent<Rigidbody2D>();
    }

    // Вызывается, когда нужно укоротить верёвку,
    // и удаляет звено сверху.
    void RemoveRopeSegment()
    {
        // Если звеньев меньше двух, выйти
        if (ropeSegments.Count < 2)
        {
            return;
        }

        // Получить верхнее звено и звено  под ним.
        GameObject topSegment = ropeSegments[0];
        GameObject nextSegment = ropeSegments[1];

        // Соединить второе звено с опорой для верёвки.
        SpringJoint2D nextSegmentJoint = nextSegment.GetComponent<SpringJoint2D>();

        nextSegmentJoint.connectedBody = this.GetComponent<Rigidbody2D>();

        // Удалить верхнее звено из списка и уничтожить его.
        ropeSegments.RemoveAt(0);
        Destroy(topSegment);
    }

    // При необходимости в каждом кадре длина веревки
    // удлиняется или укорачивается
    void Update()
    {
        // Получить верхнее звено и его сочленение.
        GameObject topSegment = ropeSegments[0];
        SpringJoint2D topSegmentJoint = topSegment.GetComponent<SpringJoint2D>();

        if(isIncreasing)
        {
            // Веревку нужно удлинить. Если длина сочленения больше
            // или равна максимальной, добавляется новое звено;
            // иначе увеличивается длина сочленения звена.

            if (topSegmentJoint.distance >= maxRopeSegmentLength)
            {
                CreateRopeSegment();
            }
            else
            {
                topSegmentJoint.distance += ropeSpeed * Time.deltaTime;
            }
        }

        if (isDecreasing)
        {
            // Верёвку нужно удлинить. Если длина сочленения
            // близка к нулю, удалить звено; иначе
            // уменьшить длину сочленения верхнего звена
            if (topSegmentJoint.distance <= 0.005f)
            {
                RemoveRopeSegment();
            }
            else
            {
                topSegmentJoint.distance -= ropeSpeed * Time.deltaTime;
            }
        }

        if (lineRenderer != null)
        {
            // Визуализатор LineRenderer рисует линию по
            // коллекции точек. Эти точки должны соответствовать
            // позициям звеньев верёвки.

            // Число вершин, отображаемых визуализатором,
            // равно числу звеньев плюс одна точка
            // на верхней опоре плюс одна точка
            // на ноге гномика
            lineRenderer.positionCount = ropeSegments.Count + 2;

            // Верхняя вершина всегда соответствует положению опоры
            lineRenderer.SetPosition(0, this.transform.position);

            // Передать визуализатору координаты всех звеньев верёвки
            for (int i = 0; i < ropeSegments.Count; i++)
            {
                lineRenderer.SetPosition(i + 1, ropeSegments[i].transform.position);
            }

            // Последняя точка соответствует точке привязки
            // несущего объекта
            SpringJoint2D connectedObjectJoint = connectedObject.GetComponent<SpringJoint2D>();
            lineRenderer.SetPosition(ropeSegments.Count + 1,
                connectedObject.transform.TransformPoint(connectedObjectJoint.anchor));
        }
    }
}
