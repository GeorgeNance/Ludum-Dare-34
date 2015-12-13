﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MouseControls : MonoBehaviour {

    public int boundary = 50;
    public float panSpeed = 5;

    private int screenWidth;
    private int screenHeight;
    private Camera camera;

    TargetManager targetMan;

    WorkerManager workMan;


	// Use this for initialization
	void Start () {
        workMan = GameObject.FindObjectOfType<WorkerManager>().GetComponent<WorkerManager>();
        camera = Camera.main;
        screenHeight = camera.pixelHeight;
        screenWidth = camera.pixelWidth;
        targetMan = GameObject.FindObjectOfType<TargetManager>().GetComponent<TargetManager>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePos = Input.mousePosition;
        Vector3 camerPos = camera.transform.position;
        //
        // Move Screen when mouse is out of bounds
        //

        //horizantol

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.mousePosition.x > screenWidth - boundary)
            {
                camerPos.x += panSpeed * Time.deltaTime;
            }
            if (Input.mousePosition.x < boundary)
            {
                camerPos.x -= panSpeed * Time.deltaTime;
            }
            //Vertical
            if (Input.mousePosition.y > screenHeight - boundary)
            {
                camerPos.z += panSpeed * Time.deltaTime; // move on +Z axis
            }
            if (Input.mousePosition.y < 0 + boundary)
            {
                camerPos.z -= panSpeed * Time.deltaTime; // move on -Z axis
            }

            camera.transform.position = camerPos;
        }



        if (Input.GetMouseButtonUp (1)) {
			Ray ray = Camera.main.ScreenPointToRay(mousePos);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit)){
                Debug.Log(hit.collider.name);

                workMan.AssignTask(hit.collider.gameObject);

                

                GameObject go = targetMan.GetPooledTarget();
                go.transform.position = new Vector3(hit.point.x,0,hit.point.z);
                go.SetActive(true);
            }
		}
	
	}
}
