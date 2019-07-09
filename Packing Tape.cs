//§§558512579643140631 "Packing Tape" "Alex K."

// Dimensions
ThicknessInMillimeters();
PackingTapeTypeAndUse();
// True color
PackingTapeLabelAdhesive();
// PackSize
TensileStrengthRating();
// Certifications
PackingTapeHeavyDuty();
PackingTapeTearBreakResistant();
PackingTapeFrustrationFree();

// --[FEATURE #1]
// -- Dimensions: Width (in Inches) x Length (in Yards); Width is how wide is the tape and Length is the total length on the roll

// --[FEATURE #2]
// -- Thickness (millimeters)
void ThicknessInMillimeters(){
    var thickness = GetReferenceBase("SP-22042");
    if (thickness.HasValue()){
        Add($"ThicknessInMillimeters⸮Thickness: {thickness} mm");
    }
}
// --[FEATURE #3]
// -- Packing tape type & Use
void  PackingTapeTypeAndUse(){
    // Pre-Printed Message|Paper Tape|Reinforced|PVC/Bag Sealing|Acrylic|Standard|Cellophane|Water Activated|Carton Sealing|Strapping/Filament|Flatback|Fashion|Moving|Label Protection|Hot Melt
    var type = GetReferenceBase("SP-21708");
    if (type.HasValue("Acrylic")) {
        Add($"PackingTapeTypeAndUse⸮Acrylic tape designed for moving and storage needs");
    }
    else if (type.HasValue("Carton Sealing")) {
        Add($"PackingTapeTypeAndUse⸮Carton sealing tape adheres instantly to most surfaces including cartons");
    }
    else if (type.HasValue("Cellophane")) {
        Add($"PackingTapeTypeAndUse⸮Cellophane tape offers excellent adhesion, dispensing and handling properties");
    }
    else if (type.HasValue("Flatback")) {
        Add($"PackingTapeTypeAndUse⸮Flatback tape is designed to minimize carton-sealing failures and prevent pilferage");
    }
    else if (type.HasValue("Hot Melt")) {
        Add($"PackingTapeTypeAndUse⸮Hot melt adhesive tape holds strong for your shipping needs");
    }
    else if (type.HasValue("Label Protection")) {
        Add($"PackingTapeTypeAndUse⸮Provides excellent label protection");
    }
    else if (type.HasValue("Paper Tape")) {
        Add($"PackingTapeTypeAndUse⸮Paper tape provides secure closure for inner packing and lightweight packaging");
    }
    else if (type.HasValue("Pre-Printed Message")) {
        Add($"PackingTapeTypeAndUse⸮Pre-printed message");
    }
    else if (type.HasValue("PVC/Bag Sealing")) {
        Add($"PackingTapeTypeAndUse⸮PVC/Bag Sealing tape offers excellent adhesion, dispensing and handling properties");
    }
    else if (type.HasValue("Reinforced")) {
        Add($"PackingTapeTypeAndUse⸮Reinforced strapping tape is ideal for heavy tasks and bundling");
    }
    else if (type.HasValue("Standard")) {
        Add($"PackingTapeTypeAndUse⸮Standard tape offers excellent adhesion, dispensing and handling properties");
    }
    else if (type.HasValue("Strapping/Filament")) {
        Add($"PackingTapeTypeAndUse⸮Strapping/Filament tape offers excellent adhesion, dispensing and handling properties");
    }
    else if (type.HasValue("Water Activated")) {
        Add($"PackingTapeTypeAndUse⸮Water activated adhesive bonds to corrugated even in dusty conditions");
    }
    else if (type.HasValue("Moving")) {
        Add($"PackingTapeTypeAndUse⸮Moving tape offers excellent adhesion, dispensing and handling properties");
    }
    else if (type.HasValue("Cellophane")) {
        Add($"PackingTapeTypeAndUse⸮Cellophane tape offers excellent adhesion, dispensing and handling properties");
    }
}
// --[FEATURE #4]
// -- True color

// --[FEATURE #5]
// -- Label adhesive
void  PackingTapeLabelAdhesive(){
    // Removable|Permanent|Permanent Self-Adhesive|Non-Adhesive
    var adhesiveType = GetReferenceBase("SP-2779");
    if (adhesiveType.HasValue("Non-Adhesive")) {
        Add($"PackingTapeLabelAdhesive⸮Non-Adhesive");
    } 
    else if (adhesiveType.HasValue("Permanent") && A[6449].HasValue("%kraft%")) {
        Add($"PackingTapeLabelAdhesive⸮Kraft tape is made for permanent adhesion");
    }
    else if (adhesiveType.HasValue("Permanent")) {
        Add($"PackingTapeLabelAdhesive⸮Tape is made for permanent adhesion");
    }
    else if (adhesiveType.HasValue("Permanent Self-Adhesive")) {
        Add($"PackingTapeLabelAdhesive⸮Permanent self-adhesive");
    }
    else if (adhesiveType.HasValue("Removable")) {
        Add($"PackingTapeLabelAdhesive⸮Removable");
    }
}
// --[FEATURE #6]
// -- Pack Size (If more than 1)

// --[FEATURE #7]
// -- Tensile strength rating (If Applicable)
void  TensileStrengthRating(){
    // Removable|Permanent|Permanent Self-Adhesive|Non-Adhesive
    var tensileStrength = GetReferenceBase("SP-22050");
    if (tensileStrength.HasValue() && !tensileStrength.HasValue("0")) {
        Add($"TensileStrengthRating⸮{tensileStrength} lbs. of tensile strength per inch of width");
    }
}
// --[FEATURE #8]
// -- Certifications (If Applicable)

// --[FEATURE #9]
// -- Additional heavy-duty
void  PackingTapeHeavyDuty(){
    if (A[6457].HasValue("heavy-duty")) {
        Add($"PackingTapeHeavyDuty⸮Heavy-duty tape for tough jobs");
    }
}
// --[FEATURE #10]
// -- Additional tear-resistant
void  PackingTapeTearBreakResistant(){
    if (A[6457].HasValue("tear-resistant") && A[6457].HasValue("break-resistant")) {
        Add($"PackingTapeTearBreakResistant⸮Resists breaking or tearing even if nicked or punctured during handling");
    }
}
// --[FEATURE #11]
// -- Additional Frustration Free
void  PackingTapeFrustrationFree(){
    if (A[6457].HasValue("Frustration Free")) {
        Add($"PackingTapeFrustrationFree⸮Frustration-Free technology");
    }
}

//§§558512579643140631 end of "Packing Tape"