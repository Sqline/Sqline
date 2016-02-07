1. If changes were made to Sqline.ClientFramework:
   1.1 Edit Sqline.VSPackage\Packages\Sqline.ClientFramework.dll.nuspec
   1.2 Update version number
   1.2 Update release notes
   1.3 Run Sqline.VSPackage\Packages\nuget-build.bat
   1.4 Update package version number in Sqline.VSPackage\Assets\Sqline.Project\Sqline.vstemplate
   1.5 Create new zip file Sqline.Project.zip and move to Sqline.VSPackage\Assets
2. Edit Sqline.VSPackage\source.extension.vsixmanifest
   2.1 Update Identity.Version
   2.2 Update version number of Sqline.ClientFramework Asset to match 1.2
3. In Sqline.VSPackage Project
   3.1 Add the newly generated nuget package under Packages to the project (and remove previous package)
   3.2 Ensure it has Build Action = Content and Include in VSIX = true
4. Create Release build
5. Copy Sqline.VSPackage\bin\Release\Sqline.VSPackage.vsix to Releases\Sqline-v{version}.vsix
6. Test vsix package in VM
7. Update Nuget Package https://www.nuget.org/packages/Sqline.ClientFramework
8. Update Sqline Visual Studio Gallery package: https://visualstudiogallery.msdn.microsoft.com/b1cd40cf-fe23-4017-a552-c5502d876905