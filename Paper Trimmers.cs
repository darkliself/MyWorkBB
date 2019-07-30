//§§11036101167637 "Paper Trimmers" "Alex K."
PaperTrimmerTypeUse();
PaperTrimmerSize();
MaximumPaperSize();
BladeLengthMaterial();
FeaturesGridsLocksGuards();
// TrueColor
// PackSize
AdditionalSelfSharpeningBlade();
AdditionalTitaniumBlades();
AdditionalAutoClamSystem();
AdditionalUsedForCutting();
AdditionalPaperTrimmersFeatures();

// --[FEATURE #1]
// -- Paper trimmer type & Use
void PaperTrimmerTypeUse() {
    // Guillotine|Rotary|Cutter / Trimmer Accessories|Replacement Blades|Cutting Mat|Replacement Blade|Roll Cutter|Craft Trimmers|Craft Trimmer|Crimper|Cutter/Trimmer Accessory|Cutting Mats|Roll Cutters|Accessories|Crimpers
    var type = GetReferenceBase("SP-14421");
    if (type.HasValue("guillotine") || type.HasValue("rotary")) {
        Add($"PaperTrimmerTypeUse⸮All paper crafting enthusiasts know that {type.ToLower()} trimmer is a must-have");
    }
    else if (type.HasValue("Cutting Mats")) {
        Add($"PaperTrimmerTypeUse⸮All paper crafting enthusiasts know that cutting mat is a must-have");
    }
    else if (type.HasValue("Replacement Blades")) {
        Add($"PaperTrimmerTypeUse⸮All paper crafting enthusiasts know that {type.ToLower()} are must-have");
    }
    else if (type.HasValue()) {
        Add($"PaperTrimmerTypeUse⸮All paper crafting enthusiasts know that {type.ToLower()} is must-have");
    }
}
// --[FEATURE #2]
// -- Paper trimmer size (inches)
void PaperTrimmerSize() {
    var trimmerSize = GetReferenceBase("SP-18664");
    if (trimmerSize.HasValue()) {
        Add($"PaperTrimmerSize⸮Paper trimmer size: {trimmerSize}");
    }
}
// --[FEATURE #3]
// -- Maximum Paper Size
void MaximumPaperSize() {
    var size = GetReferenceBase("SP-13859");
    if (size.HasValue()) {
        Add($"MaximumPaperSize⸮trimmer сuts sheets up to {size} size");
    }
}
// --[FEATURE #4]
// -- Blade length & Material
void BladeLengthMaterial() {
    var bladeLength = GetReferenceBase("SP-21137");
    var knifeBladeMaterial = GetReferenceBase("SP-12951");
    if (bladeLength.HasValue() && knifeBladeMaterial.HasValue()) {
        Add($"BladeLengthMaterial⸮{bladeLength}\" {knifeBladeMaterial.ToLower(true)} blade");
    }
    else if (bladeLength.HasValue()) {
        Add($"BladeLengthMaterial⸮Blade length: {bladeLength}\"");
    }
    else if (knifeBladeMaterial.HasValue()) {
        Add($"BladeLengthMaterial⸮Blade is made of {knifeBladeMaterial.ToLower(true)}");
    }
}
// --[FEATURE #5]
// -- Features (Grids/Locks/Guards)
void FeaturesGridsLocksGuards() {
    if (A[4315].HasValue("safety interlock")) {
        Add($"FeaturesGridsLocksGuards⸮Safety interlock secures blade when not in use");
    }
    else if (A[4315].HasValue("automatic blade locking system") && A[6703].HasValue("Yes")) {
        Add($"FeaturesGridsLocksGuards⸮Safety features include finger guard ##and automatic blade locking system");
    }
    else if (A[4315].HasValue("automatic blade locking system")) {
        Add($"FeaturesGridsLocksGuards⸮Features automatic blade locking system");
    }
}
// --[FEATURE #6]
// -- True color

// --[FEATURE #7]
// -- Pack Size (If more than 1)

// --[FEATURE #8]
// -- Additional self-sharpening blade
void AdditionalSelfSharpeningBlade() {
    if (A[4315].HasValue("self-sharpening blade")) {
        Add($"AdditionalSelfSharpeningBlade⸮Self-sharpening system keeps blade sharp");
    }
}
// --[FEATURE #9]
// -- Additional titanium coated blades
void AdditionalTitaniumBlades() {
    if (A[4315].HasValue("titanium coated blades")) {
        Add($"AdditionalTitaniumBlades⸮Titanium-bonded blades stay sharper longer");
    }
}
// --[FEATURE #10]
// -- Additional Auto Clamp system
void AdditionalAutoClamSystem() {
    if (A[4315].HasValue("Auto Clamp system")) {
        Add($"AdditionalAutoClamSystem⸮Automatic clamp holds work securely and prevents shifting");
    }
}
// --[FEATURE #11]
// -- Additional Used for cutting
void AdditionalUsedForCutting() {
    if (A[4309].HasValue()) {
        Add($"AdditionalUsedForCutting⸮Used for cutting {A[4309].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
}
// --[FEATURE #12]
// -- Additional features
void AdditionalPaperTrimmersFeatures() {
    if (A[4309].HasValue() && A[4309].WhereNot("safety interlock", "self-sharpening blade", "automatic blade locking system", "titanium coated blades", "Auto Clamp system").Count() > 0) {
        Add($"AdditionalPaperTrimmersFeatures⸮Features {A[4315].WhereNot("safety interlock", "self-sharpening blade", "automatic blade locking system", "titanium coated blades", "Auto Clamp system").Select(o => o.Value()).FlattenWithAnd().Replace("TripleTrack System", "TripleTrack system", "Soft-touch handle", "soft-touch handle")}");
    }
}
// --[FEATURE #13]
// -- Additional recycled

// --[FEATURE #14]
// -- Additional warranty


//§§11036101167637 end of "Paper Trimmers"