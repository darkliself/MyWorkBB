//§§2382510780166206  "Bookcases & Bookshelves" "Alex K."

TypeOfBookCase();
BookCaseNumOfShelves();
// Dimensions
BookCaseWeightCapacityOfShelves();
DescriptiveStyleTrim();
//Assembly
AdditionalResistantBookCase();
AdditionalCastersBookCase();
// Standards
// Warranty


// --[FEATURE #1]
// --Type of bookcase, including Finish and Material
void TypeOfBookCase() {
    // Cube|Book Displays & Easels|Wall Shelves|Corner|Barrister|Mobile|Leaning|Standard
    var type =  GetReferenceBase("SP-16664$");
    var material = GetReferenceBase("SP-17447");
    
    if (type.In("Cube", "Corner", "Barrister", "Mobile", "Leaning", "Standard") && material.HasValue()) {
        Add("TypeOfBookCase⸮{type} bookcase provides plenty of storage space and features durable {material.ToLower(true)} construction");
    }
    else if (type.In("Cube", "Corner", "Barrister", "Mobile", "Leaning", "Standard")) {
        Add("TypeOfBookCase⸮{type} bookcase adds extra storage to your space");
    }
    else if (type.HasValue("Wall Shelves") && material.HasValue()) {
        Add("TypeOfBookCase⸮Wall shelves provides plenty of storage space and features durable {material.ToLower(true)} construction");
    }
    else if (SKU.ProductLineName.HasValue("%Book Display%") || SKU.ModelName.HasValue("%Book Display%")
    || REQ.GetVariable("CNET_HL").HasValue("%Book Display%") || REQ.GetVariable("CNET_SD").HasValue("%Book Display%")) {
        Add("TypeOfBookCase⸮Book display offers a practical solution for your storage and display needs");
    }
    else if (SKU.ProductLineName.HasValue("%Easel%") || SKU.ModelName.HasValue("%Easel%")
    || REQ.GetVariable("CNET_HL").HasValue("%Easel%") || REQ.GetVariable("CNET_SD").HasValue("%Easel%")) {
        Add("TypeOfBookCase⸮Easel provides additional display space without sacrificing style");
    }
    else if (type.HasValue() && material.HasValue()) {
        Add("TypeOfBookCase⸮{type} bookcase provides plenty of storage space and made of {material.ToLower(true)}");
    }
    else if (type.HasValue()) {
        Add("TypeOfBookCase⸮{type} bookcase provides plenty of storage space");
    }
    else if (material.HasValue()) {
        Add("TypeOfBookCase⸮Made of {material.ToLower(true)}");
    }
}

// --[FEATURE #2]
// --# of shelves, include if adjustable or not and if yes, increments
void BookCaseNumOfShelves() {
    var numAdjustableShelves =  GetReferenceBase("SP-22946");
    var numNonAdjustableShelves = GetReferenceBase("SP-22947");
    var increment = GetReferenceBase("SP-22952");
    var pluralAdjShelves = "shelves";
    var pluralNonAdjShelves = "shelves";
    if (numAdjustableShelves.HasValue("1")) {
        pluralAdjShelves = "shelf";
    }
    if (numNonAdjustableShelves.HasValue("1")) {
        pluralNonAdjShelves = "shelf";
    }
    
    if (numAdjustableShelves.HasValue() && numNonAdjustableShelves.HasValue()) {
        Add("BookCaseNumOfShelves⸮{numAdjustableShelves} adjustable {pluralAdjShelves} provide flexibility and {numNonAdjustableShelves} fixed {pluralNonAdjShelves} add stability");
    }
    else if(numAdjustableShelves.HasValue() && increment.HasValue() && increment.ExtractNumbers().Any()) {
         Add("BookCaseNumOfShelves⸮{numAdjustableShelves} {pluralAdjShelves} adjust in {increment}\" increment for customizable storage");
    }
    else if (numAdjustableShelves.HasValue()) {
        Add("BookCaseNumOfShelves⸮{numAdjustableShelves} adjustable {pluralAdjShelves} for flexible storage solutions");
    }
    else if (numNonAdjustableShelves.HasValue()) {
        Add("BookCaseNumOfShelves⸮{numNonAdjustableShelves} fixed {pluralNonAdjShelves} for added stability");
    }
}

// --[FEATURE #3]
// --Dimensions (in Inches): H x W x D

// --[FEATURE #4]
// --Weight capacity of shelves in lbs. Make sure to add "evenly distributed"

void BookCaseWeightCapacityOfShelves() {
    var capacity =  GetReferenceBase("SP-21344");
    if (capacity.HasValue()) {
        Add("BookCaseWeightCapacityOfShelves⸮Shelves support up to {capacity} lbs. evenly distributed, letting you store books, pictures and other objects");
    }
}
// --[FEATURE #5]
// --Descriptive text for style/trim (If Applicable)
void DescriptiveStyleTrim() {
    if (A[10997].HasValue("%contemporary%")) {
        Add($"DescriptiveStyleTrim⸮Open and airy design for a contemporary look");
    }
    else if (A[10997].HasValue("%modern%") && A[7281].HasValue()) {
        Add($"DescriptiveStyleTrim⸮Modern style {A[7281].FirstValueOrDefault()}");
    }
    else if (A[8872].HasValue("%ladder%") && A[7281].HasValue()) {
        Add($"DescriptiveStyleTrim⸮Modern ladder style {A[7281].FirstValueOrDefault()}");
    }
    else if (A[7243].HasValue()) {
        Add($"DescriptiveStyleTrim⸮Available in {A[7243].FirstValueOrDefault()} finish");
    }
    else if (A[7277].HasValue()) {
        Add($"DescriptiveStyleTrim⸮Available in {A[7277].FirstValueOrDefault()} finish");
    }
}

// --[FEATURE #6]
// --Assembly information

// --[FEATURE #7]
// --Additional resistant
)
void AdditionalResistantBookCase() {
    if (A[7297].HasValue("%-resistant%") && A[7297].Where("%-resistant%").Count() > 1) {
        Add($"AdditionalResistantBookCase⸮Provides maximum protection against " 
        + $"{A[7297].Where("%-resistant%").Select(o => o.Value().Replace("-resistant", "")).FlattenWithAnd().Replace("scratch","scratches", "spill", "spills")}");
    }
    else if (A[7297].HasValue("%scratch-resistant%")) {
        Add($"AdditionalResistantBookCase⸮The scratch-resistant surface looks like new for many years");
    }
    else if (A[7297].HasValue("%stain-resistant%") && A[7281].HasValue()) {
        Add($"AdditionalResistantBookCase⸮Resists stains, so the {A[7281].FirstValueOrDefault()} looks neat and professional");
    }
    else if (A[7297].HasValue("%spill-resistant%")) {
        Add($"AdditionalResistantBookCase⸮Provides maximum protection against spills");
    }
    
}

// --[FEATURE #8]
// -- Additional -casters
void AdditionalCastersBookCase() {
    if (A[7297].HasValue("%casters%")) {
        Add($"AdditionalCastersBookCase⸮{A[7297].Where("%casters%").Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} for easy mobility");
    }
}

// --[FEATURE #9]
// --

// --[FEATURE #10]
// --

// --[FEATURE #11]
// --

// --[FEATURE #12]
// --Warranty (should be last bullet)

//§§2382510780166206 end of "Bookcases & Bookshelves"