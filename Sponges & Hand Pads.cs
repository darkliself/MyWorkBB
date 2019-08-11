//§§168339288178142819  "Sponges & Hand Pads" "Alex K."
SpongeHandPadTypeGradeAndUse();
// TrueColorMaterial - All
LevelOfUse();
SuitableSurface();
// Dimensions
ConstructionDesignFeatures();
// Bristle trim length - OOS
// PackSize
AdditionalBPAFree();
AdditionalSetIncludes();
AdditionalBuiltInScourerPad();
AdditionalSoapFilling();
AdditionalSanitizing();
AdditionalSpongeHandPadScent();
// Standard
// RecycledContent
// AdditionalDustMopHandleType - need remove to all

// --[FEATURE #1]
// -- Sponge/hand pad type, Grade & Use
void SpongeHandPadTypeGradeAndUse(){
    var type = GetReferenceBase("SP-18150");
    if (type.HasValue("Scouring Pad")){
        Add($"SpongeHandPadTypeGradeAndUse⸮Scouring pad offers gentle action and cleans the toughest places with ease");
    }
    else if (type.HasValue("Floor Pad")){
        Add($"SpongeHandPadTypeGradeAndUse⸮Floor pad can be used for everyday cleaning, remove light soil, scuff marks, and black heel marks");
    }
    else if (type.HasValue("Steel Wool Pad")){
        Add($"SpongeHandPadTypeGradeAndUse⸮Steel wool pad cuts through grease, rust and soil");
    }
    else if (type.HasValue("Stainless Steel Pad")){
        Add($"SpongeHandPadTypeGradeAndUse⸮Steel wool pad cuts through grease, rust and soil");
    }
    else if (type.HasValue("Scrubber")){
        Add($"SpongeHandPadTypeGradeAndUse⸮Scrubber breaks up of tough dirt with ease");
    }
    else if (type.HasValue("Set")){
        Add($"SpongeHandPadTypeGradeAndUse⸮Set let you tackle the cleaning of large surfaces and tough messe");
    }
    else if (type.HasValue("Pad Holder")){
        Add($"SpongeHandPadTypeGradeAndUse⸮Pad holder is designed to make clean up jobs faster, easier or safer");
    }
    else if (type.HasValue("Polishing Pad")){
        Add($"SpongeHandPadTypeGradeAndUse⸮Polishing pad removes soil and scuff marks with minimal dulling of finish");
    }
    else if (type.HasValue("Stripper")){
        Add($"SpongeHandPadTypeGradeAndUse⸮Stripper removes dirt, spills and scuffs leaving a clean surface ready for recoating");
    }
    else if (type.HasValue("Sponge")){
        Add($"SpongeHandPadTypeGradeAndUse⸮Sponge quickly wipes up spills and messes and can carry cleaning solutions to the work surface");
    }
    else if (type.HasValue()){
        Add($"SpongeHandPadTypeGradeAndUse⸮{type.ToLower().ToUpperFirstChar()} makes your cleaning job easier and more effective");
    }
}
// --[FEATURE #2]
// -- Material & True color (Include handle & bristles if applicable)

// --[FEATURE #3]
// -- Level of use
void LevelOfUse(){
    var use = GetReferenceBase("SP-20642");
    if (use.HasValue("Heavy")){
        Add($"LevelOfUse⸮Heavy-duty for tough cleaning jobs");
    }
    else if (use.HasValue("Medium")){
        Add($"LevelOfUse⸮Medium-duty for everyday general-purpose cleaning jobs");
    }
    else if (use.HasValue("Light")){
        Add($"LevelOfUse⸮Ideal for light duty jobs");
    }
}
// --[FEATURE #4]
// -- Suitable surface
void SuitableSurface(){
    if (A[6783].HasValue()){
        Add($"SuitableSurface⸮Great for cleaning {A[6783].Values.Select(o => o.Value()).FlattenWithAnd().ToLower(true)}");
    }
}
// --[FEATURE #5]
// -- Dimensions (in Inches): H x W x D

// --[FEATURE #6]
// -- Construction/Design Features (Include handle & bristles)
void ConstructionDesignFeatures(){
    if (A[6786].HasValue("%heat-resistant bristles%")) {
        Add($"ConstructionDesignFeatures⸮High heat resistant bristles offer long term economy");
    }
    else if (A[6786].HasValue("%angled bristle%")) {
        Add($"ConstructionDesignFeatures⸮Angled bristle for efficient cleaning");
    }
    else if (A[6786].HasValue("%soft bristle brush%")) {
        Add($"ConstructionDesignFeatures⸮Softer bristles for delicate cleaning of jars, bottles, and glasses");
    }
    else if (A[6786].HasValue("%stiff bristle%") && A[6786].HasValue("%hard bristle brush%")) {
        Add($"ConstructionDesignFeatures⸮Stiff bristles provide aggressive cleaning");
    }
    else if (A[6786].HasValue("%long handle%")) {
        Add($"ConstructionDesignFeatures⸮Long handle is useful for cleaning needs");
    }
    else if (A[6786].HasValue("%heat-resistant handle%")) {
        Add($"ConstructionDesignFeatures⸮Heat-resistant handle helps protect hands from burns, grease and grime");
    }
    else if (A[6786].HasValue("%molded ridges%")) {
        Add($"ConstructionDesignFeatures⸮Comfortable molded ridges");
    }
    else if (A[6786].HasValue("%hanging hole%") && A[6786].HasValue("%plastic handle%")) {
        Add($"ConstructionDesignFeatures⸮Plastic handle with convenient hole for hanging");
    }
    else if (A[6786].HasValue("%flexible handle%") && A[6786].HasValue("%plastic handle%")) {
        Add($"ConstructionDesignFeatures⸮Flexible handle for getting into corners and around curves");
    }
}
// --[FEATURE #7]
// -- Bristle trim length (If Applicable)

// --[FEATURE #8]
// -- Pack qty

// --[FEATURE #9]
// -- Additional BPA-free
void AdditionalBPAFree(){
    if (A[6783].HasValue("BPA-free")) {
        Add($"AdditionalBPAFree⸮BPA-free to address safety concerns");
    }
}

// --[FEATURE #10]
// -- Additional Set includes
void AdditionalSetIncludes(){
    if (A[7094].HasValue() && GetReferenceBase("SP-18150").HasValue("Set")) {
        Add($"AdditionalSetIncludes⸮Set includes: {A[7094].Values.Flatten(",")}");
    }
}
// --[FEATURE #11]
// -- Additional Water activated micro scrubber
void AdditionalWaterActivatedMicroScrubbers(){
    if (DC.KSP.GetString().HasValue("%Water%activated micro%scrubber%")) {
        Add($"AdditionalWaterActivatedMicroScrubbers⸮Water-activated micro-scrubbers reach into surface grooves, lifting away the toughest soils");
    }
}
// --[FEATURE #12]
// -- Additional built-in scourer scouring pad
void AdditionalBuiltInScourerPad(){
    if (A[6786].HasValue("built-in scourer") && A[6786].HasValue("scouring pad")) {
        Add($"AdditionalBuiltInScourerPad⸮One side is a scouring pad for scrubbing and cleaning");
    }
}
// --[FEATURE #13]
// -- Additional
void AdditionalSoapFilling(){
    if (A[6786].HasValue("soap filling")) {
        Add($"AdditionalSoapFilling⸮Pre-loaded with soap to remove tough messes");
    }
}
// --[FEATURE #14]
// -- Additional sanitizing
void AdditionalSanitizing(){
    if (A[7092].HasValue("%dishwasher%")) {
        Add($"AdditionalSanitizing⸮Sanitize in the dishwasher");
    }
    else if (A[6786].HasValue("washable%") && A[7092].HasValue("machine washable")) {
        Add($"AdditionalSanitizing⸮Machine {A[6786].Where("washable%").First().Value()}, reducing waste and creating a more eco-friendly environment");
    }
    else if (A[6786].HasValue("washable%")) {
        Add($"AdditionalSanitizing⸮{A[6786].Where("washable%").First().Value().ToUpperFirstChar()}, reducing waste and creating a more eco-friendly environment");
    }
    else if (A[7092].HasValue("machine washable")) {
        Add($"AdditionalSanitizing⸮Machine washable for multiple uses");
    }
    else if (A[7092].HasValue()) {
        Add($"AdditionalSanitizing⸮Washable for multiple uses");
    }
}
// --[FEATURE #15]
// -- Additional scent
void AdditionalSpongeHandPadScent(){
    if (A[7356].HasValue()) {
        Add($"AdditionalSpongeHandPadScent⸮Leaves {A[7356].Values.Select(o => o.Value()).FlattenWithAnd()} scent");
    }
}

// --[FEATURE #16]
// -- Additional

//§§168339288178142819  end of "Sponges & Hand Pads"