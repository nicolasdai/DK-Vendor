workspace "DK.Vendor"
	architecture "x64"
	
	configurations
	{
		"Debug",
		"Release",
		"Dist"
	}
	
	outputdir = "%{cfg.buildcfg}-%{cfg.system}-%{cfg.architecture}"
	unitytargetdir = "E:/_Sorani_Devspace/Pixel_Coffee_Office/Assets/Plugins/DK"
	
	project "DKLog"
		location "DKLog"
		kind "SharedLib"
		language "C#"
		dotnetframework ("4.7.1")
		
		targetdir ("bin/" ..outputdir.. "/%{prj.name}")
		objdir ("bin-int/" ..outputdir.. "/%{prj.name}")
		
		files
		{
			"%{prj.name}/Properties/**.cs",
			"%{prj.name}/src/**.cs"
		}
		
		links
		{
			"C:/Program Files/Unity/2019.4.13f1c1/Editor/Data/Managed/UnityEngine/UnityEngine.CoreModule.dll"
		}
		
		postbuildcommands
		{
			("{COPY} %{prj.name}.dll \"%{unitytargetdir}\"")
		}
			
		filter "configurations:Debug"
			symbols "Off"
			
		filter "configurations:Release"
			optimize "On"
			
		filter "configurations:Dist"
			optimize "On"