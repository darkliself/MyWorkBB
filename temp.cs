//§§5435445243161518, 5435445243142044, 5435445243167883, 543544524340300 "Printers BEGIN" "Serhii.O"

PrintersTypeOfPrinterUse();
PrintersPrintTechnologyResolution();
PrintersPrintSpeedMode();
PrintersConnectivity();
// Dimensions - All
PrintersInputLoadingFeature();
PrintersProcessorMemory();
AutomaticPrintingFeatures();
PrintersDisplay();
PrintersScanningCapabilities();
// Standards - All
LargeFormatPrinter();
// Warranty Information - All

// --[FEATURE #1]
// --Type of Printer & Use
void PrintersTypeOfPrinterUse(){
	var result = "";
	// Single-Function|All-in-One|
	var TypeOfPrinterRef = R("SP-13860").HasValue() ? R("SP-13860").Replace("<NULL>", "").Text :
		R("cnet_common_SP-13860").HasValue() ? R("cnet_common_SP-13860").Replace("<NULL>", "").Text : "";

	if(!String.IsNullOrEmpty(TypeOfPrinterRef)){
		switch(TypeOfPrinterRef){
			case "All-in-One":
			result = "All-in-one printer gives you printing, copying, and scanning capability";
			break;
			case "Single-Function":
			result = "This single-function printer focuses exclusively on printing, so it is easy to use";
			break;
			case "Wide/Large Format":
			result = "Wide and large format printer to accommodate a range of printing projects";
			break;
		}
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrintersTypeOfPrinterUse⸮{result}");
	}
}

// --[FEATURE #2]
// --Print Technology & Resolution
void PrintersPrintTechnologyResolution(){
	var result = "";
	// Dot Matrix|Borderless|Laser|Inkjet
	var PrintTechnologyRef = R("SP-13581").HasValue() ? R("SP-13581").Replace("<NULL>", "").Text :
		R("cnet_common_SP-13581").HasValue() ? R("cnet_common_SP-13581").Replace("<NULL>", "").Text : "";
	var Printing_MaxResolutionBW_Color = Coalesce(A[2758], A[2759]); // Printing - Max Resolution B/W | Max Resolution Color

	if(!String.IsNullOrEmpty(PrintTechnologyRef)){
		switch(PrintTechnologyRef){
			case var a when a.Equals("Laser"):
			result = Printing_MaxResolutionBW_Color.HasValue() ?
			$"Laser printing with up to {Printing_MaxResolutionBW_Color.FirstValue()} resolution ensures detailed, high-quality prints" :
			"Invest in a laser printer that will keep your business running smoothly";
			break;
			case var a when a.Equals("Inkjet"):
			result = Printing_MaxResolutionBW_Color.HasValue() ?
			$"Inkjet printer has a resolution quality that goes up to {Printing_MaxResolutionBW_Color.FirstValue()} for excellent readability" :
			"Simplify your office printing solutions with this inkjet printer";
			break;
			case var a when a.Equals("Dot Matrix"):
			result = Printing_MaxResolutionBW_Color.HasValue() ?
			$"Dot matrix printer creates documents up to {Printing_MaxResolutionBW_Color.FirstValue()} for sharp resolution" :
			"Simplify your office printing solutions with this dot matrix printer";
			break;
			case var a when a.Equals("Borderless"):
			result = Printing_MaxResolutionBW_Color.HasValue() ?
			$"Borderless printer creates documents up to {Printing_MaxResolutionBW_Color.FirstValue()} for sharp resolution" :
			"Simplify your office printing solutions with this borderless printer";
			break;
			case var a when a.Equals("PageWide"):
			result = Printing_MaxResolutionBW_Color.HasValue() ?
			$"PageWide printer has a resolution quality that goes up to {Printing_MaxResolutionBW_Color.FirstValue()} for excellent readability" :
			"Simplify your office printing solutions with this PageWide";
			break;
		}
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrintersPrintTechnologyResolution⸮{result}");
	}
}

// --[FEATURE #3]
// --Print Speed & Mode (simplex/duplex), Output Color & Monthly Duty Cycle
void PrintersPrintSpeedMode(){
	var result = "";
	var MaximumPrinterMonthlyDutyCycleRef = R("SP-4540").HasValue() ? R("SP-4540").Replace("<NULL>", "").Text :
		R("cnet_common_SP-4540").HasValue() ? R("cnet_common_SP-4540").Replace("<NULL>", "").Text : "";
	var OfficeMachine_MonthlyDutyCycle = A[2694]; // Office Machine - Monthly Duty Cycle (max)
	var Printing_MaxPrintingSpeed = A[2760]; // Printing - Max Printing Speed B/W (ppm)
	var Printing_MaxPrintingSpeedColor = A[2761]; // Printing - Max Printing Speed Color (ppm)
	var Printing_MaxPrintingSpeed2 = A[5435]; // Printing - Max Printing Speed B/W
	var Printing_MaxPrintingSpeedColor2 = A[5436]; // Printing - Max Printing Speed Color
	var DutyCycle = OfficeMachine_MonthlyDutyCycle.HasValue() ? $" with a monthly duty cycle of {OfficeMachine_MonthlyDutyCycle.FirstValue()} {OfficeMachine_MonthlyDutyCycle.Units.First().Name.Replace("page", "pages").Replace("sheet", "sheets")}" : "";

	if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeed.HasValue()
	&& Printing_MaxPrintingSpeedColor.HasValue()
	&& Printing_MaxPrintingSpeed.HasValue(Printing_MaxPrintingSpeedColor.FirstValue().ToString())){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()}ppm for color and black/white images{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeed.HasValue()
	&& Printing_MaxPrintingSpeedColor.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()}ppm in black/white and ##{Printing_MaxPrintingSpeedColor.FirstValue()}ppm in color{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeed.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()}ppm in black/white{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeedColor.HasValue()){
		result = $"##{Printing_MaxPrintingSpeedColor.FirstValue()}ppm in color{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeed2.HasValue()
	&& Printing_MaxPrintingSpeedColor2.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed2.FirstValue()} {Printing_MaxPrintingSpeed2.Units.First().Name} in black/white and ##{Printing_MaxPrintingSpeedColor2.FirstValue()} {Printing_MaxPrintingSpeedColor2.Units.First().Name} in color{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeed2.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed2.FirstValue()} {Printing_MaxPrintingSpeed2.Units.First().Name} in black/white{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeedColor2.HasValue()){
		result = $"##{Printing_MaxPrintingSpeedColor2.FirstValue()} {Printing_MaxPrintingSpeedColor2.Units.First().Name} in color{DutyCycle}";
	}
	else if(Printing_MaxPrintingSpeed.HasValue()
	&& Printing_MaxPrintingSpeedColor.HasValue()
	&& Printing_MaxPrintingSpeed.HasValue(Printing_MaxPrintingSpeedColor.FirstValue().ToString())){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()}ppm for color and black/white images";
	}
	else if(Printing_MaxPrintingSpeed.HasValue()
	&& Printing_MaxPrintingSpeedColor.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()}ppm in black/white and ##{Printing_MaxPrintingSpeedColor.FirstValue()}ppm in color";
	}
	else if(Printing_MaxPrintingSpeed.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()}ppm in black/white";
	}
	else if(Printing_MaxPrintingSpeedColor.HasValue()){
		result = $"Has a printing speed of ##{Printing_MaxPrintingSpeedColor.FirstValue()}ppm in color";
	}
	else if(Printing_MaxPrintingSpeed2.HasValue()
	&& Printing_MaxPrintingSpeedColor2.HasValue()){
		result = $"Has a printing speed of ##{Printing_MaxPrintingSpeed2.FirstValue()} {Printing_MaxPrintingSpeed2.Units.First().Name} in black/white and ##{Printing_MaxPrintingSpeedColor2.FirstValue()} {Printing_MaxPrintingSpeedColor2.Units.First().Name} in color";
	}
	else if(Printing_MaxPrintingSpeed2.HasValue()){
		result = $"Has a printing speed of ##{Printing_MaxPrintingSpeed2.FirstValue()} {Printing_MaxPrintingSpeed2.Units.First().Name} in black/white";
	}
	else if(Printing_MaxPrintingSpeedColor2.HasValue()){
		result = $"Has a printing speed of ##{Printing_MaxPrintingSpeedColor2.FirstValue()} {Printing_MaxPrintingSpeedColor2.Units.First().Name} in color";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrintersPrintSpeedMode⸮{result}");
	}
}

// --[FEATURE #4]
// --Connectivity (No. of Ports & Interface); Includes wireless
void PrintersConnectivity(){
	var result = "";
	var Connectivity_Interface = A[2703]; // Connectivity - Interface
	var InterfaceRequired_Type = A[6836]; // Interface Required - Type

	if(Connectivity_Interface.HasValue()
	&& Connectivity_Interface.Where("%Wi-Fi%").Any()
	&& InterfaceRequired_Type.HasValue()){
		result = $@"{InterfaceRequired_Type.Values.Match(23, 6836).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2 x $1")).Flatten(", ").Replace("<>", "##1")} and Wi-Fi connectivity help to enhance productivity";
	}
	else if(Connectivity_Interface.HasValue()
	&& Connectivity_Interface.WhereNot("%Bluetooth%").Any()
	&& Connectivity_Interface.Where("%Wi-Fi%").Any()
	&& Connectivity_Interface.Values.Count() > 1){
		result = $@"{InterfaceRequired_Type.WhereNot("%Wi-Fi%").Match(23, 6836).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2 x $1")).Flatten(", ").Replace("<>", "##1")} and Wi-Fi connectivity help to enhance productivity";
	}
	else if(Connectivity_Interface.HasValue()
	&& Connectivity_Interface.WhereNot("%Bluetooth%").Any()
	&& Connectivity_Interface.Where("%LAN%").Any()
	&& InterfaceRequired_Type.HasValue()){
	    var Lan = $"{InterfaceRequired_Type.Where("%lan%").Match(23, 6836).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2")).First()} x".Replace("## x", "##1 x");
		result = $@"Network-ready wired {Lan} {Connectivity_Interface.Where("%LAN%").First().Value()} Ethernet and {InterfaceRequired_Type.WhereNot("%LAN%").Match(23, 6836).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2 x $1")).Flatten(", ").RegexReplace(@"(## x)", "##1 x")} connectivity";
	}
	else if(Connectivity_Interface.HasValue()
	&& Connectivity_Interface.WhereNot("%Bluetooth%").Any()
	&& Connectivity_Interface.Where("%LAN%").Any()){
		result = $@"Network-ready wired {Connectivity_Interface.Where("%LAN%").First().Value()} Ethernet and {Connectivity_Interface.WhereNot("%LAN%").Flatten(", ")} connectivity";
	}
	else if(InterfaceRequired_Type.HasValue()){
		var Bluetooth = Connectivity_Interface.HasValue() && Connectivity_Interface.Where("%Bluetooth%").Any() ? $", and {Connectivity_Interface.Where("%Bluetooth%").First().Value()} interface" : "";
		result = $@"Printer features {InterfaceRequired_Type.Values.Match(23, 6836).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2 x $1")).Flatten(", ").Replace("<>", "##1")}{Bluetooth}";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrintersConnectivity⸮{result}");
	}
}

// --[FEATURE #5] - All
// --Dimensions (in Inches): H x W x D

// --[FEATURE #6]
// --Input loading feature, Input capacity & Media type supported (includes ink cartridges)
void PrintersInputLoadingFeature(){
	var result = "";
	var PaperInputCapacityRef = R("SP-4547").HasValue() ? R("SP-4547").Replace("<NULL>", "").Text :
		R("cnet_common_SP-4547").HasValue() ? R("cnet_common_SP-4547").Replace("<NULL>", "").Text : "";
	var Printer_MediaType = A[193]; // Printer - Media Type
	var DocumentMediaHandlingDetails_Type = A[2710]; // Document & Media Handling Details - Type
	var DocumentMediaHandling_SupportedMediaType = A[2730]; // Document & Media Handling - Supported Media Type
	var TotalMediaCapacity  = A[443]; // Total Media Capacity
	var MediaCapacity = TotalMediaCapacity.HasValue() ? $"{TotalMediaCapacity.FirstValue()} {TotalMediaCapacity.Units.First().Name.Replace("page", "pages").Replace("sheet", "sheets").Replace("sheetss", "sheets")}" : "";

	if(!String.IsNullOrEmpty(PaperInputCapacityRef)
	&& Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).HasValue()
	&& DocumentMediaHandlingDetails_Type.HasValue("bypass tray")
	&& DocumentMediaHandlingDetails_Type.Where("bypass tray").Match(2712).Values().First().In("%1%")
	&& !String.IsNullOrEmpty(MediaCapacity)){
		result = $"{MediaCapacity} input capacity and a single-sheet bypass tray; types of media supported include {Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).Values.Select(s => s.Value()).FlattenWithAnd(10, ",").TrimEnd(",")}";
	}
	else if(!String.IsNullOrEmpty(PaperInputCapacityRef)
	&& DocumentMediaHandlingDetails_Type.HasValue("bypass tray")
	&& DocumentMediaHandlingDetails_Type.Where("bypass tray").Match(2712).Values().First().In("%1%")
	&& !String.IsNullOrEmpty(MediaCapacity)){
		result = $"{MediaCapacity} input capacity lessens the need for constant reloading; single sheet bypass tray";
	}
	else if(!String.IsNullOrEmpty(PaperInputCapacityRef)
	&& Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).HasValue()
	&& !String.IsNullOrEmpty(MediaCapacity)){
		result = $"{MediaCapacity} input capacity; types of media supported include {Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).Values.Select(s => s.Value()).FlattenWithAnd(10, ",").TrimEnd(",")}";
	}
	else if(!String.IsNullOrEmpty(PaperInputCapacityRef)
	&& !String.IsNullOrEmpty(MediaCapacity)){
		result = $"{MediaCapacity} input capacity lessens the need for constant reloading";
	}
	else if(Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).HasValue()){
		result = $"Types of media supported include {Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).Values.Select(s => s.Value()).FlattenWithAnd(10, ",").TrimEnd(",")}";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrintersInputLoadingFeature⸮{result}");
	}
}

// --[FEATURE #7]
// --Printer processor & printer/fax memory
void PrintersProcessorMemory(){
	var result = "";
	var PrinterProcessorRef = R("SP-5129").HasValue() ? R("SP-5129").Replace("<NULL>", "").Text :
		R("cnet_common_SP-5129").HasValue() ? R("cnet_common_SP-5129").Replace("<NULL>", "").Text : "";
	var PrinterMemoryRef = R("SP-4539").HasValue() ? R("SP-4539").Replace("<NULL>", "").Text :
		R("cnet_common_SP-4539").HasValue() ? R("cnet_common_SP-4539").Replace("<NULL>", "").Text : "";
	var FaxMachine_TotalMemoryCapacity = A[2747]; // Fax Machine - Total Memory Capacity

	if(!String.IsNullOrEmpty(PrinterProcessorRef)
	&& !String.IsNullOrEmpty(PrinterMemoryRef)
	&& FaxMachine_TotalMemoryCapacity.HasValue()){
		result = $"{PrinterProcessorRef} processor, {PrinterMemoryRef} printer memory and fax memory of up to {FaxMachine_TotalMemoryCapacity.FirstValue()} pages provides convenient, reliable printing of your most important documents";
	}
	else if(!String.IsNullOrEmpty(PrinterProcessorRef)
	&& !String.IsNullOrEmpty(PrinterMemoryRef)){
		result = $"{PrinterProcessorRef} processor and {PrinterMemoryRef.Split(":").Last()} of memory ensure speed and accuracy";
	}
	else if(!String.IsNullOrEmpty(PrinterProcessorRef)
	&& FaxMachine_TotalMemoryCapacity.HasValue()){
		result = $"{PrinterProcessorRef} processor and fax memory of up to {FaxMachine_TotalMemoryCapacity.FirstValue()} pages provides convenient, reliable printing of your most important documents";
	}
	else if(!String.IsNullOrEmpty(PrinterMemoryRef)
	&& FaxMachine_TotalMemoryCapacity.HasValue()){
	    var MemoryRef = PrinterMemoryRef.Split(" ").ToList();
        var Memory = MemoryRef.Select(s => MemoryRef.IndexOf(s) == 1 ? s.ToLower() : s).Flatten(" ");
		result = $"{Memory}, up to {FaxMachine_TotalMemoryCapacity.FirstValue()} pages fax memory";
	}
	else if(!String.IsNullOrEmpty(PrinterProcessorRef)){
		result = $"Processor speed is {PrinterProcessorRef} to handle large printing jobs";
	}
	else if(!String.IsNullOrEmpty(PrinterMemoryRef)){
	    var MemoryRef = PrinterMemoryRef.Split(" ").ToList();
        var Memory = MemoryRef.Select(s => MemoryRef.IndexOf(s) == 1 ? s.ToLower() : s).Flatten(" ");
		result = $"{Memory} printer memory provides convenient, reliable printing of your most important documents";
	}
	else if(FaxMachine_TotalMemoryCapacity.HasValue()){
		result = $"The fax memory accommodates up to {FaxMachine_TotalMemoryCapacity.FirstValue()} pages for snag-free operation";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrintersProcessorMemory⸮{result}");
	}
}

// --[FEATURE #8]
// --Automatic Printing Features; including duplex printing
void AutomaticPrintingFeatures(){
	var result = "";
	// Automatic|Manual
	var DuplexPrintingRef = R("SP-13862").HasValue() ? R("SP-13862").Replace("<NULL>", "").Text :
		R("cnet_common_SP-13862").HasValue() ? R("cnet_common_SP-13862").Replace("<NULL>", "").Text : "";
	var DocumentFeederCapacityRef = R("SP-350275").HasValue() ? R("SP-350275").Replace("<NULL>", "").Text :
		R("cnet_common_SP-350275").HasValue() ? R("cnet_common_SP-350275").Replace("<NULL>", "").Text : "";
	var DocumentMediaHandlingDetails_Type = A[2710]; // Document & Media Handling Details - Type
	var Copying_AutomaticDuplexing = A[2778]; // Copying - Automatic Duplexing
	var Printing_AutomaticDuplexing = A[5425]; // Printing - Automatic Duplexing
	var Scanning_AutomaticDuplexing = A[5738]; // Scanning - Automatic Duplexing
	var Details_Type = DocumentMediaHandlingDetails_Type.HasValue()
	&& DocumentMediaHandlingDetails_Type.Where("ADF").Any()
	&& A[2725].HasValue()
	&& A[2725].HasValue(DocumentMediaHandlingDetails_Type.Where("ADF").Match(2712).Values().Select(s => s.RegexReplace(@"(.*)\s(\d*).*", "$2")).First().ToString()) ?
	$" with {A[2725].FirstValue()} {A[2725].Units.First().Name} auto feeder" : "";

	if(Copying_AutomaticDuplexing.HasValue("yes")
	&& Printing_AutomaticDuplexing.HasValue("yes")
	&& Scanning_AutomaticDuplexing.HasValue("yes")){
		result = $"Automatic duplexing for effortless two-sided copying, scanning, and printing{Details_Type}";
	}
	else if(Copying_AutomaticDuplexing.HasValue("yes")
	&& Printing_AutomaticDuplexing.HasValue("yes")){
		result = $"Automatic two-sided printing and copying helps save paper{Details_Type}";
	}
	else if(Copying_AutomaticDuplexing.HasValue("yes")){
		result = $"Automatic two-sided duplexing enables hands-free two-sided copying{Details_Type}";
	}
	else if(Printing_AutomaticDuplexing.HasValue("yes")){
		result = $"Help save paper and create two-sided documents with automatic duplex printing{Details_Type}";
	}
	else if(Scanning_AutomaticDuplexing.HasValue("yes")
	&& Copying_AutomaticDuplexing.HasValue("yes")){
		result = $"Save time with single pass duplex scanning and copying, by scanning both sides of 2-Sided documents simultaneously{Details_Type}";
	}
	else if(Scanning_AutomaticDuplexing.HasValue("yes")){
		result = $"Single-pass duplex scanning allows fast multi-page scanning{Details_Type}";
	}
	else if(!String.IsNullOrEmpty(DocumentFeederCapacityRef)){
		result = $"{SKU.ProductType} with automatic duplex printing and {DocumentFeederCapacityRef} sheets auto feeder";
	}
	else if(!String.IsNullOrEmpty(DuplexPrintingRef)){
		result = $"{SKU.ProductType} with automatic duplex printing";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"AutomaticPrintingFeatures⸮{result}");
	}
}

// --[FEATURE #9]
// --Display, Indicators, and Controls (Button, Touch Screen, LCD, etc)
void PrintersDisplay(){
	var result = "";
	var PrinterDisplayRef = R("SP-4562").HasValue() ? R("SP-4562").Replace("<NULL>", "").Text :
		R("cnet_common_SP-4562").HasValue() ? R("cnet_common_SP-4562").Replace("<NULL>", "").Text : "";
	var Display_Features = A[5299]; // Display - Features
	var Chassis_BuiltInDevices = A[2239]; // Chassis - Built-In Devices

	if(!String.IsNullOrEmpty(PrinterDisplayRef)
	&& Display_Features.HasValue("touch screen")){
		result = $"Easily manage tasks with the {PrinterDisplayRef.Replace("touch screen", "")} touchscreen";
	}
	else if(!String.IsNullOrEmpty(PrinterDisplayRef)
	&& Coalesce(PrinterDisplayRef).ExtractNumbers().Any()
	&& Coalesce(PrinterDisplayRef).ExtractNumbers().First() >= 2){
		result = $"{PrinterDisplayRef.Replace("Five lines", "Five-line").Replace("touch screen", "touchscreen")} display enables easy setup and navigation";
	}
	else if(Display_Features.HasValue("touch screen")){
		result = "Easily manage your print jobs with the touch screen display";
	}
	else if(Chassis_BuiltInDevices.HasValue()
	&& Chassis_BuiltInDevices.Where("%touch screen%", "%display%").Any()){
		var BuiltInDevices = Chassis_BuiltInDevices.HasValue("control panel") ? $" and {Chassis_BuiltInDevices.FirstValue()}" : "";
		result = $"Easily manage your print jobs with the {Chassis_BuiltInDevices.Where("%touch screen%").First().Value().Replace("touch screen", "touchscreen").Replace(" inch", @"""").Replace(" display", "")} display{BuiltInDevices}";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrintersDisplay⸮{result}");
	}
}

// --[FEATURE #10]
// --Scanning capabilities (includes scanner type, technology/software, resolution & output color)
void PrintersScanningCapabilities(){
	var result = "";
	var ScanResolutionRef = R("SP-4557").HasValue() ? R("SP-4557").Replace("<NULL>", "").Text :
		R("cnet_common_SP-4557").HasValue() ? R("cnet_common_SP-4557").Replace("<NULL>", "").Text : "";
	var Scanning_OpticalResolution = A[2754]; // Scanning - Optical Resolution
	var InterfaceRequired_Type = A[2755]; // Interface Required - Type
	var Scanning_ColorDepth = A[2756]; // Scanning - Color Depth
	var Scanning_GrayscaleDepth = A[2757]; // Scanning - Grayscale Depth
	var Scanning_ColorDepthInternal = A[4724]; // Scanning - Color Depth (Internal)
	var OpticalResolution = Scanning_OpticalResolution.HasValue() && InterfaceRequired_Type.HasValue() ? 
	    $"{Scanning_OpticalResolution.FirstValue()} optical resolution, " : Scanning_OpticalResolution.HasValue() ?
	    $"{Scanning_OpticalResolution.FirstValue()} optical resolution " : "";
	var Required_Type = InterfaceRequired_Type.HasValue() ? $"{InterfaceRequired_Type.FirstValue()} interpolated resolution" : "";
	var ColorDepth = Scanning_ColorDepth.HasValue() ? $"{Scanning_ColorDepth.FirstValue()} color depth, " : "";
	var GrayscaleDepth = Scanning_GrayscaleDepth.HasValue() ? $"{Scanning_GrayscaleDepth.FirstValue()} grayscale depth " : "";

	if(Scanning_GrayscaleDepth.HasValue()){		
		result = $"{OpticalResolution}{Required_Type}{ColorDepth}{GrayscaleDepth}for good quality printed text ##and images";
	}
	else if(Scanning_ColorDepth.HasValue()){
		result = $"{OpticalResolution}{Required_Type}{ColorDepth}for good quality printed text ##and images";
	}
	else if(InterfaceRequired_Type.HasValue()){
		result = $"{OpticalResolution}{Required_Type}for good quality printed text ##and images";
	}
	else if(Scanning_OpticalResolution.HasValue()){
		result = $"{OpticalResolution}for good quality printed text and images";
	}
	else if(!String.IsNullOrEmpty(ScanResolutionRef)
	&& (Scanning_ColorDepth.HasValue() || Scanning_ColorDepthInternal.HasValue())){
		result = $"Color scanner captures resolutions up to {ScanResolutionRef} to capture every detail";
	}
	else if(!String.IsNullOrEmpty(ScanResolutionRef)){
		result = $"Scanner captures resolutions up to {ScanResolutionRef} to capture every detail";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrintersScanningCapabilities⸮{result}");
	}
}

// --[FEATURE #11] - All
// --Certification and Standards

// --[FEATURE #12] - All
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void LargeFormatPrinter(){
    var result = "";
    var DocumentMediaHandling_LargeFormatPrinterSize = A[4919]; // Document & Media Handling - Large Format Printer Size
    
    if(DocumentMediaHandling_LargeFormatPrinterSize.HasValue()){
        result = $"{DocumentMediaHandling_LargeFormatPrinterSize.FirstValue()} large-format printer is equipped to satisfy many diverse needs of architects, engineers, constructors, GIS and other professional";
    }
    if(!String.IsNullOrEmpty(result)){
	    Add($"LargeFormatPrinter⸮{result}");
	}
}

// --[FEATURE #13] - All
// --Warranty Information. NOTE: If REFURBISHED, then “Warranty Length Non-mfg. warranty through Vendor. This product is NOT warrantied through Brand Name”

//5435445243161518, §§5435445243142044, §§5435445243167883, §§543544524340300 "Printers END" "Serhii.O" §§