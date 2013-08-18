using Mono.Cecil;
using ScrollsModLoader.Interfaces;
using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace Relentless
{
    public class Relentless : BaseMod
    {
        string serverHost = "";
        string serverKey  = "";

        public static string GetName()
        {
            return "Relentless";
        }

        public static int GetVersion()
        {
            return 1;
        }

        public static MethodDefinition[] GetHooks(TypeDefinitionCollection scrollsTypes, int version)
        {
            try
            {
                return new MethodDefinition[]
                {
                    scrollsTypes["Communicator"].Methods.GetMethod("Awake")[0]
                };
            }
            catch
            {
                return new MethodDefinition[] { };
            }
        }

        public override void BeforeInvoke(InvocationInfo info)
        {
            if (info.targetMethod.Equals("Awake"))
            {
                typeof(Communicator).GetField("publicKeyXml", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(info.target, serverKey);
            }
        }

        public override void AfterInvoke(InvocationInfo info, ref object returnValue)
        {
            if (info.targetMethod.Equals("Awake"))
            {
                typeof(Communicator).GetField("lobbyIpAddress", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(info.target, serverHost);
            }
        }
    }
}

