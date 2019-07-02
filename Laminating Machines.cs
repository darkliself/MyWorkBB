//§§168130732148142540  "Laminating Machines" "Alex K."

LaminatingMachineTypeColorCompatibility();
LaminatingSpeedHeatUpTime();

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

// --[FEATURE #4]
// -- Dimensions

// --[FEATURE #5]
// -- Functionality (includes special tech & ADF) & Anti-jam features

// --[FEATURE #6]
// -- Controls/Settings (includes UI) & Indicators (includes display type)

// --[FEATURE #7]
// -- Design (includes integrated features, storage options & carrier-free operation)

// --[FEATURE #8]
// -- Safety Features

// --[FEATURE #9]
// -- Power Management

// --[FEATURE #10]
// -- Maintenance

// --[FEATURE #11]
// --

// --[FEATURE #12]
// -- Warranty

//§§168130732148142540 end of "Laminating Machines"