# Crystal VRC Toolkit — User guide

## 1. Add the repository

[Open the install page](https://crystal-vrc-toolkit-install.xqv5xwnjjq.chatgpt.site) and press **Add to VCC / ALCOM**. Your browser may ask to open the app. Confirm the repository in the app.

If the app does not open, copy this URL into its Add Repository settings:

```text
https://raw.githubusercontent.com/y2kbeatzz-dot/crystal-vrc-toolkit/vpm/index.json
```

The button adds a repository; it does not silently install a package into your project. VCC or ALCOM must be installed and associated with the `vcc://` link protocol.

## 2. Add the toolkit to a project

Close Unity. In VCC / ALCOM, open your project's package-management screen, find **Crystal VRC Toolkit**, and add it. Then open the project in Unity and wait for compilation.

### Already using the ZIP or Unity import version?

Before adding the managed package, move your previous toolkit folder outside the project and keep a backup:

- Embedded install: `Packages/dev.crystal.vrc-toolkit`.
- Unity import install: `Assets/CrystalVRC-Toolkit` (and its adjacent `.meta` file).

Choose the path that exists in your project. Do not remove unrelated packages. Keeping both installations can cause duplicate script errors.

## 3. Open the hub

In Unity choose **Tools → Crystal VRC Toolkit → Open Hub**.

- **All tools:** search resources and open their official install pages.
- **Favorites:** save your most-used resources. Favorites stay on this computer.
- **Materials:** choose a scene avatar/object root to see renderer material slots. Assign replacements using the object fields; Ctrl+Z undoes changes. Shared material assets are not edited.
- **Blendshapes:** choose a scene root, expand a mesh, and adjust its sliders. Ctrl+Z undoes changes. This edits weights, not expression menus or animation clips.
- **Project checks:** select a root, run checks, then copy the report. Checks include inactive children in that hierarchy, not your entire project.
- **Quick actions:** open installed SDK/Unity panels.

Material and blendshape edits require scene objects outside Play Mode. Run the VRChat SDK's own validation before uploading.

## 4. Install linked community tools

Use a card's **Docs / install** link, or **Add repo in VCC / ALCOM** if available. Add that tool separately through your manager, allowing it to resolve dependencies. Return to Unity and choose **Refresh installed packages**. Entries without package detection show their official install page instead. The toolkit does not guarantee every tool works with every project or every other tool.

## 5. Update

Close Unity and use VCC / ALCOM's package-management screen to update Crystal VRC Toolkit when a newer version is listed. Reopen Unity and wait for compilation.

## Troubleshooting

- **Button does nothing:** try Copy repository link on the install page, then add it manually in the manager.
- **Toolkit is not listed:** confirm the repository is added and enabled, and refresh the package list.
- **Unity menu is missing:** check the Console for compile errors, including errors from other packages. [Report an issue](https://github.com/y2kbeatzz-dot/crystal-vrc-toolkit/issues) with your Unity version, toolkit version and the full first error. Avoid posting private project content.
- **Duplicate type errors:** check that the older Assets or embedded installation was moved outside the project.
- **A shortcut is unavailable:** install its relevant SDK/tool first.

## Official tool links

| Tool | Official source |
| --- | --- |
| Modular Avatar | https://modular-avatar.nadena.dev/docs/intro |
| VRCFury | https://vrcfury.com/download/ |
| Avatar Optimizer | https://vpm.anatawa12.com/avatar-optimizer/en/ |
| lilToon | https://lilxyzw.github.io/lilToon/ |
| Poiyomi | https://www.poiyomi.com/ |
| VRChat Avatar SDK | https://creators.vrchat.com/avatars/ |
| UdonSharp | https://creators.vrchat.com/worlds/udon/udonsharp/ |
| ClientSim | https://creators.vrchat.com/worlds/clientsim/ |
| Gesture Manager | https://github.com/BlackStartx/VRC-Gesture-Manager |
| Av3Emulator | https://github.com/lyuma/Av3Emulator |
| Pumkin's Avatar Tools | https://github.com/rurre/PumkinsAvatarTools |
| lilAvatarUtils | https://github.com/lilxyzw/lilAvatarUtils |
| lilycalInventory | https://github.com/lilxyzw/lilycalInventory |
| Avatars 3.0 Manager | https://github.com/VRLabs/Avatars-3.0-Manager |
| ComboGestureExpressions (archived) | https://github.com/hai-vr/combo-gesture-expressions-av3 |
| d4rkAvatarOptimizer | https://github.com/d4rkc0d3r/d4rkAvatarOptimizer |
| VRCQuestTools | https://kurotu.github.io/VRCQuestTools/docs/intro/ |
| EasyQuestSwitch | https://github.com/vrchat-community/EasyQuestSwitch |
| VRWorld Toolkit | https://github.com/oneVR/VRWorldToolkit |
| AudioLink | https://github.com/llealloo/audiolink |
| LTCGI | https://ltcgi.dev/ |
| VR Stage Lighting | https://github.com/AcChosen/VR-Stage-Lighting |
| USharpVideo | https://github.com/MerlinVR/USharpVideo |
