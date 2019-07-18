//§§5881382810783158854  "Safes & Secure Storage" "Alex K."
SafeTypeAndUse();
MaterialSafeLockingMechanism();
CapacityOrVolume();
// Dimensions
SafeInteriorDimentions();
SafeRatings();
// PrinterPartsWeight -- All

SafeMountMethod();
SafeFloorMat();
SafeCarpetedInterior();
SafeDropDrawer();
SafeElectronicLockKey();
// standards
SafeResistantOrAnti();

// --[FEATURE #1]
// -- Safe type & Use
void SafeTypeAndUse(){
    var type = GetReferenceBase("SP-14228");
    if (type.HasValue("Fireproof")){
        Add($"SafeTypeAndUse⸮This safe is designed to be fire-resistant");
    }
    else if (type.HasValue("Book")){
        Add($"SafeTypeAndUse⸮Looks like a real book and will blend seamlessly into your bookshelf");
    }
    else if (type.HasValue("Picture")){
        Add($"SafeTypeAndUse⸮This safe looks and functions as a standard photo frame while concealing the pressence of a safe");
    }
    else if (type.HasValue("Box")){
        Add($"SafeTypeAndUse⸮Protect your valuables with the security box");
    }
    else if (type.HasValue("Gun")){
        Add($"SafeTypeAndUse⸮Gun safe will protect your guns and keep accidents from terrorizing your life");
    }
    else if (type.HasValue("Key") && A[7281].HasValue()){
        Add($"SafeTypeAndUse⸮{A[7281].FirstValueOrDefault().ToUpperFirstChar()} provides the perfect solution for secure organization of all of your keys");
    }
    else if (type.HasValue("Fire/Waterproof") && A[7281].HasValue()){
        Add($"SafeTypeAndUse⸮{A[7281].FirstValueOrDefault().ToUpperFirstChar()} protects your valuables from fire and flood");
    }
    else if (type.HasValue("Standard")){
        Add($"SafeTypeAndUse⸮Safe provides safety and security for your essential documents and most valuable possessions");
    }
    else if (type.HasValue("Depository")){
        Add($"SafeTypeAndUse⸮Depository safe for short-term storage of deposits, receipts, files");
    }
    else if (type.HasValue()){
        Add($"SafeTypeAndUse⸮{type.ToLower().ToUpperFirstChar()} safe provides safety and security for your essential documents and most valuable possessions");
    }
}
// --[FEATURE #2]
// -- Material of item & Safe locking mechanism
void MaterialSafeLockingMechanism(){
    var materialOfItem = GetReferenceBase("SP-21408");
    var safeLockingMechanism = GetReferenceBase("SP-21043");
    if (materialOfItem.HasValue() && safeLockingMechanism.HasValue()){
        Add($"MaterialSafeLockingMechanism⸮{materialOfItem.ToLower(true).ToUpperFirstChar()} construction with {safeLockingMechanism.Replace("w/", "and ").ToLower()} lock");
    }
    else if (safeLockingMechanism.HasValue()) {
        Add($"MaterialSafeLockingMechanism⸮{safeLockingMechanism.ToLower().ToUpperFirstChar()} lock keeps the contents protected");
    }
    else if (materialOfItem.HasValue()) {
        Add($"MaterialSafeLockingMechanism⸮Made {materialOfItem.ToLower(true)} construction to provide long-lasting strength and use");
    }
}
// --[FEATURE #3]
// -- Capacity or volume (cubic feet)
void CapacityOrVolume(){
    var vol = GetReferenceBase("SP-20646");
    if (vol.HasValue() && !vol.HasValue("0")){
        Add($"CapacityOrVolume⸮Features {vol} cu. ft. interior storage capacity");
    }
}
// --[FEATURE #4]
// -- Overall dimensions (in Inches): H x W x D

// --[FEATURE #5]
// -- Interior dimensions & Configuration (Can be multiple bullets)
void SafeInteriorDimentions(){
    var size = GetReferenceBase("SP-431");
    if (size.HasValue()){
        Add($"SafeInteriorDimentions⸮Interior dimensions: {size}");
    }
}
// --[FEATURE #6]
// -- Safe ratings
void SafeRatings(){
    var rate = GetReferenceBase("SP-24081");
    if (rate.HasValue()){
        Add($"SafeRatings⸮Safe has {rate}");
    }
}
// --[FEATURE #7]
// -- Product weight (lbs.) - All

// --[FEATURE #8]
// -- Delivery Method Dropship; Curbside; Inside Delivery; White Glove; Installed; etc.)
// multi features
// --[FEATURE #9]
// -- Additional Safe Mount Method
void SafeMountMethod(){
    var rate = GetReferenceBase("SP-24081");
    if (A[6082].HasValue("%wall mountable%")){
        Add($"SafeMountMethod⸮Designed to be mounted on walls and other secure foundations");
    }
    else if (A[6082].HasValue("%floor-mounted%")) {
        Add($"SafeMountMethod⸮This floor-mounted safe is easy to install and offers exceptional protection from intruders");
    }
}
// --[FEATURE #10]
// -- Additional floor ma
void SafeFloorMat(){
    if (A[7283].HasValue("%floor mat%")){
        Add($"SafeFloorMat⸮Includes protective floor mat ensuring that items are not scratched");
    }
}
// --[FEATURE #11]
// -- Additional carpeted interior

void SafeCarpetedInterior(){
    if (A[7283].HasValue("%carpeted interior%")){
        Add($"SafeCarpetedInterior⸮Carpeted interior helps prevent scratching to valuables during storage");
    }
}
// --[FEATURE #12]
// -- Additional drop drawer
void SafeDropDrawer(){
    if (A[7297].HasValue("%drop drawer%")){
        Add($"SafeDropDrawer⸮To resist tampering, this safe has a drop drawer with a row of jagged teeth that prevents deposited objects from being fished out");
    }
}
// --[FEATURE #13]
// -- Additional
void SafeElectronicLockKey(){
    if (A[7297].HasValue("%electronic lock%") && A[7297].HasValue("%override key%")){
        Add($"SafeElectronicLockKey⸮Electronic lock with override key lets you choose your combination");
    }
}
// --[FEATURE #14]
// -- Additional resistant, anti
void SafeResistantOrAnti(){
    if (A[7297].HasValue("%resistant%", "%anti%") && !A[7297].HasValue("%fire%", "%water%")){
        Add($"SafeResistantOrAnti⸮{A[7297].Where("%resistant%", "%anti%").Select(o => o.HasValue("%fire%", "%water%") ? "" : o.Value()).FlattenWithAnd().ToUpperFirstChar()} for extra protection");
    }
    else if (A[5810].HasValue("%resistant%", "%anti%") && !A[5810].HasValue("%fire%", "%water%")){
        Add($"SafeResistantOrAnti⸮{A[5810].Where("%resistant%", "%anti%").Select(o => o.HasValue("%fire%", "%water%") ? "" : o.Value()).FlattenWithAnd().ToUpperFirstChar()} for extra protection");
    }
}

// --[FEATURE #16]
// -- Warranty

//§§5881382810783158854 end of "Safes & Secure Storage"