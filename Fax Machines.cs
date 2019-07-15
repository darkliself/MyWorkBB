//§§168130738704165742 "Fax Machines" "Alex K."
FaxProductTypeFunc();
MemoryTransmissionReception();
PrintSpeedResolutionColorDuplexing();
InputLoadingFeatureInputCapacity();
// Dimensions
DisplayAndIndicators();
BroadcastCapacity();
FaxMachineConnectivity();
FaxMachineCopyFunctionality();
FaxMachineScanningCapabilities();
FaxMachineCallingCapabilities();
// Warranty

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
void DisplayAndIndicators() {
    if (A[2780].HasValue() && A[2779].HasValue() && A[5299].HasValue("LCD", "LED")) {
        Add($"DisplayAndIndicators⸮Easy to read {A[2780].FirstValueOrDefault().ExtractNumbers().First()}-character/{A[2779].FirstValueOrDefault().Replace(" lines", "-line")} {A[5299].Where("LCD", "LED").First()} display");
    }
    else if (A[2780].HasValue() && A[2779].HasValue()) {
        Add($"DisplayAndIndicators⸮Easy to read {A[2780].FirstValueOrDefault().ExtractNumbers().First()}-character/{A[2779].FirstValueOrDefault().Replace(" lines", "-line")} display");
    }
    else if (A[2779].HasValue() && A[5299].HasValue("LCD", "LED")) {
        Add($"DisplayAndIndicators⸮Easy to read {A[2779].FirstValueOrDefault().Replace(" lines", "-line")} {A[5299].Where("LCD", "LED").First()} display");
    }
    else if (A[2779].HasValue()) {
        Add($"DisplayAndIndicators⸮Easy to read {A[2779].FirstValueOrDefault().Replace(" lines", "-line")} display");
    }
}
// --[FEATURE #7]
// -- Broadcast Capacity (if applicable)
void BroadcastCapacity() {
    if (GetReferenceBase("SP-64").HasValue()) {
        Add($"BroadcastCapacity⸮Up to {GetReferenceBase("SP-64")} auto dialing");
    }
}
// --[FEATURE #8]
// -- Connectivity (No. of Ports & Interface)(includes wireless)
void FaxMachineConnectivity() {
    if (A[2703].HasValue("%Wi-Fi%") && !A[2703].HasValue("%Bluetooth%") && A[6836].HasValue()) {
        Add($"FaxMachineConnectivity⸮{A[6836].Values.Match(23, 6836).Values(" x ").Flatten(", ")} and ##Wi-Fi connectivity enhance productivity");
    }
    else if (A[2703].HasValue("%Wi-Fi%") && !A[2703].HasValue("%Bluetooth%") && A[2703].Values.Count() > 1) {
        Add($"FaxMachineConnectivity⸮{A[2703].WhereNot("%Wi-Fi%").Flatten(", ")} and ##Wi-Fi connectivity enhance productivity");
    }
    else if (A[2703].HasValue("%LAN%") && A[2703].HasValue("%Bluetooth%") && A[6836].HasValue()) {
        Add($"FaxMachineConnectivity⸮Network-ready wired {A[6836].Where("%lan%").Match(23).Values().First()} x {A[2703].Where("%LAN%").First()} ##Ethernet and {A[6836].WhereNot("%LAN%").Match(23, 6836).Values(" x ").Flatten(", ").Replace("<NULL>", "##1")}  connectivity");
    }
    else if (A[2703].HasValue("%LAN%") && A[2703].HasValue("%Bluetooth%")) {
        Add($"FaxMachineConnectivity⸮Network-ready wired {A[2703].Where("%LAN%").First().Value()} ##Ethernet, {A[2703].WhereNot("%LAN%").FlattenWithAnd()} connectivity");
    }
    else if (A[6836].HasValue() && A[2703].HasValue("%Bluetooth%")) {
        Add($"FaxMachineConnectivity⸮Features {A[6836].Values.Match(23, 6836).Values(" x ").Flatten(", ").Replace("<NULL>", "##1")} and {A[2703].Where("%Bluetooth%").First().Value()} interface");
    }
      else if (A[6836].HasValue()) {
        Add($"FaxMachineConnectivity⸮Features {A[6836].Values.Match(23, 6836).Values(" x ").Flatten(", ").Replace("<NULL>", "##1")} interface");
    }
}
// --[FEATURE #9]
// -- Copy Functionality (if applicable)

void FaxMachineCopyFunctionality() {
    if (A[2763].HasValue()) {
        Add($"FaxMachineCopyFunctionality⸮Copies documents at up to {A[2763].FirstValueOrDefault()} copies per minute so you need one less machine for your office");
    }
    else if (A[2764].HasValue()) {
        Add($"FaxMachineCopyFunctionality⸮Copies documents at up to {A[2764].FirstValueOrDefault()} copies per minute so you need one less machine for your office");
    }
}
// --[FEATURE #10]
// -- Scanning Capabilities (includes technology/software, resolution & output color) if applicable
void FaxMachineScanningCapabilities() {
    if (A[2756].HasValue() && GetReferenceBase("SP-183").HasValue()) {
        Add($"FaxMachineScanningCapabilities⸮This fax machine captures up {GetReferenceBase("SP-183")} resolution in color for efficient archiving and copying");
    }
    else if (A[2757].HasValue() && GetReferenceBase("SP-183").HasValue()) {
        Add($"FaxMachineScanningCapabilities⸮This fax machine captures up {GetReferenceBase("SP-183")} resolution in grayscale for efficient archiving and copying");
    }
    else if (GetReferenceBase("SP-183").HasValue()) {
        Add($"FaxMachineScanningCapabilities⸮This fax machine captures up {GetReferenceBase("SP-183")} resolution for efficient archiving and copying");
    }
}
// --[FEATURE #11]
// -- Calling Capabilities (includes answering machine feature) if applicable
void FaxMachineCallingCapabilities() {
    
    if (SKU.ProductId.HasValue("10903324")) {
        Add($"FaxMachineCallingCapabilities⸮Supports one-touch dialing, speed dialing");
    }
    else if (SKU.ProductId.HasValue("1268015")) {
        Add($"FaxMachineCallingCapabilities⸮Supports caller ##ID, speed dialing, telephone/fax switch, fax forwarding, broadcast transmission, group dialing, and distinctive ring");
    }
    else  if (SKU.ProductId.HasValue("10462012")) {
        Add($"FaxMachineCallingCapabilities⸮Supports one-touch dialing, speed dialing, fax forwarding, and broadcast transmission");
    }
    else  if (SKU.ProductId.HasValue("11134298")) {
        Add($"FaxMachineCallingCapabilities⸮Supports one-touch dialing, speed dialing, telephone/fax switch, fax forwarding, and broadcast transmission");
    }
    else  if (SKU.ProductId.HasValue("5824318")) {
        Add($"FaxMachineCallingCapabilities⸮Supports caller ##ID, broadcast transmission, distinctive ring");
    }
    else  if (SKU.ProductId.HasValue("1268016")) {
        Add($"FaxMachineCallingCapabilities⸮Supports caller ##ID, one-touch dialing, speed dialing, telephone/fax switch, fax forwarding, and broadcast transmission");
    }
    else  if (SKU.ProductId.HasValue("11098412")) {
        Add($"FaxMachineCallingCapabilities⸮Supports one-touch dialing, speed dialing, fax forwarding, broadcast transmission, and group dialing");
    }
    else  if (SKU.ProductId.HasValue("2549299")) {
        Add($"FaxMachineCallingCapabilities⸮Supports caller ##ID, one-touch dialing, speed dialing, telephone/fax switch, fax forwarding, and broadcast transmission");
    }
    else {
        var arr = new List<string>();
        if (A[2737].HasValue()) {
            arr.Add("caller ##ID");
        }
        if (A[2735].HasValue()) {
            arr.Add("one-touch dialing");
        }
        if (A[2736].HasValue()) {
            arr.Add("speed dialing");
        }
        if (A[2738].HasValue()) {
            arr.Add("telephone/fax switch");
        }
        if (A[2743].HasValue("fax forwarding")) {
            arr.Add("fax forwarding");
        }
        if (A[2740].HasValue()) {
            arr.Add("Broadcast Transmission");
        }
        if (A[2743].HasValue("group dialing")) {
            arr.Add("group dialing");
        }
        if (A[2743].HasValue("Distinctive Ring")) {
            arr.Add("Distinctive Ring");
        }
        if (arr.Count() > 0) {
            Add($"FaxMachineCallingCapabilities⸮Supports {arr.FlattenWithAnd()}");
        }
    }
}
// --[FEATURE #13]
// -- Warranty
//§§168130738704165742 end of "Fax Machines"