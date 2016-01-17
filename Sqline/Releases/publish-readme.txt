1. If changes were made to Sqline.ClientFramework:
   1.1 Edit Sqline.VSPackage\Packages\Sqline.ClientFramework.dll.nuspec
   1.2 Update version number
   1.2 Update release notes
   1.3 Run Sqline.VSPackage\Packages\nuget-build.bat
   1.4 Update package version number in Sqline.VSPackage\Assets\Sqline.Project\Sqline.vstemplate
2. Edit Sqline.VSPackage\source.extension.vsixmanifest
   2.1 Update Identity.Version
   2.2 Update version number of Sqline.ClientFramework Asset to match 1.2
3. Create Release build
4. Copy Sqline.VSPackage\bin\Release\Sqline.VSPackage.vsix to Releases\Sqline-v{version}.vsix
5. Update Nuget Package https://www.nuget.org/packages/Sqline.ClientFramework
6. Update Sqline Visual Studio Gallery package: https://visualstudiogallery.msdn.microsoft.com/b1cd40cf-fe23-4017-a552-c5502d876905