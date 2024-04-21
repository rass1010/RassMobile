using GorillaNetworking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace RassMobile.Mods
{
    internal class Flight : ModFramework
    {

        bool isSteam;
        Vector2 leftjoystick;
        float speed = 1500f;

        public Flight(string name, string description) : base(name, description)
        {
        }

        public override void Update()
        {
            base.Update();
            if(ControllerInputPoller.instance.rightControllerIndexFloat > 0f)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.velocity = GorillaLocomotion.Player.Instance.rightControllerTransform.forward * speed * Time.deltaTime * ControllerInputPoller.instance.rightControllerIndexFloat;
            }

            

        }

    }
}
