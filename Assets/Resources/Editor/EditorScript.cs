using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorScript : EditorWindow

{
    Vector3 RayHitLocation;
    public string[] State = new string[]
    {
        "DevCube", "HealthPickup", "AmmoPickup"
    };
    public int index;
    Color color;
    [MenuItem("Window/TestEditorScript")]
    public static void ShowWindow()
    {
        GetWindow<EditorScript>("TestEditor");
    }

    private void OnGUI()
    {

        GUILayout.Label("Dev-Spawn Menu");
        Vector3 CameraPosition = UnityEditor.SceneView.lastActiveSceneView.camera.transform.position;
        Vector3 CameraForwardVector = UnityEditor.SceneView.lastActiveSceneView.camera.transform.forward;

        GameObject DevCube = Resources.Load("Prefabs/Dev Cube") as GameObject;
        GameObject HealthPickup = Resources.Load("Prefabs/Health Pickup") as GameObject;
        GameObject AmmoPickup = Resources.Load("Prefabs/Ammo Pickup") as GameObject;
        index = EditorGUILayout.Popup(index, State);

        RaycastHit hit;
        Ray landingRay = new Ray(CameraPosition, CameraForwardVector);
        color = EditorGUILayout.ColorField("Color", color);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Create Object"))
        {
            if (Physics.Raycast(landingRay, out hit, 50))
            {
                if (hit.collider)
                {
                    if (index == 0)
                    {
                        GameObject Cube = Instantiate(DevCube, RayHitLocation, Quaternion.Euler(0, 0, 0));
                        Renderer cuberenderer = Cube.GetComponent<Renderer>();

                        if (cuberenderer != null)
                        {
                            cuberenderer.material.color = color;
                        }
                    }

                    if (index == 1)
                    {
                        Instantiate(HealthPickup, RayHitLocation, Quaternion.Euler(0, 0, 0));
                    }

                    if (index == 2)
                    {
                        Instantiate(AmmoPickup, RayHitLocation, Quaternion.Euler(0, 0, 0));
                    }
                }
            }
        }

        if (GUILayout.Button("Color Selected Object"))
            if (index == 0)
            {
                foreach (GameObject obj in Selection.gameObjects)
                {
                    Renderer renderer = obj.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = color;
                    }
                }
            }
        GUILayout.EndHorizontal();

    }

    private void OnInspectorUpdate()
    {
        Vector3 CameraPosition = UnityEditor.SceneView.lastActiveSceneView.camera.transform.position;
        Vector3 CameraForwardVector = UnityEditor.SceneView.lastActiveSceneView.camera.transform.forward;
        GameObject DevCube = Resources.Load("Dev Cube") as GameObject;

        RaycastHit hit;
        Ray landingRay = new Ray(CameraPosition, CameraForwardVector);

        if (Physics.Raycast(landingRay, out hit, 50))
        {
            if (hit.collider)
            {
                RayHitLocation = new Vector3(hit.point.x, hit.point.y + 0.5f, hit.point.z);
            }
        }
    }
}
