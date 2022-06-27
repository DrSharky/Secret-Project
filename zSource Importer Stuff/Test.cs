using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace uSrcTools
{
	public class Test : MonoBehaviour
    {
		public static Test Inst;

        public SourceBSPLoader bsp;
		public SourceStudioModel model;
		public Material testMaterial;
		public Texture cameraTexture;
        public GameObject modelsGameObject;

        public string exportLocation = "D:\\uSource\\Example\\";
		public string mapName;
		public string modelName;
        public string modelFolder;
		public bool skinnedModel = false;
		public Transform player;
		public Transform playerCamera;
		public Transform skyCamera;
		public Light light_environment;
		public Vector3 skyCameraOrigin;
		public Vector3 startPos;

		public bool loadMap = true;
		public bool loadModel = false;
        public bool batchLoadModels = false;
		public bool exportMap = false;
		public bool isL4D2 = false;
		public bool forceHDR = false;
		public bool skipSky = true;


        void Awake ()
		{
			Inst = this;
		}

		void Start ()
		{
			//player.transform.position = GameObject.Find ("info_player_start").transform.position;

			if (loadMap)
			{
				if (bsp == null)
					bsp = GetComponent<SourceBSPLoader> ();

				bsp.Load (mapName);
		        /*if (exportMap)
		    	{
                    COLLADAExport.Geometry g = bsp.map.BSPToGeometry ();
		    	    print ("Exporting map.");
		    	    COLLADAExport.Export(@"I:\uSource\test\"+mapName+".dae",g,false,false);
		    	    COLLADAExport.Export (exportLocation+mapName+".dae ",g,false,false);
		        }*/
		    }

		    if(loadModel)
		    {
    			GameObject modelObj = new GameObject("TestModel ");
			    model.Load (@"models/"+modelName+".mdl");
                Debug.Log(@"models/"+modelName+".mdl");
                //model.GetInstance(modelObj,skinnedModel);
                model.GetInstance (modelObj,skinnedModel,0);
			    //modelObj.transform.localEulerAngles=new Vector3(270,0,0);
		    }
            if (batchLoadModels)
            {
                LoadModelsRec(modelFolder, modelsGameObject);
            }

	    }

        void LoadModelsRec(string modelFolder, GameObject parentObject)
        {
            for (int i = 0; i < Directory.GetDirectories(modelFolder).Length; i++)
            {
                string objName = Directory.GetDirectories(modelFolder)[i].Replace(modelFolder + @"\", "");
                objName = objName.Substring(0, 1).ToUpper() + objName.Substring(1);
                GameObject newObj = GameObject.Find(objName);
                if (newObj == null)
                    newObj = new GameObject(objName);
                newObj.transform.parent = parentObject.transform;
                LoadModelsRec(Directory.GetDirectories(modelFolder)[i], newObj);
            }

            string[] modelFiles = Directory.GetFiles(modelFolder);

            for (int j = 0; j < modelFiles.Length; j++)
            {
                SourceStudioModel newModel = new SourceStudioModel();
                if (modelFiles[j].Contains(".mdl"))
                {
                    newModel.Load(modelFiles[j]);
                    string modelName = modelFiles[j].Replace(modelFolder + @"\", "");
                    modelName = modelName.Replace(".mdl", "");
                    GameObject modelObj = GameObject.Find(modelName);
                    if (modelObj == null || modelObj.GetComponent<MeshRenderer>() == null)
                        modelObj = new GameObject(modelName);

                    newModel.GetInstance(modelObj, skinnedModel, 0);
                    modelObj.transform.parent = parentObject.transform;
                }
            }
        }

		void Update ()
		{
			//BSP.DrawDebugObjects (player.position);
		}

		void OnDrawGizmos ()
		{
			if (model!=null)
				model.OnDrawGizmos ();
		}
	}

}