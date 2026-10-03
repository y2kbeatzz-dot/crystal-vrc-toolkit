using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Crystal.VRCToolkit
{
    public sealed class CrystalToolkit : EditorWindow
    {
        sealed class Tool
        {
            public string Name, Category, Description, Package, Docs, Repo;
            public Tool(string n, string c, string d, string p, string u, string r = "")
            { Name=n; Category=c; Description=d; Package=p; Docs=u; Repo=r; }
        }
        static readonly Tool[] Tools = {
            new Tool("Modular Avatar", "Avatars", "Build outfits, toggles and reusable avatar parts without destructive edits.", "nadena.dev.modular-avatar", "https://modular-avatar.nadena.dev/docs/intro", "https://vpm.nadena.dev/vpm.json"),
            new Tool("VRCFury", "Avatars", "Add avatar features and compatible accessories with less manual setup.", "com.vrcfury.vrcfury", "https://vrcfury.com/download/", "https://vcc.vrcfury.com"),
            new Tool("Avatar Optimizer", "Optimization", "Non-destructive utilities for reducing avatar complexity at build time.", "com.anatawa12.avatar-optimizer", "https://vpm.anatawa12.com/avatar-optimizer/en/", "https://vpm.anatawa12.com/vpm.json"),
            new Tool("lilToon", "Shaders", "Feature-rich toon shading for avatar materials.", "jp.lilxyzw.liltoon", "https://lilxyzw.github.io/lilToon/", "https://lilxyzw.github.io/vpm-repos/vpm.json"),
            new Tool("Poiyomi", "Shaders", "Avatar shading, effects and material workflows. Install from the official page.", "", "https://www.poiyomi.com/"),
            new Tool("VRChat Avatar SDK", "Avatars", "Official avatar setup, validation and upload tools. Add through Manage Project.", "com.vrchat.avatars", "https://creators.vrchat.com/avatars/"),
            new Tool("UdonSharp", "Worlds", "Write world behaviours in C#. Included with the Worlds SDK.", "com.vrchat.worlds", "https://creators.vrchat.com/worlds/udon/udonsharp/"),
            new Tool("ClientSim", "Worlds", "Test supported world behaviours inside Unity. Included with the Worlds SDK.", "com.vrchat.worlds", "https://creators.vrchat.com/worlds/clientsim/") ,
            new Tool("Gesture Manager", "Testing", "By BlackStartx. Preview expressions, gestures and avatar menus in Unity.", "vrchat.blackstartx.gesture-manager", "https://github.com/BlackStartx/VRC-Gesture-Manager", "https://vrchat-community.github.io/vpm-listing-curated/index.json"),
            new Tool("Av3Emulator", "Testing", "By lyuma. Emulate Avatars 3.0 playable layers and parameters in Unity.", "lyuma.av3emulator", "https://github.com/lyuma/Av3Emulator", "https://vrchat-community.github.io/vpm-listing-curated/index.json"),
            new Tool("Pumkin's Avatar Tools", "Avatars", "By rurre. Avatar setup helpers and component copying. Follow the official install guide.", "", "https://github.com/rurre/PumkinsAvatarTools", ""),
            new Tool("lilAvatarUtils", "Avatars", "By lilxyzw. Utilities for avatar modification.", "", "https://github.com/lilxyzw/lilAvatarUtils", "https://lilxyzw.github.io/vpm-repos/vpm.json"),
            new Tool("lilycalInventory", "Avatars", "By lilxyzw. Configure avatar modifications applied at build time.", "", "https://github.com/lilxyzw/lilycalInventory", "https://lilxyzw.github.io/vpm-repos/vpm.json"),
            new Tool("Avatars 3.0 Manager", "Animation", "By VRLabs. Manage playable layers, controller merges and expression parameters.", "dev.vrlabs.av3manager", "https://github.com/VRLabs/Avatars-3.0-Manager", "https://vrchat-community.github.io/vpm-listing-curated/index.json"),
            new Tool("ComboGestureExpressions (archived)", "Animation", "By Hai. Facial expression and gesture authoring. Repository archived; check compatibility before using.", "", "https://github.com/hai-vr/combo-gesture-expressions-av3", ""),
            new Tool("d4rkAvatarOptimizer", "Optimization", "By d4rkc0d3r. Reduce avatar skinned mesh and material count.", "", "https://github.com/d4rkc0d3r/d4rkAvatarOptimizer", ""),
            new Tool("VRCQuestTools", "Mobile", "By kurotu. Support for converting avatars for mobile. Does not automatically improve performance rank.", "", "https://kurotu.github.io/VRCQuestTools/docs/intro/", ""),
            new Tool("EasyQuestSwitch", "Mobile", "By Jordo / VRChat community. Automate platform-specific component changes.", "vrchat.jordo.easyquestswitch", "https://github.com/vrchat-community/EasyQuestSwitch", "https://vrchat-community.github.io/vpm-listing-curated/index.json"),
            new Tool("VRWorld Toolkit", "Worlds", "By oneVR. World diagnostics, optimization checks and build reports.", "dev.onevr.vrworldtoolkit", "https://github.com/oneVR/VRWorldToolkit", "https://vrchat-community.github.io/vpm-listing-curated/index.json"),
            new Tool("AudioLink", "Audio / lighting", "By the AudioLink contributors. Audio-reactive data for world and avatar shaders.", "com.llealloo.audiolink", "https://github.com/llealloo/audiolink", "https://vrchat-community.github.io/vpm-listing-curated/index.json"),
            new Tool("LTCGI", "Audio / lighting", "By PiMaker. Real-time area lighting for Unity and VRChat.", "", "https://ltcgi.dev/", ""),
            new Tool("VR Stage Lighting", "Audio / lighting", "By AcChosen. Stage-lighting shaders, prefabs and world scripts.", "", "https://github.com/AcChosen/VR-Stage-Lighting", ""),
            new Tool("USharpVideo", "Worlds", "By MerlinVR. World video player supporting video and live streams.", "", "https://github.com/MerlinVR/USharpVideo", "")
        };
        readonly Dictionary<string,string> packages = new Dictionary<string,string>();
        VisualElement content;
        string category="All tools", query="";
        GameObject target;
        string lastReport="Run a check to create a report.";
        const string Pref="Crystal.VRCToolkit.Favorite.";

        [MenuItem("Tools/Crystal VRC Toolkit/Open Hub")]
        public static void Open() { var w=GetWindow<CrystalToolkit>(); w.titleContent=new GUIContent("Crystal Toolkit"); w.minSize=new Vector2(640,480); }

        public void CreateGUI()
        {
            rootVisualElement.Clear();
            var script=MonoScript.FromScriptableObject(this);
            var path=AssetDatabase.GetAssetPath(script);
            var sheet=AssetDatabase.LoadAssetAtPath<StyleSheet>(System.IO.Path.GetDirectoryName(path)+"/Toolkit.uss");
            if(sheet!=null) rootVisualElement.styleSheets.Add(sheet);
            rootVisualElement.AddToClassList("crystal-root");
            var header=new VisualElement(); header.AddToClassList("header");
            header.Add(Label("CRYSTAL", "brand")); header.Add(Label("VRC TOOLKIT", "subtitle"));
            header.Add(Label("Your creator workspace, together.", "muted")); rootVisualElement.Add(header);
            var body=new VisualElement(); body.AddToClassList("body"); rootVisualElement.Add(body);
            var nav=new ScrollView(); nav.AddToClassList("nav"); body.Add(nav);
            foreach(var name in new[]{"All tools","Favorites","Avatars","Testing","Animation","Optimization","Shaders","Mobile","Worlds","Audio / lighting","Materials","Blendshapes","Project checks","Quick actions","Setup"})
            {
                string tab=name; var b=new Button(()=>{category=tab;Render();}){text=tab}; b.AddToClassList("nav-button"); nav.Add(b);
            }
            nav.Add(Label("v1.1.0 • 23 tools", "muted"));
            content=new VisualElement(); content.AddToClassList("content"); body.Add(content);
            RefreshPackages(); Render();
        }
        static Label Label(string text, string cls) {var l=new Label(text);l.AddToClassList(cls);return l;}
        static Button Action(string text, Action action) {var b=new Button(action){text=text};b.AddToClassList("action");return b;}
        void RefreshPackages()
        {
            packages.Clear();
            foreach(var p in UnityEditor.PackageManager.PackageInfo.GetAllRegisteredPackages()) packages[p.name]=p.version;
        }
        bool Favorite(Tool t) {return EditorPrefs.GetBool(Pref+t.Name,false);}
        void Render()
        {
            if(content==null)return;
            content.Clear(); content.Add(Label(category,"page-title"));
            if(category=="Materials"){Materials();return;}
            if(category=="Blendshapes"){Blendshapes();return;}
            if(category=="Project checks"){Checks();return;}
            if(category=="Quick actions"){QuickActions();return;}
            if(category=="Setup"){Setup();return;}
            var search=new TextField(){value=query};search.label="Search";
            search.RegisterValueChangedCallback(e=>{query=e.newValue;RenderCards();});content.Add(search);
            content.Add(Action("Refresh installed packages",()=>{RefreshPackages();RenderCards();}));
            var scroll=new ScrollView();scroll.name="cards-scroll";content.Add(scroll);RenderCards();
        }
        void RenderCards()
        {
            var scroll=content.Q<ScrollView>("cards-scroll");if(scroll==null)return;scroll.Clear();
            var grid=new VisualElement();grid.AddToClassList("grid");scroll.Add(grid);int count=0;
            foreach(var t in Tools)
            {
                if(category=="Favorites"&&!Favorite(t))continue;
                if(category!="All tools"&&category!="Favorites"&&category!=t.Category)continue;
                if(!((t.Name+" "+t.Description+" "+t.Category).IndexOf(query,StringComparison.OrdinalIgnoreCase)>=0))continue;
                count++; var card=new VisualElement();card.AddToClassList("card");grid.Add(card);
                card.Add(Label(t.Category.ToUpperInvariant(),"tag"));card.Add(Label(t.Name,"card-title"));card.Add(Label(t.Description,"description"));
                string version;
                string status="See official install page";
                if(t.Package!="")status=packages.TryGetValue(t.Package,out version)?"Installed • "+version:"Not detected";
                card.Add(Label(status,"status"));
                var row=new VisualElement();row.AddToClassList("actions");card.Add(row);
                row.Add(Action("Docs / install",()=>Application.OpenURL(t.Docs)));
                row.Add(Action(Favorite(t)?"★ Saved":"☆ Save",()=>{EditorPrefs.SetBool(Pref+t.Name,!Favorite(t));RenderCards();}));
                if(t.Repo!="")
                {
                    card.Add(Action("Add repo in VCC / ALCOM",()=>Application.OpenURL("vcc://vpm/addRepo?url="+Uri.EscapeDataString(t.Repo))));
                    card.Add(Action("Copy repository URL",()=>{EditorGUIUtility.systemCopyBuffer=t.Repo;ShowNotification(new GUIContent("Repository URL copied"));}));
                }
            }
            if(count==0)scroll.Add(Label("No matching tools. Try another search or save a favorite.","muted"));
        }

        void RootPicker()
        {
            var field=new UnityEditor.UIElements.ObjectField("Avatar / object root"){objectType=typeof(GameObject),allowSceneObjects=true,value=target};
            field.RegisterValueChangedCallback(e=>{target=e.newValue as GameObject;Render();});content.Add(field);
            content.Add(Action("Use selected object",()=>{target=Selection.activeGameObject;Render();}));
        }
        static void MarkChanged(Renderer r)
        {
            EditorUtility.SetDirty(r);
            PrefabUtility.RecordPrefabInstancePropertyModifications(r);
            if(r.gameObject.scene.IsValid()) UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(r.gameObject.scene);
        }
        bool EditableTarget()
        {
            return target!=null&&!EditorUtility.IsPersistent(target)&&!EditorApplication.isPlaying;
        }
        void Materials()
        {
            RootPicker();
            content.Add(Label("Material slots in the chosen hierarchy. Replacements edit renderer assignments, not the material asset. Ctrl+Z undoes changes. Editing requires a scene object outside Play Mode.","description"));
            if(target==null)return;
            var scroll=new ScrollView();content.Add(scroll);
            foreach(var renderer in target.GetComponentsInChildren<Renderer>(true))
            {
                var r=renderer;var card=new VisualElement();card.AddToClassList("card");scroll.Add(card);
                card.Add(Label(ObjectPath(r.transform),"card-title"));
                card.Add(Action("Select object",()=>Selection.activeGameObject=r.gameObject));
                var mats=r.sharedMaterials;
                for(int i=0;i<mats.Length;i++)
                {
                    int slot=i;var field=new UnityEditor.UIElements.ObjectField("Slot "+slot){objectType=typeof(Material),allowSceneObjects=false,value=mats[i]};
                    field.SetEnabled(EditableTarget());
                    field.RegisterValueChangedCallback(e=>{
                        if(r==null||!EditableTarget())return;
                        var current=r.sharedMaterials;if(slot>=current.Length)return;
                        Undo.RecordObject(r,"Crystal Replace Material");current[slot]=e.newValue as Material;r.sharedMaterials=current;MarkChanged(r);
                    });card.Add(field);
                    if(mats[i]!=null)card.Add(Label("Shader: "+(mats[i].shader!=null?mats[i].shader.name:"Missing"),"muted"));
                }
            }
        }
        void Blendshapes()
        {
            RootPicker();
            content.Add(Label("Adjust blendshape weights on scene renderers. Ctrl+Z undoes changes. This does not create animation clips or expression menus. Sliders show 0–100; existing weights outside this range are shown in the label.","description"));
            if(target==null)return;
            var scroll=new ScrollView();content.Add(scroll);int count=0;
            foreach(var renderer in target.GetComponentsInChildren<SkinnedMeshRenderer>(true))
            {
                var r=renderer;var mesh=r.sharedMesh;if(mesh==null||mesh.blendShapeCount==0)continue;
                var fold=new Foldout(){text=ObjectPath(r.transform),value=false};scroll.Add(fold);
                for(int i=0;i<mesh.blendShapeCount;i++)
                {
                    count++;int index=i;float original=r.GetBlendShapeWeight(i);
                    var slider=new Slider(mesh.GetBlendShapeName(i)+" ("+original.ToString("0.##")+")",0,100){value=original,showInputField=true};
                    slider.SetEnabled(EditableTarget());
                    slider.RegisterValueChangedCallback(e=>{
                        if(r==null||!EditableTarget()||r.sharedMesh!=mesh)return;
                        Undo.RecordObject(r,"Crystal Adjust Blendshape");r.SetBlendShapeWeight(index,e.newValue);MarkChanged(r);
                    });fold.Add(slider);
                }
            }
            if(count==0)scroll.Add(Label("No blendshapes found under this root.","muted"));
        }

        void Setup()
        {
            content.Add(Label("1. Add a tool's repository using its card.\n2. In VCC or ALCOM, open your project and manage packages.\n3. Add the package and let the manager resolve its dependencies.\n4. Return to Unity and refresh the installed-package list.","description"));
            content.Add(Label("If the Add repo button does not launch your manager, use Copy repository URL and add it in the manager's repository settings.\n\nThis hub links to official tools; it does not bundle or silently install third-party assets. Favorites are stored on this computer.\n\nUse Project checks on an avatar root or a scene object. Checks inspect the selected hierarchy only, including inactive children. Results are basic Unity diagnostics, not VRChat upload or Quest certification.","description"));
            content.Add(Action("VCC documentation",()=>Application.OpenURL("https://vcc.docs.vrchat.com/")));
        }
        void QuickActions()
        {
            content.Add(Action("Open VRChat SDK panel",()=>Menu("VRChat SDK/Show Control Panel")));
            content.Add(Action("Open ClientSim settings",()=>Menu("VRChat SDK/Utilities/ClientSim")));
            content.Add(Action("Open Package Manager",()=>Menu("Window/Package Manager")));
            content.Add(Action("Open Console",()=>Menu("Window/General/Console")));
            content.Add(Action("Select project Packages folder",()=>{var asset=AssetDatabase.LoadMainAssetAtPath("Packages");if(asset)EditorGUIUtility.PingObject(asset);else ShowNotification(new GUIContent("Open Packages in the Project window"));}));
            content.Add(Label("Shortcuts depend on the relevant tools being installed. Third-party tools keep their own menus and interfaces.","muted"));
        }
        void Menu(string path) {if(!EditorApplication.ExecuteMenuItem(path))ShowNotification(new GUIContent("Menu unavailable. Check installation and Console."));}
        void Checks()
        {
            var field=new UnityEditor.UIElements.ObjectField("Avatar / object root"){objectType=typeof(GameObject),allowSceneObjects=true,value=target};
            field.RegisterValueChangedCallback(e=>target=e.newValue as GameObject);content.Add(field);
            content.Add(Action("Use selected object",()=>{target=Selection.activeGameObject;Render();}));
            content.Add(Action("Run checks",()=>{lastReport=Scan(target);Render();}));
            content.Add(Action("Copy report",()=>EditorGUIUtility.systemCopyBuffer=lastReport));
            var scroll=new ScrollView();content.Add(scroll);scroll.Add(Label(lastReport,"report"));
        }
        static string ObjectPath(Transform t)
        {var parts=new List<string>();while(t!=null){parts.Add(t.name);t=t.parent;}parts.Reverse();return string.Join("/",parts);}
        public static string Scan(GameObject root)
        {
            if(root==null)return "Select an avatar or scene object first.";
            var report=new StringBuilder();int issues=0,missing=0,materials=0,renderers=0;long triangles=0;var meshes=new HashSet<Mesh>();
            report.AppendLine("CRYSTAL VRC TOOLKIT — SELECTED HIERARCHY REPORT");report.AppendLine("Unity: "+Application.unityVersion);report.AppendLine("Root: "+ObjectPath(root.transform));
            foreach(var tr in root.GetComponentsInChildren<Transform>(true))
            {
                int n=GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(tr.gameObject);
                if(n>0){missing+=n;issues++;report.AppendLine("Missing scripts ("+n+"): "+ObjectPath(tr));}
            }
            foreach(var r in root.GetComponentsInChildren<Renderer>(true))
            {
                renderers++;
                foreach(var mat in r.sharedMaterials)
                {
                    if(mat==null){materials++;issues++;report.AppendLine("Empty material slot: "+ObjectPath(r.transform));}
                    else if(mat.shader==null||mat.shader.name=="Hidden/InternalErrorShader") {issues++;report.AppendLine("Missing/error shader: "+mat.name+" on "+ObjectPath(r.transform));}
                }
                var skin=r as SkinnedMeshRenderer;var filter=r.GetComponent<MeshFilter>();var mesh=skin!=null?skin.sharedMesh:(filter!=null?filter.sharedMesh:null);
                if((skin!=null||r is MeshRenderer)&&mesh==null){issues++;report.AppendLine("Missing mesh: "+ObjectPath(r.transform));}
                if(mesh!=null)
                {
                    meshes.Add(mesh);
                    for(int i=0;i<mesh.subMeshCount;i++)if(mesh.GetTopology(i)==MeshTopology.Triangles)triangles+=(long)mesh.GetIndexCount(i)/3;
                }
            }
            var animator=root.GetComponent<Animator>();
            if(animator==null)report.AppendLine("Note: no Animator on this root (may be expected for props/world objects).");
            else if(animator.avatar==null) {issues++;report.AppendLine("Animator has no Avatar asset. Check model rig setup.");}
            else if(!animator.avatar.isValid){issues++;report.AppendLine("Animator Avatar asset is invalid.");}
            report.AppendLine();report.AppendLine("Findings: "+issues+" • Missing scripts: "+missing+" • Empty material slots: "+materials);
            report.AppendLine("Renderers: "+renderers+" • Unique meshes: "+meshes.Count+" • Triangle instances: "+triangles);
            report.AppendLine("Counts include inactive children; repeated mesh instances are counted per renderer. This is not an official VRChat performance rank.");
            if(issues==0)report.AppendLine("No problems found by these checks. Run VRChat SDK validation before uploading.");
            return report.ToString();
        }
    }
}
