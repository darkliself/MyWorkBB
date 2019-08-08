//§§1374510010003  "Transparent Tape" "Alex K."
TapeDimensions();
TapeCoreDiameter();
TapeThickness();
TapeTypeAndUse();
// ColorFamily
// PackSize
TapeDispenserIncluded();
TapeTensileStrength();
// Standards
AdditionalTapeInvisible();
AdditionalTapePermanentAdhesive();
AdditionalTapeMatteGlossyFinish();
AdditionalWritablSurface();
// Recycled Content
// Post Consumer content
AdditionalIndoorOutdoorUse();
// Warranty


// --[FEATURE #1]
// -- Dimensions as W (in inches) x L (in yards) where width is how wide is the tape and Length is the total length on the roll.
void TapeDimensions() {
    var width = GetReferenceBase("SP-22046");
    var length = GetReferenceBase("SP-22047");
    if (width.HasValue() && length.HasValue()) {
        Add($"TapeDimensions⸮Tape with dimensions of {width}\"W x {length} yds.");
    }
    else if (width.HasValue()) {
         Add($"TapeDimensions⸮Tape width: {width}\"");
    }
    else if (length.HasValue()) {
         Add($"TapeDimensions⸮Tape length: {length} yds.");
    }
}
// --[FEATURE #2]
// -- Core Diameter (inches)
void TapeCoreDiameter() {
    if (GetReferenceBase("SP-22045").HasValue()) {
        Add($"TapeCoreDiameter⸮Core diameter: {GetReferenceBase("SP-22045")}\"");
    }
}
// --[FEATURE #3]
// -- Mil. Thickness
void TapeThickness() {
    if (A[6445].HasValue() && A[6445].Units.First().Name == "mm") {
        Add($"TapeThickness⸮Tape thickness: {Math.Round(A[6445].FirstValueOrDefault() * 39.3701, 2)} mils");
    }
    else if (A[6445].HasValue() && A[6445].Units.First().Name == "µm") {
        Add($"TapeThickness⸮Tape thickness: {Math.Round(A[6445].FirstValueOrDefault() * 0.0393701, 2)} mils");
    }
}
// --[FEATURE #4]
// -- Type of Tape/Use
void TapeTypeAndUse(){
    var type = GetReferenceBase("SP-18701");
    if (type.HasValue("Double Sided")){
        Add($"TapeTypeAndUse⸮Double sided Tape holds things together without being seen");
    }
    else if (type.HasValue("Masking")){
        Add($"TapeTypeAndUse⸮Masking tape provides instant adhesion and resists lifting or curling");
    }
    else if (type.HasValue("Electrical")){
        Add($"TapeTypeAndUse⸮Electrical tape provides excellent resistant to abrasion, moisture, alkalis, acids and corrosion");
    }
    else if (type.HasValue("Removable")){
        Add($"TapeTypeAndUse⸮Removable tape is great for temporary posting");
    }
    else if (type.HasValue("Transparent")){
        Add($"TapeTypeAndUse⸮Transparent tape is the perfect clear tape for wrapping, sealing, and label protection");
    }
    else if (type.HasValue("Invisible")){
        Add($"TapeTypeAndUse⸮Invisible tape is the perfect option for sealing, mending, and labeling");
    }
    else if (type.HasValue()){
        Add($"TapeTypeAndUse⸮{type.ToLower().ToUpperFirstChar()} tape is great option for labeling and other");
    }
}
// --[FEATURE #5]
// -- Color Family

// --[FEATURE #6]
// -- Tape Pack Size (if more than 1)

// --[FEATURE #7]
// -- Dispenser included (If applicable)
void TapeDispenserIncluded(){
    if (A[6461].HasValue("dispenser") && A[6462].HasValue("desktop")){
        Add($"TapeDispenserIncluded⸮Desktop tape dispenser for keeping tape at hand atop your desk");
    }
    else if (A[6461].HasValue("dispenser") && A[6462].HasValue("hand held")) {
        Add($"TapeDispenserIncluded⸮Dispenser is easy to hold and carry");
    }
    else if (A[6461].HasValue("dispenser")) {
        Add($"TapeDispenserIncluded⸮Includes dispenser for easy use");
    }
}
// --[FEATURE #8]
// -- Tensile strength rating (If Applicable)
void TapeTensileStrength(){
    if (A[6453].HasValue("1") && A[6453].Units.First().NameUSM == "lb/in" ){
        Add($"TapeTensileStrength⸮Desktop tape dispenser for keeping tape at hand atop your desk");
    }
    else if (A[6453].HasValue() && A[6453].Units.First().NameUSM == "lb/in") {
         Add($"TapeTensileStrength⸮Features a tensile strength of {A[6453].FirstValueUsm()} lbs. per inch");
    }
}

// --[FEATURE #9]
// -- Certifications

// --[FEATURE #10]
// -- Additional Invisible
void AdditionalTapeInvisible(){
    if (A[6457].HasValue("%invisible after application%")){
        Add($"AdditionalTapeInvisible⸮Once applied, tape becomes invisible");
    }
}
// --[FEATURE #11]
// -- Additional Permanent adhesive
void AdditionalTapePermanentAdhesive(){
    if (A[6457].HasValue("%permanent adhesive%")){
        Add($"AdditionalTapePermanentAdhesive⸮Strong tape ideal for permanent, secure paper mending");
    }
}

// --[FEATURE #12]
// -- Additional Matte/glossy finish
void AdditionalTapeMatteGlossyFinish(){
    if (A[6457].HasValue("%finish%")){
        Add($"AdditionalTapeMatteGlossyFinish⸮{A[6457].Where("%finish%").Select(o => o.Value()).Flatten(", ").ToUpperFirstChar()} for basic office use");
    }
}
// --[FEATURE #13]
// -- Additional Writable surface
void AdditionalWritablSurface(){
    if (A[6685].HasValue()){
        Add($"AdditionalWritablSurface⸮Can be written on with pen, pencil, or marker");
    }
}

// --[FEATURE #14]
// -- Additional
void AdditionalIndoorOutdoorUse(){
    if (A[6189].HasValue("indoor/outdoor use")){
        Add($"AdditionalIndoorOutdoorUse⸮Can be written on with pen, pencil, or marker");
    }
}

// --[FEATURE #15]
// -- Additional

// --[FEATURE #12]
// -- Additional

// --[FEATURE #12]
// -- Additional

//§§1374510010003  end of "Transparent Tape"