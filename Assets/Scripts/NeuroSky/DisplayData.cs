using UnityEngine;
using System;

public class DisplayData : MonoBehaviour
{
	public Texture2D[] signalIcons;
	
	private int indexSignalIcons = 1;
	
    TGCConnectionController controller;

    private int poorSignal1;
    private int attention1;
    private int meditation1;
	
	private float delta;
	private float halpha;
	private float lalpha;
	private float theta;

	

    void Start()
    {
		

		controller = GameObject.Find("NeuroSkyTGCController").GetComponent<TGCConnectionController>();
		
		controller.UpdatePoorSignalEvent += OnUpdatePoorSignal;
		controller.UpdateAttentionEvent += OnUpdateAttention;
		controller.UpdateMeditationEvent += OnUpdateMeditation;
		
		controller.UpdateDeltaEvent += OnUpdateDelta;

		controller.UpdateHighAlphaEvent += OnUpdateHAlpha;
		controller.UpdateLowAlphaEvent += OnUpdateLAlpha;
		controller.UpdateThetaEvent += OnUpdateTheta;
		
    }
	
	void OnUpdatePoorSignal(int value){
		poorSignal1 = value;
		if(value < 25){
      		indexSignalIcons = 0;
		}else if(value >= 25 && value < 51){
      		indexSignalIcons = 4;
		}else if(value >= 51 && value < 78){
      		indexSignalIcons = 3;
		}else if(value >= 78 && value < 107){
      		indexSignalIcons = 2;
		}else if(value >= 107){
      		indexSignalIcons = 1;
		}
	}
	void OnUpdateAttention(int value){
		attention1 = value;
	}
	void OnUpdateMeditation(int value){
		meditation1 = value;
	}
	void OnUpdateDelta(float value){
		delta = value;
	}
	void OnUpdateHAlpha(float value){
		halpha = value;
	}
	void OnUpdateLAlpha(float value){
		lalpha = value;
	}
	void OnUpdateTheta(float value){
		theta = value;
	}


    void OnGUI()
    {
		GUILayout.BeginHorizontal();
		
		
        if (GUILayout.Button("Connect"))
        {
            controller.Connect();
        }
        if (GUILayout.Button("DisConnect"))
        {
            controller.Disconnect();
			indexSignalIcons = 1;
        }
		
		GUILayout.Space(Screen.width-250);
		GUILayout.Label(signalIcons[indexSignalIcons]);
		
		GUILayout.EndHorizontal();

		
        GUILayout.Label("PoorSignal: " + poorSignal1);
        GUILayout.Label("Attention: " + attention1);
        GUILayout.Label("Meditation: " + meditation1);

		GUILayout.Label("Theta: " + theta);
		float alpha = halpha + lalpha;
		GUILayout.Label("Alpha: " + alpha);


    }
}
