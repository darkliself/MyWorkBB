//§§168130738704165742 "Fax Machines" "Alex K."
FaxProductTypeFunc();
MemoryTransmissionReception();
PrintSpeedResolutionColorDuplexing();
InputLoadingFeatureInputCapacity();

// --[FEATURE #1]
// -- Product Type, Integrated Functionality, Modem & Transmission Speed
void FaxProductTypeFunc() {
    var speed = GetReferenceBase("SP-23948");
    var type = GetReferenceBase("SP-18058");
 
    if (A[4386].HasValue("fax") && A[4386].HasValue("copier", "printer", "scanner") &&  type.HasValue() && speed.HasValue()) {
        Add($"FaxProductTypeFunc⸮{type} fax machine with {A[4386].Where("copier", "printer", "scanner").Flatten("/").Replace("printer", "print", "scanner", "scan", "copier", "copy")} functionality and a {speed}Kbps fax modem for quick transmission of files");
    }
    else if (A[4386].HasValue("fax") && A[4386].HasValue("copier", "printer", "scanner") &&  type.HasValue() ) {
        Add($"FaxProductTypeFunc⸮{type} fax machine with {A[4386].Where("copier", "printer", "scanner").Flatten("/").Replace("printer", "print", "scanner", "scan", "copier", "copy")} functionality and fax modem for quick transmission of files");
    }
    else if (type.HasValue() && speed.HasValue()) {
        Add($"FaxProductTypeFunc⸮{type} fax machine and a {speed}Kbps fax modem for quick transmission of files");
    }
    else if (type.HasValue()) {
        Add($"FaxProductTypeFunc⸮{type} fax machine and fax modem for quick transmission of files");
    }
    else if (A[4386].HasValue("fax") && A[4386].HasValue("copier", "printer", "scanner")) {
        Add($"FaxProductTypeFunc⸮Fax machine with {A[4386].Where("copier", "printer", "scanner").Flatten("/").Replace("printer", "print", "scanner", "scan", "copier", "copy")} functionality and fax modem for quick transmission of files");
    } 
}
// --[FEATURE #2]
// -- Memory (Transmission & Reception)
void MemoryTransmissionReception() {
    if (A[2747].HasValue() ) {
        Add($"MemoryTransmissionReception⸮Faxing memory stores up to {A[2747].FirstValueOrDefault()} pages");
    }
    else  if (A[2748].HasValue() ) {
        Add($"MemoryTransmissionReception⸮Faxing memory stores up to {A[2748].FirstValueOrDefault()} pages");
    }
}
// --[FEATURE #3]
// -- Output (Print Speed, Resolution, Color & Duplexing Capability)
void PrintSpeedResolutionColorDuplexing() {
    if (A[2760].HasValue() && A[2761].HasValue()) {
        Add($"PrintSpeedResolutionColorDuplexing⸮Features a printing speed of {A[2760].FirstValueOrDefault()} pages per minute in black and {A[2761].FirstValueOrDefault()} pages per minute in color");
    }
    else  if (!A[2760].HasValue() && A[2761].HasValue() && A[5425].HasValue("yes")) {
        Add($"PrintSpeedResolutionColorDuplexing⸮Features a printing speed of ##{A[2761].FirstValueOrDefault()} pages per minute in color with automatic duplexing");
    }
    else if (!A[2760].HasValue() && A[2761].HasValue() ) {
        Add($"PrintSpeedResolutionColorDuplexing⸮Features a printing speed of ##{A[2761].FirstValueOrDefault()} pages per minute in color");
    }
    else if (A[2760].HasValue() && !A[2761].HasValue() && A[5425].HasValue("yes")) {
        Add($"PrintSpeedResolutionColorDuplexing⸮Features a printing speed of ##{A[2760].FirstValueOrDefault()} pages per minute in black with automatic duplexing");
    }
    else if (A[2760].HasValue() && !A[2761].HasValue()) {
        Add($"PrintSpeedResolutionColorDuplexing⸮Features a printing speed of ##{A[2760].FirstValueOrDefault()} pages per minute in black");
    }
    else if (A[3172].HasValue() && A[3172].Units.First().Name == "ppm") {
         Add($"PrintSpeedResolutionColorDuplexing⸮Features a printing speed of ##{A[3172].FirstValueOrDefault()} pages per minute");
    }
}

// --[FEATURE #4]
// -- Input loading feature, Input Capacity (standard & ADF) & Size
void InputLoadingFeatureInputCapacity() {
    if (A[2731].HasValue() && A[2725].HasValue() && A[6838].HasValue()) {
        Add($"InputLoadingFeatureInputCapacity⸮Accomodates {A[6838].FirstValueOrDefault()}-size pages in the " +
            $"{A[2731].FirstValueOrDefault()}-{A[2731].Units.First().Name.Replace("sheets", "sheet")} input tray and " +
            $"{A[2725].FirstValueOrDefault()}-{A[2725].Units.First().Name.Replace("sheets", "sheet")} auto document feeder");
    }
    else if (A[2731].HasValue() && A[6838].HasValue()) {
        Add($"InputLoadingFeatureInputCapacity⸮Accomodates {A[6838].FirstValueOrDefault()}-size pages in the " +
            $"{A[2731].FirstValueOrDefault()}-{A[2731].Units.First().Name.Replace("sheets", "sheet")} input tray");
    }
    else if (A[2725].HasValue() && A[6838].HasValue()) {
        Add($"InputLoadingFeatureInputCapacity⸮Accomodates {A[6838].FirstValueOrDefault()}-size pages in the " +
            $"{A[2725].FirstValueOrDefault()}-{A[2725].Units.First().Name.Replace("sheets", "sheet")} auto document feeder");
    }
}

// --[FEATURE #5]
// -- Dimensions

// --[FEATURE #6]
// -- Display (Type & Size) & Indicators (includes Caller ID, telephone directory/capacity)

// --[FEATURE #7]
// -- Broadcast Capacity (if applicable)

// --[FEATURE #8]
// -- Connectivity (No. of Ports & Interface)(includes wireless)

// --[FEATURE #9]
// -- Copy Functionality (if applicable)

// --[FEATURE #10]
// -- Scanning Capabilities (includes technology/software, resolution & output color) if applicable

// --[FEATURE #11]
// -- Calling Capabilities (includes answering machine feature) if applicable

// --[FEATURE #12]
// -- Additional

// --[FEATURE #13]
// -- Warranty
//§§168130738704165742 end of "Fax Machines"