# Crystal VRC Toolkit

A purple-and-teal Unity editor workspace for VRChat creators. Includes 23 tool/source cards, favorites, selected-hierarchy checks, material assignment editing and blendshape controls with Undo.

## Install through VCC or ALCOM

After the first successful Publish VPM Repository workflow run, add:

`https://raw.githubusercontent.com/y2kbeatzz-dot/crystal-vrc-toolkit/vpm/index.json`

Then manage your project and add **Crystal VRC Toolkit**. Open Unity and choose **Tools → Crystal VRC Toolkit → Open Hub**.

For an existing local installation, close Unity and move `Packages/dev.crystal.vrc-toolkit` or `Assets/CrystalVRC-Toolkit` outside the project before adding the managed package. Keep a backup. Do not install both copies.

## First publication

Create a public GitHub repository named `crystal-vrc-toolkit` under `y2kbeatzz-dot`, and commit this folder's contents on `main` (include `.github`). The workflow publishes the package as a GitHub release and the VPM listing on branch `vpm`. GitHub Pages is not required. No deploy keys, external hosting or third-party secrets are needed.

## Updates

Edit the source under `Package`, bump its semantic version in `Package/package.json`, and commit to `main`. Existing versions stay in the feed. Published package bytes cannot be changed without a version bump.

## Status

User confirmed v1.1.0 imports and works in Unity. Package and feed generation are checked locally. Hosted publication must still be verified after creating the repository.

Community tools belong to their authors. This repository distributes only Crystal's hub code and official links, not the third-party tool code. See each tool's official documentation for licensing, dependencies and installation.
