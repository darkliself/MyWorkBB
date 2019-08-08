//§§1683366810274142820 "All-Purpose Cleaners & Degreasers" "Alex K."
CleanersDegreasersTypeForm();
CleanersDegreasersSize();
// PackSize
CleanersDegreasersActiveChemical();
// Standards
CleanersDegreasersLowFoaming();
CleanersDegreasersWaterBasedFormula();
CleanersDegreasersUnpleasantOdors();
CleanersDegreasersMultiSurface();

// --[FEATURE #1]
// -- Type and Form (Multi, Glass, Disinfect AND Wipe, Spray, Concentrate) including Product scent
void CleanersDegreasersTypeForm() {
    // Spray|Tablets|Foam|Aerosol|Wipes|Liquid|Pod or Capsule|Gel|Sponge|Cream|Powder
    var formFactor = GetReferenceBase("SP-23334");
    
    var typeOfSpeCleaner = GetReferenceBase("SP-24326");
    var typeOfFloorCleaner = GetReferenceBase("SP-24150");
    
    if (formFactor.HasValue("Liquid") && typeOfSpeCleaner.HasValue("All-Purpose Cleaner")
    && A[6955].HasValue() && A[6955].Values.Count() > 1) {
        Add($"CleanersDegreasersTypeForm⸮All-purpose liquid cleaning solution with {A[6955].Values.Flatten(", ").ToTitleCase().Replace(" scent", "")} scents");
    }
    else if (formFactor.HasValue("Liquid") && typeOfSpeCleaner.HasValue("All-Purpose Cleaner")
    && A[6955].HasValue()) {
        Add($"CleanersDegreasersTypeForm⸮All-purpose liquid cleaning solution with {A[6955].FirstValueOrDefault().ToTitleCase().Replace(" scent", "")} scent");
    }
    else if (formFactor.HasValue("Liquid") && typeOfSpeCleaner.HasValue("All-Purpose Cleaner")) {
        Add($"CleanersDegreasersTypeForm⸮All-purpose liquid cleaning solution is perfect for garbage cans, doors and more");
    }
    else if (!A[6955].HasValue()) {
        Add($"CleanersDegreasersTypeForm⸮Cleaner has no scent, just specially formulated cleansing agents for lasting results");
    }
    else if (formFactor.HasValue("Liquid") && A[6955].HasValue() && typeOfFloorCleaner.HasValue()) {
        Add($"CleanersDegreasersTypeForm⸮{typeOfFloorCleaner.ToLower().ToUpperFirstChar()} with {A[6955].FirstValueOrDefault().ToTitleCase().Replace(" scent", "")} scent");
    }
    else if (typeOfSpeCleaner.HasValue("all-purpose cleaner") && A[6955].HasValue() && A[6956].HasValue()) {
        Add($"CleanersDegreasersTypeForm⸮All-purpose cleaner with {A[6955].FirstValueOrDefault().ToTitleCase().Replace(" scent", "")} scent and is ideal for use on {A[6956].Values.Take(2).FlattenWithAnd()}");
    }
    else if (A[6964].HasValue() && formFactor.HasValue("wipes")) {
        Add($"CleanersDegreasersTypeForm⸮Disinfecting Wipes are known for killing germs as well as flu virus");
    }
    else if (formFactor.HasValue("spray") && A[6956].HasValue() && A[6956].Values.Count() > 1 && A[6955].HasValue()) {
        Add($"CleanersDegreasersTypeForm⸮Spray cleaner has a {A[6955].FirstValueOrDefault().ToTitleCase().Replace(" scent", "")} scent and removes dirt from a variety of surfaces");
    }
    else if (formFactor.HasValue("spray") && A[6956].HasValue() && A[6956].Values.Count() > 1) {
        Add($"CleanersDegreasersTypeForm⸮Spray cleaner removes dirt from {A[6956].Values.Take(2).FlattenWithAnd()}");
    }
    else if (formFactor.HasValue("spray") && A[6955].HasValue("%original%")) {
        Add($"CleanersDegreasersTypeForm⸮Spray cleaner has an Original scent");
    }
    else if (formFactor.HasValue("spray") && A[6955].HasValue()) {
        Add($"CleanersDegreasersTypeForm⸮Spray cleaner has a {A[6955].FirstValueOrDefault().ToTitleCase().Replace(" scent", "")} scent");
    }
    else if (formFactor.HasValue("spray")) {
        Add($"CleanersDegreasersTypeForm⸮Spray cleaner removes dirt from a variety of surfaces");
    }
    else if (typeOfFloorCleaner.HasValue() && A[6955].HasValue() && !A[6955].HasValue("oceanic", "nature")) {
        Add($"CleanersDegreasersTypeForm⸮{typeOfFloorCleaner.ToLower().ToUpperFirstChar()} with a {A[6955].FirstValueOrDefault().ToTitleCase().Replace(" scent", "")} scent");
    }
    else if (typeOfFloorCleaner.HasValue() && A[6955].HasValue("oceanic")) {
        Add($"CleanersDegreasersTypeForm⸮{typeOfFloorCleaner.ToLower().ToUpperFirstChar()} with an oceanic scent");
    }
    else if (typeOfFloorCleaner.HasValue() && A[6955].HasValue("nature")) {
        Add($"CleanersDegreasersTypeForm⸮{typeOfFloorCleaner.ToLower().ToUpperFirstChar()} with scent of nature");
    }
}
// --[FEATURE #2]
// -- Size (ounces)
void CleanersDegreasersSize() {
    var size = GetReferenceBase("SP-21132");
    var typeOfSpeCleaner = GetReferenceBase("SP-24326");
    if (typeOfSpeCleaner.In("Aerosol", "Spray") && size.HasValue()) {
        Add($"CleanersDegreasersSize⸮{size} oz. bottle allows easy spraying with no mess or fumes");
    }
    else if (A[6956].HasValue("%Glass%","%window%", "%mirror%") && size.HasValue()) {
        Add($"CleanersDegreasersSize⸮Comes in a {size} oz. bottle and is perfect for cleaning all glass surfaces");
    }
    else if (size.HasValue()) {
        Add($"CleanersDegreasersSize⸮{size} oz. capacity");
    }
}
// --[FEATURE #3]
// -- Pack Size (or each)

// --[FEATURE #4]
// -- Active Chemical(s)
void CleanersDegreasersActiveChemical() {
    if (A[6961].HasValue()) {
        Add($"CleanersDegreasersActiveChemical⸮Professional formula lifts dirt and grime and leaves a pleasant fragrance");
    }
}
// --[FEATURE #5]
// -- Additional Low Foaming
void CleanersDegreasersLowFoaming() {
    if (A[6970].HasValue("%low foam%")) {
        Add($"CleanersDegreasersLowFoaming⸮Low foaming to prevent recovery tank clogging");
    }
}
// --[FEATURE #6]
// -- Additional
// Standards

// --[FEATURE #7]
// -- Additional
void CleanersDegreasersWaterBasedFormula() {
    if (A[6970].HasValue("%water%based%")) {
        Add($"CleanersDegreasersWaterBasedFormula⸮Water-based formula perfect for routine maintenance to clean and preserve shine");
    }
}
// --[FEATURE #8]
// -- Additional
void CleanersDegreasersUnpleasantOdors() {
    if (A[6970].HasValue("%unpleasant odors%")) {
        Add($"CleanersDegreasersUnpleasantOdors⸮Deodorizes and eliminates unpleasant odors");
    }
}
// --[FEATURE #9]
// -- Additional Multi Surface
void CleanersDegreasersMultiSurface() {
    if (A[6951].HasValue("wipes") && A[6956].HasValue() && A[6956].Values.Count() > 3) {
        Add($"CleanersDegreasersMultiSurface⸮Multi-surface cleaning wipes");
    }
}

//§§1683366810274142820 end of "All-Purpose Cleaners & Degreasers"