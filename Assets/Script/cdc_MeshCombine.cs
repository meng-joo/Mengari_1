using UnityEngine;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[ExecuteInEditMode]

public class cdc_MeshCombine : MonoBehaviour
{
    [Header("3D Mesh Center Pivot")]
    [Tooltip("�߽����� �� Transform�� ����. ������� ��� ���� ���� ��ǥ�� ����")]
    public Transform centerPosition;
    public bool autoCenterThis = true;

    private void Awake()
    {
        if (autoCenterThis)
        {
            centerPosition = this.transform;
        }
    }

    public void CombineMeshesChildrens()
    {
        Mesh myMesh = new Mesh();
        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();


        List<MeshFilter> meshF = new List<MeshFilter>();
        List<GameObject> subMesh = new List<GameObject>();

        for (int i = 0; i < meshFilters.Length; i++)
        {
            meshF.Add(meshFilters[i]);
            subMesh.Add(meshFilters[i].gameObject);
        }

        CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        int np = 0;
        while (np < meshF.Count)
        {
            if (meshFilters[np].sharedMesh != null)
            {
                combine[np].mesh = meshF[np].sharedMesh;
                combine[np].transform = meshF[np].transform.localToWorldMatrix;
                //meshF[np].gameObject.SetActive(false);
            }
            np++;
        }

        transform.GetComponent<MeshFilter>().mesh = myMesh;
        transform.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);
        transform.gameObject.SetActive(true);

        if (centerPosition != null)
        {
            gameObject.transform.position = centerPosition.position;
            gameObject.transform.rotation = centerPosition.rotation;

            Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
            Vector3[] vertices = mesh.vertices;
            int i = 0;
            while (i < vertices.Length)
            {
                vertices[i] -= centerPosition.position;
                i++;
            }
            mesh.vertices = vertices;
        }
        else
        {
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }


    }

    #region EditorOnly
#if UNITY_EDITOR
    [CustomEditor(typeof(cdc_MeshCombine))]
    public class cdc_MeshCombine_Instance : Editor
    {
        public cdc_MeshCombine myFild;

        private void OnEnable()
        {
            if (AssetDatabase.Contains(target))
            {
                myFild = null;
            }
            else
            {
                myFild = (cdc_MeshCombine)target;
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            cdc_MeshCombine script = target as cdc_MeshCombine;

            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.HelpBox("�ڽ��� ������ MeshFilter�� �����ݴϴ�.\nCenter Position�� Ʈ������ ���� �� �ش� Ʈ���� �� �߽����� �������� �� ��ġ �˴ϴ�.", MessageType.Info);
            EditorGUILayout.Space();

            if (GUILayout.Button("�ڽ� �޽� �ϳ��� ��ġ��"))
            {
                script.CombineMeshesChildrens();
            }
            EditorGUILayout.Space();
        }
    }
#endif
    #endregion


}
