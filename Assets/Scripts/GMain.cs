using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMain : MonoBehaviour
{
    private static GMouseController mouseController_gmc;
    private static GGameController gameController_ggc;

    public static GMouseController getMouseController()
    {   
        if(GMain.mouseController_gmc == null)
        {
            GMain.mouseController_gmc = new GMouseController(new GMouseModel());
        }
        return GMain.mouseController_gmc;
    }

    public static GGameController getGameController()
    {   
        if(GMain.gameController_ggc == null)
        {
            GMain.gameController_ggc = new GGameController(new GGameModel(), new GGameView());
        }
        return GMain.gameController_ggc;
    }

    public static GGameplayController getGameplayController()
    {   
        return GMain.getGameController().getGameplayController();
    }

    public static bool isGameReady()
    {
        return GMain.gameController_ggc != null;
    }

    // Start is called before the first frame update
    void Start()
    {
        //ATLAS GENERATION...
        //GAtlasGenerator.generateAtlasFont();
        //GAtlasGenerator.generateAtlasObjects();
        //GAtlasGenerator.generateAtlasObjectsBlicks();
        //GAtlasGenerator.generateAtlasInterface();
        //...ATLAS GENERATION

        GMain.getGameController().onGameReady();


        //PROBLEM GENERATION...
        //new GProblemGenerator().generate(GRobotTemplate.ROBOT_DESCRIPTOR_TEST4, 15, 1000);
        //...PROBLEM GENERATION

       
    }

    // Update is called once per frame
    void Update()
    {
        GScreen.update();
        GMain.getMouseController().update();

        if(GScreen.isLandscapeMode())
        {
            GMain.getGameController().update();
            GGameView gameView_ggv = (GGameView) GMain.getGameController().getView();
            gameView_ggv.update();
            deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        }

        //HTML5 BUILD...
        this.redraw();
        //...HTML5 BUILD
    }

    void redraw()
    {
        GRenderer.clearScreen();

        if(GScreen.isLandscapeMode())
        {
            GGameView gameView_ggv = (GGameView) GMain.getGameController().getView();
            GRenderer.drawGameView(gameView_ggv);
            this.drawFPS();
        }
        else
        {
            GRenderer.drawPortraitMode();
        }
    }
/*
    void OnGUI()
    {
        GScreen.update();
        GMain.getMouseController().update();
        GGameView gameView_ggv = (GGameView) GMain.getGameController().getView();
        GRenderer.clearScreen();
        GRenderer.drawGameView(gameView_ggv);
        this.drawFPS();
    }*/
/*
    void Awake () {
        //QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = 61;
    }
*/
    private float deltaTime = 0.0f;

    private void drawFPS()
    {
        GRenderer.setColor(255, 255, 255, 0.1f);
        GRenderer.drawText(""+ Mathf.RoundToInt(1.0f / deltaTime), 100 - 2 * GScreen.getSidesRatio(), 95f, 5f, GRenderer.TEXT_ALIGN_MODE_ID_RIGHT);
        GRenderer.setColor(255, 255, 255);

        //Debug.Log(Time.deltaTime);
    }
}
