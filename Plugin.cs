using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using Utilla;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR;
using GorillaNetworking;
using HarmonyLib;
using Valve.VR;
using RassMobile.Mods;

namespace RassMobile
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    [BepInDependency("org.legoandmars.gorillatag.utilla", "1.5.0")] // Make sure to add Utilla 1.5.0 as a dependency!
    [ModdedGamemode]
    public class Plugin : BaseUnityPlugin
    {
        public GameObject _Phone;
        public TMP_Text _Text;
        public TMP_Text _Description;
        public RawImage _Image;
        Vector2 leftjoystick;
        bool isSteam;
        bool hold;
        bool holdJoyStickR;
        bool holdJoyStickL;
        bool holdInJoyStick;
        bool inAllowedRoom;
        List<ModFramework> mods = new List<ModFramework>();
        int currentMod = 0;
        bool phone;

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            mods.Add(new BounceMonke("Bounce Monke", "makes the player bouncy"));
            mods.Add(new MidAirTurn("Mid Air Turn", "pull down the left joystick turn mid air while also inverting the velocity"));
            mods.Add(new GrapplingHook("Grapplng Hook", "press and release A to grapple"));
            mods.Add(new SpiderMonke("Spider Monke", "press Trigger to shoot webs like spider man"));
            mods.Add(new TimeStop("Time Stop", "stop time by pressing A"));
            mods.Add(new Dash("Dash", "press A to dash"));
            mods.Add(new AirGrab("Air Grab", "hold Grip to grab the air (Its basically like platforms without the platforms)"));
            mods.Add(new Rewind("Rewind", "hold B to record your movement then release B to replay your movements"));
            mods.Add(new BiggerMonkey("Bigger Monkey", "makes you bigger"));
            mods.Add(new Upsidedown("Upsidedown", "turns you upsidedown"));
            mods.Add(new Flight("Flight", "hold trigger to fly"));
            mods.Add(new Geppo("Geppo", "hold trigger to and move your controller to launch your self (Mod idea by my brother)"));
            mods.Add(new FirstPerson("First Person Camera", "Makes your computer have a first person view"));
            
            Utilla.Events.GameInitialized += GameInitialized;
        }



        private void GameInitialized(object sender, EventArgs e)
        {
            // Player instance has been created
            isSteam = Traverse.Create(PlayFabAuthenticator.instance).Field("platform").GetValue().ToString().ToLower() == "steam";
            InstantiatePhone();
        }

        void Update()
        {
            if (inAllowedRoom)
            {
                if (isSteam) { leftjoystick = SteamVR_Actions.gorillaTag_LeftJoystick2DAxis.axis; }
                else { ControllerInputPoller.instance.leftControllerDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out leftjoystick); }

                if (ControllerInputPoller.instance.leftControllerPrimaryButton && !hold)
                {
                    hold = true;
                    if(phone)
                    {
                        _Phone.SetActive(false);
                        phone = false;
                        Debug.Log("Disabled");
                    }
                    else
                    {
                        _Phone.SetActive(true);
                        phone = true;
                        Debug.Log("Enabled");
                    }
                }
                else if(!ControllerInputPoller.instance.leftControllerPrimaryButton && hold)
                {
                    hold = false;
                }

                if (phone)
                {
                    

                    if (leftjoystick.x > 0.9f && !holdJoyStickR)
                    {
                        holdJoyStickR = true;
                        currentMod++;
                        if(currentMod > mods.Count - 1)
                        {
                            currentMod = 0;
                        }

                        _Text.text = (currentMod + 1) + ". " + mods[currentMod].Name;
                        _Description.text = mods[currentMod].Description;

                        _Image.color = mods[currentMod].Enabled ? Color.green : Color.red;

                    }
                    else if (leftjoystick.x <= 0.9f && holdJoyStickR)
                    {
                        holdJoyStickR = false;
                    }
                    
                    if(leftjoystick.x < -0.9f && !holdJoyStickL)
                    {
                        holdJoyStickL = true;
                        currentMod--;
                        if (currentMod < 0)
                        {
                            currentMod = mods.Count - 1;
                        }

                        _Text.text = (currentMod + 1) + ". " + mods[currentMod].Name;
                        _Description.text = mods[currentMod].Description;

                        _Image.color = mods[currentMod].Enabled ? Color.green : Color.red;

                    }
                    else if (leftjoystick.x >= -0.9f && holdJoyStickL)
                    {
                        holdJoyStickL = false;
                    }


                    if (ControllerInputPoller.instance.leftControllerSecondaryButton && !holdInJoyStick)
                    {
                        holdInJoyStick = true;
                        mods[currentMod].Enabled = !mods[currentMod].Enabled;
                        _Image.color = mods[currentMod].Enabled ? Color.green : Color.red;

                        if (mods[currentMod].Enabled)
                        {
                            mods[currentMod].OnEnabled();
                        }
                        else
                        {
                            mods[currentMod].OnDisabled();
                        }
                    }
                    else if(!ControllerInputPoller.instance.leftControllerSecondaryButton && holdInJoyStick)
                    {
                        holdInJoyStick = false;
                    }
                }

                for (int i = 0; i < mods.Count; i++)
                {
                    if (mods[i].Enabled)
                    {
                        mods[i].Update();
                    }
                }

            }
        }

        void InstantiatePhone()
        {
            Stream str = Assembly.GetExecutingAssembly().GetManifestResourceStream("RassMobile.modmenu");
            AssetBundle bundle = AssetBundle.LoadFromStream(str);

            GameObject Phone = bundle.LoadAsset<GameObject>("RassModMenu");
            _Phone = Instantiate(Phone);

            Transform leftHand = GorillaLocomotion.Player.Instance.leftControllerTransform;
            _Phone.transform.SetParent(leftHand.transform, false);
            _Phone.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            _Phone.transform.eulerAngles = new Vector3(90, -90, 0);
            _Phone.transform.localPosition = new Vector3(0.08f, 0.04f, -0.08f);

            _Text = _Phone.transform.Find("Canvas").Find("Text (TMP)").GetComponent<TMP_Text>();
            _Description = _Phone.transform.Find("Canvas").Find("Description").GetComponent<TMP_Text>();
            _Image = _Phone.transform.Find("Canvas").Find("RawImage").GetComponent<RawImage>();

            _Text.text = (currentMod + 1) + ". " + mods[currentMod].Name;
            _Description.text = mods[currentMod].Description;

            _Image.color = mods[currentMod].Enabled ? Color.green : Color.red;

            _Phone.SetActive(false);

        }

        [ModdedGamemodeJoin]
        private void RoomJoined(string gamemode)
        {
            // The room is modded. Enable mod stuff.
            inAllowedRoom = true;
            phone = false;

            for(int i = 0; i < mods.Count; i++)
            {
                if (mods[i].Enabled)
                {
                    mods[i].OnEnabled();
                }
            }

            _Phone.SetActive(false);
        }

        [ModdedGamemodeLeave]
        private void RoomLeft(string gamemode)
        {
            // The room was left. Disable mod stuff.
            inAllowedRoom = false;
            for (int i = 0; i < mods.Count; i++)
            {
                if (mods[i].Enabled)
                {
                    mods[i].OnDisabled();
                }
            }
            phone = false;
            _Phone.SetActive(false);
        }

    }
}
