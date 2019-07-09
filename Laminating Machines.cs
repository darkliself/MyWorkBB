//§§168130732148142540  "Laminating Machines" "Alex K."

LaminatingMachineTypeColorCompatibility();
LaminatingSpeedHeatUpTime();
ThroatWidthInput();
// Dimentions
LaminatingMachineFunctionality();
ControlsSettingsIndicators();
LaminatingMachineSafetyFeatures();
LaminatingMachinePowerManagement();
// Maintenance OOS
AdditionalAutoShutOff();
AdditionalPackageIncludes();
// Warranty


// --[FEATURE #1]
// -- Laminating machine type, Color, Compatibility (also for pouches/rolls) & Use
void LaminatingMachineTypeColorCompatibility() {
    var trueColor = GetReferenceBase("SP-22967");
    // Cold|Thermal & Cold|Thermal
    var type = GetReferenceBase("SP-18633");
    // Laminating Material Type 6899
    if (type.HasValue() && trueColor.HasValue() && A[6899].HasValue()) {
        Add($"LaminatingMachineTypeColorCompatibility⸮{trueColor} {type.Replace("&","and").ToLower()} laminator for use with laminating {A[6899].Values.Flatten().Replace("pouch", "pouches", "roll", "rolls")} to protect documents");
    }
    else if (type.HasValue() && trueColor.HasValue()) {
        Add($"LaminatingMachineTypeColorCompatibility⸮{trueColor} {type.Replace("&","and").ToLower()} laminator makes it easy to protect and preserve the important documents");
    }
    else if (type.HasValue() && A[6899].HasValue()) {
        Add($"LaminatingMachineTypeColorCompatibility⸮{type.Replace("&","and").ToLower().ToUpperFirstChar()} laminator for use with laminating {A[6899].Values.Flatten().Replace("pouch", "pouches", "roll", "rolls")} to protect documents");
    }
    else if (type.HasValue()) {
        Add($"LaminatingMachineTypeColorCompatibility⸮{type.Replace("&","and").ToLower().ToUpperFirstChar()} laminator makes it easy to protect and preserve the important documents");
    }
}

// --[FEATURE #2]
// -- Construction (includes no. of rollers), Heat-up Time (includes special tech) & Laminating Speed
void LaminatingSpeedHeatUpTime() {
    var warmUpTime = GetReferenceBase("SP-18633");
    var laminatingSpeed = GetReferenceBase("SP-12674");
    var minPlural = "minutes";
    if (warmUpTime.HasValue("1")) {
        minPlural = "minute";
    }
    if (warmUpTime.HasValue() && laminatingSpeed.HasValue()) {
        Add($"LaminatingSpeedHeatUpTime⸮Provides lamination with {warmUpTime.RegexReplace(@"(^[0-9]{2,}$)", @"$1-").RegexReplace(@"(^[0-9]$)", @"$1 ")}{minPlural} warm-up time and {laminatingSpeed}\" per minute laminating speed");
    }
    else  if (warmUpTime.HasValue()) {
        Add($"LaminatingSpeedHeatUpTime⸮Laminator provides lamination with {warmUpTime.RegexReplace(@"(^[0-9]{2,}$)", @"$1-").RegexReplace(@"(^[0-9]$)", @"$1 ")}{minPlural} warm-up time");
    }
    else if (laminatingSpeed.HasValue()) {
        Add($"LaminatingSpeedHeatUpTime⸮Laminates at variable speeds of up to {laminatingSpeed}\" per minute");
    }
}

// --[FEATURE #3]
// -- Throat Width & Input (Type & Size)
void ThroatWidthInput() {
    var throatWidth = GetReferenceBase("SP-22453");
    var inputWidth = GetReferenceBase("SP-23995");
    
    if (throatWidth.HasValue() && inputWidth.HasValue()) {
        Add($"NumberOfPockets⸮{throatWidth} wide throat supports input up to {inputWidth} wide");
    }
    else if (throatWidth.HasValue()) {
        Add($"NumberOfPockets⸮{throatWidth} wide throat");
    }
    else if (inputWidth.HasValue()) {
        Add($"NumberOfPockets⸮Input up to {inputWidth} wide");
    }
}

// --[FEATURE #4]
// -- Dimensions

// --[FEATURE #5]
// -- Functionality (includes special tech & ADF) & Anti-jam features
void LaminatingMachineFunctionality() {
    var throatWidth = GetReferenceBase("SP-22453");
    var inputWidth = GetReferenceBase("SP-23995");
    
    if (A[4282].HasValue("Flash Heat Technology")) {
        Add($"LaminatingMachineFunctionality⸮Rapid warm-up with Flash-Heat Technology");
    }
    else if (A[4282].HasValue("auto pouch thickness detection")) {
        Add($"LaminatingMachineFunctionality⸮Pouch Thickness Detection automatically determines optimal settings");
    }
    else if (A[4282].HasValue("reverse button")) {
        Add($"LaminatingMachineFunctionality⸮Reverses jam prevention ensures smooth operation");
    }
    else if (A[4282].HasValue("auto reverse function")) {
        Add($"LaminatingMachineFunctionality⸮Auto-reverse mode reverses document flow for easy removal");
    }
    else if (A[4282].HasValue("Anti-jam system")) {
        Add($"LaminatingMachineFunctionality⸮Anti-jam system for efficient laminating");
    }
}

// --[FEATURE #6]
// -- Controls/Settings (includes UI) & Indicators (includes display type)
void  ControlsSettingsIndicators() {
    var tempSettings = GetReferenceBase("SP-1080");
    var CNET_MKT = GetReferenceBase("CNET_MKT");
    
    if (CNET_MKT.HasValue("%Premium design includes easy-to-use LED touch controls and button that turns green when machine is ready%")
    && tempSettings.HasValue() && !tempSettings.HasValue("1")) {
        Add($"ControlsSettingsIndicators⸮{tempSettings.Replace(" settings", "")} temperature settings allow for customized use; premium design includes easy-to-use LED touch controls and button that turns green when machine is ready");
    }
    else if (CNET_MKT.HasValue("%Advanced temperature control ensures heat is carefully monitored for smooth, consistent results every time%")) {
        Add($"ControlsSettingsIndicators⸮Advanced temperature control ensures smooth, consistent results");
    }
}
// --[FEATURE #7]
// -- Design (includes integrated features, storage options & carrier-free operation)
void  LaminatingMachineDesign() {
    var CNET_MKT = GetReferenceBase("CNET_MKT");
    
    if (CNET_MKT.HasValue("%Stylish and ultra compact for storage, it laminates a single document from in under a minute%")) {
        Add($"LaminatingMachineDesign⸮Compact design for easy storage");
    }
    else if (CNET_MKT.HasValue("%Built-in carrying handle and cord wrap allows for neat storage no matter where you go%")) {
        Add($"LaminatingMachineDesign⸮Built-in carrying and cord-wrap handle for easy portability");
    }
    else if (CNET_MKT.HasValue("%Includes hidden built-in cord storage and foldable input tray%")) {
        Add($"LaminatingMachineDesign⸮Hidden built-in cord storage and foldable input tray for easy portability");
    }
    else if (CNET_MKT.HasValue("%Stylish and compact for storage, the Fusion 1000L laminates%")) {
        Add($"LaminatingMachineDesign⸮Compact design for easy storage");
    }
}
// --[FEATURE #8]
// -- Safety Features
void  LaminatingMachineSafetyFeatures() {
    var safety = GetReferenceBase("SP-1128");
    if (safety.HasValue("Yes")) {
        Add($"LaminatingMachineSafetyFeatures⸮For added safety, HeatGuard technology traps heat inside the laminator, so the outside is comfortable to touch");
    }
}
// --[FEATURE #9]
// -- Power Management
void LaminatingMachinePowerManagement() {
    if (A[4236].HasValue() && A[3548].HasValue()) {
        Add($"LaminatingMachinePowerManagement⸮Power: {A[4236].FirstValueOrDefault()} nominal voltage and {A[3548].FirstValueOrDefault()} {A[3548].Units.First().Name} maximum power consumption");
    }
    else if (A[4236].HasValue()) {
        Add($"LaminatingMachinePowerManagement⸮Power: {A[4236].FirstValueOrDefault()} nominal voltage");
    }
    else if (A[3548].HasValue()) {
        Add($"LaminatingMachinePowerManagement⸮{A[3548].FirstValueOrDefault()} {A[3548].Units.First().Name} maximum power consumption");
    }
}
// --[FEATURE #10]
// -- Maintenance OOS

// --[FEATURE #11]
// -- Additional Auto Shut off
void AdditionalAutoShutOff() {
    var autoOff = GetReferenceBase("SP-736");
    if (autoOff.HasValue()) {
        Add($"AdditionalAutoShutOff⸮Auto shut off save energy and prevents overheating");
    }
}

// --[FEATURE #11]
// -- Additional Package Includes
void LaminatingMachineAdditionalPackageIncludes() {
    if (A[9500].HasValue() && A[9500].WhereNot("laminator").Count() > 0) {
        Add($"LaminatingMachineAdditionalPackageIncludes⸮Package includes: {A[9500].WhereNot("laminator").Flatten(", ")}");
    }
}
// --[FEATURE #12]
// -- Warranty

//§§168130732148142540 end of "Laminating Machines"