import hashlib,json,os,pathlib,zipfile
root=pathlib.Path(__file__).resolve().parents[1]
repository=os.environ['GITHUB_REPOSITORY']
owner,name=repository.split('/')
manifest=json.loads((root/'Package/package.json').read_text())
version=manifest['version'];package=manifest['name']
asset=f'{package}-{version}.zip'
manifest['url']=f'https://github.com/{repository}/releases/download/v{version}/{asset}'
manifest['author']={'name':'Crystal','email':'269330001+y2kbeatzz-dot@users.noreply.github.com','url':f'https://github.com/{owner}'}
manifest['repo']=f'https://raw.githubusercontent.com/{repository}/vpm/index.json'
output=root/'dist';output.mkdir(exist_ok=True)
with zipfile.ZipFile(output/asset,'w',zipfile.ZIP_DEFLATED) as archive:
 for path in sorted((root/'Package').rglob('*')):
  if path.is_file():
   rel=str(path.relative_to(root/'Package'))
   data=json.dumps(manifest,indent=2).encode() if rel=='package.json' else path.read_bytes()
   info=zipfile.ZipInfo(rel,date_time=(2026,1,1,0,0,0));info.compress_type=zipfile.ZIP_DEFLATED;info.external_attr=0o644<<16
   archive.writestr(info,data)
manifest['zipSHA256']=hashlib.sha256((output/asset).read_bytes()).hexdigest()
existing=root/'previous-index.json'
listing=json.loads(existing.read_text()) if existing.exists() else {'name':'Crystal VRC Toolkit','id':'dev.crystal.vrc-toolkit.repository','author':manifest['author'],'url':manifest['repo'],'packages':{}}
versions=listing['packages'].setdefault(package,{'versions':{}})['versions']
if version in versions and versions[version].get('zipSHA256')!=manifest['zipSHA256']:
 raise SystemExit('This version already exists with different bytes. Bump Package/package.json version first.')
versions[version]=manifest
(output/'index.json').write_text(json.dumps(listing,indent=2)+'\n')
with open(os.environ.get('GITHUB_OUTPUT',str(output/'build-output.txt')),'a') as f:
 f.write(f'version={version}\nasset={asset}\n')
print('Built',asset,'and validated version/checksum history.')
