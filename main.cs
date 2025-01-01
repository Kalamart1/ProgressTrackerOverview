using MelonLoader;
using UnityEngine;
using Il2CppTMPro;
using RumbleModdingAPI;
using Il2CppRUMBLE.Economy;
using Il2CppRUMBLE.Economy.Interactables;
using Il2CppRUMBLE.Players;

namespace ProgressTrackerOverview
{
    public static class BuildInfo
    {
        public const string ModName = "ProgressTrackerOverview";
        public const string ModVersion = "1.0.0";
        public const string Description = "Adds an overview of your total GC/BP progress on the gear market's progress tracker";
        public const string Author = "Kalamart";
        public const string Company = "";
    }

    public class MyMod : MelonMod
    {
        // variables
        bool initialized = false;
        bool isInGym = false;
        static ProgressTracker progressTracker;
        static CatalogHandler handler;
        GameObject BPGCtext;
        GameObject BPtext;
        GameObject GCtext;
        TextMeshPro bpobj_only;
        TextMeshPro gcobj_only;
        TextMeshPro bpobj;
        TextMeshPro gcobj;
        public static void Log(string msg)
        {
            MelonLogger.Msg(msg);
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            isInGym = (sceneName == "Gym");
            if (!initialized && isInGym)
            {
                Calls.onMapInitialized += InitOverview;
            }
        }

        public override void OnFixedUpdate()
        {
            UpdateProgressOverview();
        }

        /**
         * <summary>
         * Initializes the managers and objects that store the data for the progress tracker overview
         * </summary>
         */
        private void InitOverview()
        {
            // create new text object (copy from the progress text inside of the tracker)
            GameObject ptobj = Calls.GameObjects.Gym.Scene.GymProduction.SubStaticGroupBuildings.ProgressTracker.GetGameObject();
            progressTracker = ptobj.GetComponent<ProgressTracker>();
            handler = Calls.GameObjects.DDOL.GameInstance.Initializable.CatalogHandler.GetGameObject().GetComponent<CatalogHandler>();
            Transform OGparent = ptobj.transform.GetChild(1).GetChild(1).GetChild(0); // the parent obj in the tracker
            GameObject parent = GameObject.Instantiate(OGparent.gameObject);
            parent.SetActive(true);
            parent.transform.SetParent(ptobj.transform);
            parent.name = "Overview";
            parent.transform.localPosition = new Vector3(0f, -0.18f, 0.05f); // position right under the flipping board
            parent.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            parent.transform.localRotation = Quaternion.identity;

            BPGCtext = parent.transform.GetChild(0).gameObject;
            BPtext = parent.transform.GetChild(1).gameObject;
            GCtext = parent.transform.GetChild(2).gameObject;
            // when only one of the two texts appears, it's made bigger
            BPtext.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            GCtext.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Object.Destroy(parent.transform.GetChild(3).gameObject); // destroy the brown rectangle behind it
            bpobj_only = BPtext.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
            gcobj_only = GCtext.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
            bpobj = BPGCtext.transform.GetChild(0).gameObject.GetComponent<TextMeshPro>();
            gcobj = BPGCtext.transform.GetChild(1).gameObject.GetComponent<TextMeshPro>();
            Color darkBlue = gcobj.colorGradient.topLeft; // use the dark blue part of the original gradient
            // make gradient uniform with only the dark blue color
            gcobj.colorGradient = new VertexGradient(darkBlue);
            gcobj_only.colorGradient = new VertexGradient(darkBlue);
            // a "white" gradient is equivalent to removing it, so the text becomes yellow (background color)
            bpobj.colorGradient = new VertexGradient(Color.white);
            bpobj_only.colorGradient = new VertexGradient(Color.white);

            Log("Initialized Overview objects");
            initialized = true;
            UpdateProgressOverview();
        }

        /**
         * <summary>
         * Updates the text on the progress tracker overview, and selects
         * which of the 3 objects to show: GC and BP, only GC, or only BP.
         * </summary>
         */
        public void UpdateProgressOverview()
        {
            if (!initialized || !isInGym)
            {
                return;
            }
            int totalBPreq = 0;
            int totalGCreq = 0;
            foreach (string item in progressTracker.presentingItems)
            {
                CatalogItem obj = handler.GetCatalogItem(item);
                foreach (Currency req in obj.RequiredCurrencies)
                {
                    if (req.Type == Currency.CurrencyType.coin)
                    {
                        totalGCreq += req.Amount; // sum all GC requirements
                    }
                    else if (totalBPreq< req.Amount)
                    {
                        totalBPreq = req.Amount; // take max BP requirement
                    }
                }
            }

            // select the type of text we want: GC and BP, only GC, or only BP
            bool isGCtracked = (totalGCreq > 0);
            bool isBPtracked = (totalBPreq > 0);
            BPGCtext.SetActive(isGCtracked && isBPtracked);
            GCtext.SetActive(isGCtracked && !isBPtracked);
            BPtext.SetActive(!isGCtracked && isBPtracked);

            PlayerData playerData = Calls.Players.GetLocalPlayer().Data;
            int gc = playerData.EconomyData.CoinsAmount; // your current GC amount
            int bp = playerData.GeneralData.BattlePoints; // your current BP amount

            // set text for both GC and BP progress
            gcobj.text = $"GC: {gc}/{totalGCreq}";
            bpobj.text = $"BP: {bp}/{totalBPreq}";
            gcobj_only.text = gcobj.text;
            bpobj_only.text = bpobj.text;
        }
    }
}
