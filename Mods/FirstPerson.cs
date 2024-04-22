using GorillaNetworking;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace RassMobile.Mods
{
    internal class FirstPerson : ModFramework
    {
        private GameObject TPC = GameObject.Find("Player Objects/Third Person Camera");
        
        public Firstperson(string name, string description) : base(name, description)
        {
        }
        
        public override void OnEnabled()
        {
            if (TPC != null)
            {
                TPC.SetActive(false);
            }
            else
            {
                Debug.LogError("Failed to find Third Person Camera GameObject.");
            }
        }

        public override void OnDisabled()
        {
            if (TPC != null)
            {
                TPC.SetActive(false);
            }
            else
            {
                Debug.LogError("Failed to find Third Person Camera GameObject.");
            }
        }
    }
}
