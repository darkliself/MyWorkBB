//§§1676385310823140691  "Copy & Multipurpose Paper" "Alex K."

CopyPaperTypeAndUse();
// Dimensions
// PaperWeight - All
// PaperBrightnessRating
PaperColorAndFinish();
SheetQuantityAndReamsPerCase();
// AcidFree
// RecycleContent
// Standards
AdditionalCopyPaperSmoothSurface();
// NumberOfHoles - all

// --[FEATURE #1]
// -- Paper type & Use
void CopyPaperTypeAndUse() {
    var type = GetReferenceBase("SP-21737");
    var use = GetReferenceBase("SP-12560");
    if (type.HasValue("Specialty") && A[4759].HasValue("%carbonless paper%")) {
        Add($"CopyPaperTypeAndUse⸮Specialty computer paper is excellent for making carbonless copies");
    }
    else if (type.HasValue() &&  use.HasValue() && !use.HasValue("Other")) {
        Add($"CopyPaperTypeAndUse⸮Reliable {type.ToLower()} paper is perfect for {use.ToLower()} use");
    }
    else if (type.HasValue() && use.HasValue("Other")) {
        Add($"CopyPaperTypeAndUse⸮Reliable {type.ToLower()} paper is perfect for various uses");
    }
    else if (type.HasValue()) {
        Add($"CopyPaperTypeAndUse⸮Reliable {type.ToLower()} paper is perfect for everyday use");
    }
    else if (A[4763].HasValue()) {
        Add($"CopyPaperTypeAndUse⸮Works great in your {A[4763].Values.Select(o => o.Value()).FlattenWithAnd().Replace("and", "or")} printer");
    }
}
// --[FEATURE #2]
// -- Paper Weight (lbs.) *Include both Bond and Text weight if applicable.

// --[FEATURE #3]
// -- Dimensions (in Inches): Standup use Height x Width x Depth, Layflat use Width x Length

// --[FEATURE #4]
// -- Brightness

// --[FEATURE #5]
// -- Paper Color & Finish
void PaperColorAndFinish() {
    var trueColor = GetReferenceBase("SP-22967");
    var paperFinish = GetReferenceBase("SP-21738");
    if (trueColor.HasValue("Photo white") && paperFinish.HasValue()) {
        Add($"PaperColorAndFinish⸮White photo paper with {paperFinish.ToLower(true)} finish");
    }
    else if (trueColor.HasValue() && paperFinish.HasValue()) {
        Add($"PaperColorAndFinish⸮{trueColor.ToLower().ToUpperFirstChar()} paper with {paperFinish.ToLower(true)} finish");
    }
    else if (trueColor.HasValue() && trueColor.HasValue("%bar%")) {
        Add($"PaperColorAndFinish⸮Comes in {trueColor.ToLower()}");
    }
}
// --[FEATURE #6]
// -- Sheet Quantity & Reams per case
void SheetQuantityAndReamsPerCase() {
    var rearms = GetReferenceBase("SP-22607");
    var packQty = GetReferenceBase("SP-16747");
    // Carton|Count|Each|Roll|Pallet|Box|Truck Load|Ream|Case|Pack
    var sellingQuantity = GetReferenceBase("SP-23866");
    if (SKU.ProductId.In("20503614", "20503652")) {
        Add($"SheetQuantityAndReamsPerCase⸮Contains 10 reams per case, 40 cases per pallet");
    }
    else if (A[353].HasValue() && rearms.HasValue("1-Ream") && sellingQuantity.HasValue("%Ream%") && packQty.HasValue("1")) {
        Add($"SheetQuantityAndReamsPerCase⸮One ream, one sheet");
    }
    else if (A[353].HasValue() && rearms.HasValue("1-Ream") && sellingQuantity.HasValue() && packQty.HasValue("1")) {
        Add($"SheetQuantityAndReamsPerCase⸮One ream per {sellingQuantity.ToLower()}, one sheet");
    }
    else if (A[353].HasValue() && rearms.HasValue("1-Ream") && sellingQuantity.HasValue("%Ream%") && packQty.HasValue()) {
        Add($"SheetQuantityAndReamsPerCase⸮One ream, {packQty} sheets total");
    }
    else if (A[353].HasValue() && rearms.HasValue("1-Ream") && packQty.HasValue() && sellingQuantity.HasValue()) {
        Add($"SheetQuantityAndReamsPerCase⸮One ream per {sellingQuantity.ToLower()}, {packQty} sheets total");
    }
    else if (A[353].HasValue() && rearms.HasValue() && sellingQuantity.HasValue("%Ream%") && packQty.HasValue()) {
        Add($"SheetQuantityAndReamsPerCase⸮{rearms.Replace("-Ream", "")} reams, {packQty} sheets total");
    }
    else if (A[353].HasValue() && rearms.HasValue() && packQty.HasValue() && sellingQuantity.HasValue()) {
        Add($"SheetQuantityAndReamsPerCase⸮{rearms.Replace("-Ream", "")} reams per {sellingQuantity.ToLower()}, {packQty} sheets total");
    }
}
// --[FEATURE #7]
// -- Acid Free

// --[FEATURE #8]
// -- Recycled Content (%)

// --[FEATURE #9]
// -- Additional

// --[FEATURE #10]
// -- Additional smooth surface
void AdditionalCopyPaperSmoothSurface() {
    if (A[10618].HasValue("smooth")) {
        Add($"AdditionalCopyPaperSmoothSurface⸮Smooth surface for high-impact color graphics");
    }
}
// --[FEATURE #11]
// -- Additional

// --[FEATURE #12]
// -- Additional

//§§1676385310823140691  end of "Copy & Multipurpose Paper"