//§§1221973142133 "Letter & Desktop Trays" "Alex K."
LetterDesktopTrayType();
// TrueColorMaterial
// Dimensions
// ReportCoversPaperSize - 48 ReportCoversPaperSize();
// LetterDesktopTrayMaximumPaperSize -- 48 MaximumPaperSize();
// NumOfCompartmentsAll - All
// PackSize
// RecycledContent
// PostConsumerContent
AdditionalZigzagDesign();
AdditionalRibbedBase();
AdditionalWireConstruction();
AdditionalBreakResistant();
AdditionalSolidDesign();
AdditionalLetterDesktopTrayRubberFeet();
AdditionalLetterDesktopTrayHandles();
// Standards
// Warranty

// --[FEATURE #1]
// -- Letter/desktop tray type & Use
void LetterDesktopTrayType() {
    var type = GetReferenceBase("SP-18637");
    if (type.HasValue("Side Loading") && A[5922].HasValue()) {
        Add($"LetterDesktopTrayType⸮Create a stylish and convenient desktop organization system using these {type.ToLower().Replace(" loading", "-loading")} {A[5922].FirstValueOrDefault().Pluralize()}");
    }
    else if (type.HasValue() && A[5922].HasValue()) {
        Add($"LetterDesktopTrayType⸮Save space around your desk with {type.ToLower().Replace(" loading", "-loading")} {A[5922].FirstValueOrDefault().Pluralize()}");
    }
}
// --[FEATURE #2]
// -- True color & Material

// --[FEATURE #3]
// -- Dimensions (in Inches): H x W x D

// --[FEATURE #4]
// -- Paper Size

// --[FEATURE #5]
// -- # of compartments
void NumOfCompartmentsAll() {
    var count = GetReferenceBase("SP-21897");
    if (count.HasValue()) {
        Add($"NumOfCompartmentsAll⸮Features {count} compartments");
    }
}
// --[FEATURE #6]
// -- Maximum paper size

// --[FEATURE #7]
// -- Pack quantity (If more than 1)

// --[FEATURE #8]
// -- Feature

// --[FEATURE #9]
// -- Recycled content (%)

// --[FEATURE #10]
// -- Additional zigzag design
void AdditionalZigzagDesign() {
    var count = GetReferenceBase("SP-21897");
    if (A[5945].HasValue("zigzag design")) {
        Add($"AdditionalZigzagDesign⸮Stylish zigzag design adds a modern look to your office");
    }
}
// --[FEATURE #11]
// -- Additional ribbed base
void AdditionalRibbedBase() {
    if (A[5945].HasValue("ribbed base")) {
        Add($"AdditionalRibbedBase⸮Ribbed base design prevents papers from sticking to the bottom");
    }
}
// --[FEATURE #12]
// -- Additional wire construction
void AdditionalWireConstruction() {
    if (A[5955].HasValue("%wire%")) {
        Add($"AdditionalWireConstruction⸮Simple wire construction for any desktop or shelf");
    }
}
// --[FEATURE #12]
// -- Additional
void AdditionalBreakResistant() {
    if (A[5945].HasValue("break-resistant")) {
        Add($"AdditionalBreakResistant⸮Features break-resistant construction");
    }
}
// --[FEATURE #12]
// -- Additional Solid Design
void AdditionalSolidDesign() {
    if (GetReferenceBase("SP-17441").HasValue("%Black%") 
    && (GetReferenceBase("SP-17447").HasValue("%Plastic%") || GetReferenceBase("SP-17447").HasValue("%steel%")
    || GetReferenceBase("SP-17447").HasValue("%metal%"))) {
        Add($"AdditionalSolidDesign⸮Solid design complements any work area");
    }
}
// --[FEATURE #12]
// -- Additional Rubber Feet
void AdditionalLetterDesktopTrayRubberFeet() {
    if (A[5945].HasValue("%rubber feet%")) {
        Add($"AdditionalLetterDesktopTrayRubberFeet⸮Most items include rubber feet to protect work surface");
    }
}
// --[FEATURE #12]
// -- Additional handles
void AdditionalLetterDesktopTrayHandles() {
    if (A[5945].HasValue("%handles%")) {
        Add($"AdditionalLetterDesktopTrayHandles⸮Built in handles make trays easily portable");
    }
}
//§§1221973142133 end of "Letter & Desktop Trays"