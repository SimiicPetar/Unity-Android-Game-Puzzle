﻿#if TTP_GAMEPROGRESSION
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
#if UNITY_IOS
using System.Runtime.InteropServices;
#endif

namespace Tabtale.TTPlugins
{
    /// <summary>
    /// This class logs game events like tutorial steps, mission status, level up in analytycs service.
    /// There are two analytics supported DeltaDNA and Firebase. DeltaDNA is deprecated 
    /// </summary>
    public class TTPGameProgression
    {
        /// <summary>
        /// Log tutorial step
        /// </summary>
        /// <param name="isMandatory">Indicates that step is mandatory</param>
        /// <param name="tutorialStepID">Step ID</param>
        /// <param name="tutorialName">Tutorial string name</param>
        /// <param name="tutorialStepName">Step string name</param>
        /// <param name="additionalParams">Other additional parameters of current step</param>
        public static void TutorialStep(bool isMandatory, int tutorialStepID, string tutorialName, string tutorialStepName, Dictionary<string, object> additionalParams)
        {
            CallAnalyticsByReflection("TutorialStep", new object[] { isMandatory, tutorialStepID, tutorialName, tutorialStepName, additionalParams });
        }

        /// <summary>
        /// Log moment when player reached main screen
        /// </summary>
        /// <param name="additionalParams">Other additional parameters</param>
        public static void ReachedMainScreen(Dictionary<string, object> additionalParams)
        {
            CallAnalyticsByReflection("ReachedMainScreen", new object[] { additionalParams });
        }

        /// <summary>
        /// Log mission started event
        /// </summary>
        /// <param name="id">Mission ID</param>
        /// <param name="name">Mission name</param>
        /// <param name="type">Mission type</param>
        /// <param name="missionStartedType">Type of mission startup</param>
        /// <param name="additionalParams">Other additional parameters</param>
        public static void MissionStarted(string id, string name, string type, string missionStartedType, Dictionary<string, object> additionalParams)
        {
            DDNAEvents.MissionStarted(id, name, type, missionStartedType, additionalParams);
        }

        /// <summary>
        /// Log mission completed event
        /// </summary>
        /// <param name="additionalParams">Other additional parameters</param>
        public static void MissionComplete(Dictionary<string, object> additionalParams)
        {
            DDNAEvents.MissionComplete(additionalParams);
        }

        /// <summary>
        /// Log mission is abandoned by user
        /// </summary>
        /// <param name="additionalParams">Other additional parameters</param>
        public static void MissionAbandoned(Dictionary<string, object> additionalParams)
        {
            DDNAEvents.MissionAbandoned(additionalParams);
        }

        /// <summary>
        /// Log mission failed event
        /// </summary>
        /// <param name="additionalParams">Other additional parameters</param>
        public static void MissionFailed(Dictionary<string, object> additionalParams)
        {
            DDNAEvents.MissionFailed(additionalParams);
        }

        /// <summary>
        /// Player levels up in the game
        /// </summary>
        /// <param name="skinName">Player's skin name</param>
        /// <param name="levelUpName">New level name</param>
        /// <param name="level">Level value</param>
        /// <param name="additionalParams">Other additional parameters</param>
        public static void LevelUp(string skinName, string levelUpName, int level, Dictionary<string, object> additionalParams)
        {
            DDNAEvents.LevelUp(skinName, levelUpName, level, additionalParams);
        }

        /// <summary>
        /// Deprecated class for Delta DNA analytics
        /// Use only for old apps
        /// </summary>
        public class DDNAEvents
        {
            /// <summary>
            /// Log mission started event
            /// </summary>
            /// <param name="id">Mission ID</param>
            /// <param name="name">Mission name</param>
            /// <param name="type">Mission type</param>
            /// <param name="missionStartedType">Type of mission startup</param>
            /// <param name="additionalParams">Other additional parameters</param>
            public static void MissionStarted(string id, string name, string type, string missionStartedType, Dictionary<string, object> additionalParams)
            {
                Debug.LogWarning("DDNAEvents:MissionStarted used from GameProgression plugin");
                CallAnalyticsByReflection("MissionStarted", new object[] { id, name, type, missionStartedType, additionalParams });
            }

            /// <summary>
            /// Log mission completed event
            /// </summary>
            /// <param name="additionalParams">Other additional parameters</param>
            public static void MissionComplete(Dictionary<string, object> additionalParams)
            {
                Debug.LogWarning("DDNAEventsMissionComplete used from GameProgression plugin");
                CallAnalyticsByReflection("MissionComplete", new object[] { "missionCompleted", additionalParams });
            }

            /// <summary>
            /// Log mission is abandoned by user
            /// </summary>
            /// <param name="additionalParams">Other additional parameters</param>
            public static void MissionAbandoned(Dictionary<string, object> additionalParams)
            {
                Debug.LogWarning("DDNAEventsMissionAbandoned used from GameProgression plugin");
                CallAnalyticsByReflection("MissionComplete", new object[] { "missionAbandoned", additionalParams });
            }

            /// <summary>
            /// Log mission failed event
            /// </summary>
            /// <param name="additionalParams">Other additional parameters</param>
            public static void MissionFailed(Dictionary<string, object> additionalParams)
            {
                Debug.LogWarning("DDNAEvents:MissionFailed used from GameProgression plugin");
                CallAnalyticsByReflection("MissionComplete", new object[] { "missionFailed", additionalParams });
            }

            /// <summary>
            /// Player levels up in the game
            /// </summary>
            /// <param name="skinName">Player's skin name</param>
            /// <param name="levelUpName">New level name</param>
            /// <param name="level">Level value</param>
            /// <param name="additionalParams">Other additional parameters</param>
            public static void LevelUp(string skinName, string levelUpName, int level, Dictionary<string, object> additionalParams)
            {
                Debug.LogWarning("DDNAEvents:LevelUp used from GameProgression plugin");
                CallAnalyticsByReflection("LevelUp", new object[] { skinName, levelUpName, level, additionalParams });
                if (Impl != null)
                {
                    Impl.PopUpMgrSetLevel(level);
                }
            }
        }


        public class FirebaseEvents
        {
            /// <summary>
            /// Log mission started event
            /// </summary>
            /// <param name="missionID">Mission ID</param>
            /// <param name="additionalParams">Other additional parameters</param>
            public static void MissionStarted(int missionID, Dictionary<string, object> additionalParams)
            {
                Debug.LogWarning("FirebaseEvents:MissionStarted: missionID=" + missionID);
                CallAnalyticsByReflection("MissionStartedFirebase", new object[] { missionID, additionalParams });
            }

            /// <summary>
            /// Log mission completed event
            /// </summary>
            /// <param name="additionalParams">Other additional parameters</param>
            public static void MissionComplete(Dictionary<string, object> additionalParams)
            {
                Debug.LogWarning("FirebaseEvents:MissionComplete:");
                CallAnalyticsByReflection("MissionCompleteFirebase", new object[] { "missionCompleted", additionalParams });
            }

            /// <summary>
            /// Log mission is abandoned by user
            /// </summary>
            /// <param name="additionalParams">Other additional parameters</param>
            public static void MissionAbandoned(Dictionary<string, object> additionalParams)
            {
                Debug.LogWarning("FirebaseEvents: MissionAbandoned:");
                CallAnalyticsByReflection("MissionCompleteFirebase", new object[] { "missionAbandoned", additionalParams });
            }

            /// <summary>
            /// Log mission failed event
            /// </summary>
            /// <param name="additionalParams">Other additional parameters</param>
            public static void MissionFailed(Dictionary<string, object> additionalParams)
            {
                Debug.LogWarning("FirebaseEvents:MissionFailed:");
                CallAnalyticsByReflection("MissionCompleteFirebase", new object[] { "missionFailed", additionalParams });
            }

            public static void LevelUp(int level, Dictionary<string, object> additionalParams)
            {
                Debug.LogWarning("FirebaseEvents:LevelUp: level=" + level);
                CallAnalyticsByReflection("LevelUpFirebase", new object[] { level, additionalParams });
                if (Impl != null)
                {
                    Impl.PopUpMgrSetLevel(level);
                }
            }
        }

        private static void CallAnalyticsByReflection(string methodName, object[] parameters)
        {
            System.Type analyticsClsType = System.Type.GetType("Tabtale.TTPlugins.TTPAnalytics");
            if (analyticsClsType != null)
            {
                MethodInfo method = analyticsClsType.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
                if (method != null)
                {
                    method.Invoke(null, parameters);
                }
                else
                {
                    Debug.LogWarning("CallAnalyticsByReflection:: could not find method - " + methodName);
                }
            }
            else
            {
                Debug.LogWarning("CallAnalyticsByReflection:: could not find TTPAnalytics class");
            }
        }

        private interface ITTPGameProgression
        {
            void PopUpMgrSetLevel(int level);
        }
        private static ITTPGameProgression _impl;
        private static ITTPGameProgression Impl
        {
            get
            {
                if (_impl == null)
                {
                    if (TTPCore.DevMode)
                    {
                        _impl = new EditorImpl();
                    }
                    else if (UnityEngine.Application.platform == UnityEngine.RuntimePlatform.Android ||
                        UnityEngine.Application.platform == UnityEngine.RuntimePlatform.IPhonePlayer)
                    {
#if UNITY_ANDROID
                        _impl = new AndroidImpl ();
#endif
#if UNITY_IOS && !TTP_DEV_MODE
                        _impl = new IosImpl();
#endif

                    }
                    else
                    {
#if UNITY_EDITOR
                        _impl = new EditorImpl();
#endif
                    }
                }
                if (_impl == null)
                {
                    Debug.LogError("TTPGameProgression::Impl: failed to create native impl");
                }
                return _impl;
            }
        }
#if UNITY_ANDROID
        private class AndroidImpl : ITTPGameProgression
        {
            private const string SERVICE_GET_METHOD = "getPopUpMgr";

            private AndroidJavaObject _serivceJavaObject;

            private AndroidJavaObject ServiceJavaObject
            {
                get
                {
                    if (_serivceJavaObject == null)
                    {
                        _serivceJavaObject = ((TTPCore.ITTPCoreInternal)TTPCore.Impl).GetServiceJavaObject(SERVICE_GET_METHOD);
                    }
                    if (_serivceJavaObject == null)
                        Debug.LogError("TTPGameProgression::AndroidImpl: failed to get popupmgr native instance.");
                    return _serivceJavaObject;
                }
            }

            public void PopUpMgrSetLevel(int level)
            {
                if(ServiceJavaObject != null)
                {
                    ServiceJavaObject.Call("setLevel", level);
                }
            }
        }
#endif
#if UNITY_IOS && !TTP_DEV_MODE
        private class IosImpl : ITTPGameProgression
        {
            [DllImport("__Internal")]
            private static extern void ttpPopUpMgrSetLevel(int level);

            public void PopUpMgrSetLevel(int level)
            {
                ttpPopUpMgrSetLevel(level);
            }
        }
#endif
        //#if UNITY_EDITOR
        private class EditorImpl : ITTPGameProgression
        {
            public void PopUpMgrSetLevel(int level)
            {
            }
        }
//#endif
    }

}
#endif