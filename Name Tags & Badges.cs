//§§542129029641140965  "Name Tags & Badges" "Alex K."
NameTagBadgeTypeAndUse();
AttachmentStyle();
BadgeLoadingStyle();
// Dimentions
PrinterTypeUsed();
// PackSize
// RecycledContent
// PostConsumerContent
NameTagsOrientation();
AdditionalPVCFree();
// Additional material color
AdditionalPrePunchedNameTags();
AdditionalGarmentFriendlyClip();
AdditionalLacqueredMagneticSide();
AdditionalNameTagRemovableAdhesive();


// --[FEATURE #1]
// -- Name tag/badge type & Use
void NameTagBadgeTypeAndUse() {
    var type = GetReferenceBase("SP-18516");
    if (type.HasValue("ID Badge Holder")) {
        Add($"NameTagBadgeTypeAndUse⸮Fasten this ##ID badge holder to clothing, bags and more to keep your hands free");
    }
    else if (type.HasValue("Badge Reels")) {
        Add($"NameTagBadgeTypeAndUse⸮Badge reel connects easily to any ID card or name badge");
    }
    else if (type.HasValue("Lanyards")) {
        Add($"NameTagBadgeTypeAndUse⸮Lanyard to hang identification badge around your neck");
    }
    else if (type.HasValue("Sticker Name Tags/Labels")) {
        Add($"NameTagBadgeTypeAndUse⸮Name tag is ideal for meetings, seminars, special events, conventions and more");
    }
    else if (type.HasValue("ID Cards")) {
        Add($"NameTagBadgeTypeAndUse⸮ID Cards for everyday use");
    }
    else if (type.HasValue("Plastic Name Tags/ID Cards")) {
        Add($"NameTagBadgeTypeAndUse⸮Plastic Name Tags for everyday use");
    }
    else if (type.HasValue()) {
        Add($"NameTagBadgeTypeAndUse⸮{type.ToLower().ToUpperFirstChar()} for everyday use");
    }
}
// --[FEATURE #2]
// -- Attachment style
void AttachmentStyle() {
    var type = GetReferenceBase("SP-21608");
    
    if (type.HasValue("Pin")) {
        Add($"AttachmentStyle⸮Pin securely holds badge in place");
    }
    else if (A[6086].HasValue("metal clip") || A[6121].HasValue("metal clip")) {
        Add($"AttachmentStyle⸮Secures to clothing with metal clip");
    }
    else if (A[6086].HasValue("clip") || A[6121].HasValue("clip")) {
        Add($"AttachmentStyle⸮Clip secures badge to clothing");
    }
   else if (A[6121].HasValue("belt carabiner") || A[6121].HasValue("belt clip")) {
        Add($"AttachmentStyle⸮Belt clip model hooks on pants belt");
    }
    else if (type.HasValue("Bulldog Clip")) {
        Add($"AttachmentStyle⸮Bulldog clip fastens to clothing, bags and more");
    }
    else if (type.HasValue("Swivel Clip")) {
        Add($"AttachmentStyle⸮Integrated swivel clip helps prevent damage to garments");
    }
    else if (type.HasValue("Swivel Hook")) {
        Add($"AttachmentStyle⸮Swivel hook to attach ID card securely");
    }
}
// --[FEATURE #3]
// -- Badge loading style
void BadgeLoadingStyle() {
    if (A[6087].HasValue("top side open")) {
        Add($"BadgeLoadingStyle⸮Top-loading design prevents inserts from falling out");
    }
    else if (GetReferenceBase("SP-21603").HasValue("Self Adhesive")) {
        Add($"BadgeLoadingStyle⸮Self-adhesive badges adhere firmly and remove easily");
    }
}
// --[FEATURE #4]
// -- Name tag & Badge size (Use fractions over decimals)

// --[FEATURE #5]
// -- Printer type used (If applicable)
void PrinterTypeUsed() {
    var type = GetReferenceBase("SP-21605");
    if (type.HasValue("Inkjet/Laser")) {
        Add($"PrinterTypeUsed⸮Designed for use with both inkjet and laser printers for optimal versatility");
    }
    else if (type.HasValue("Laser")) {
        Add($"PrinterTypeUsed⸮Allows creating badges with laser printers, eliminating extra costs");
    }
    else if (type.HasValue("Inkjet")) {
        Add($"PrinterTypeUsed⸮Allows creating badges with inkjet printers, eliminating extra costs");
    }
}
// --[FEATURE #6]
// -- Pack Qty (# of badges)

// --[FEATURE #7]
// -- Recycled Content (%)

// --[FEATURE #9]
// -- Additional Orientation
void NameTagsOrientation() {
    var type = GetReferenceBase("SP-21605");
    if (A[6087].HasValue("landscape/portrait orientation")) {
        Add($"NameTagsOrientation⸮Use in horizontal or vertical orientation");
    }
    else if ((A[6121].HasValue("portrait orientation", "vertical cards orientation") 
        || A[6087].HasValue("portrait orientation", "vertical cards orientation") 
        || A[4783].HasValue("portrait orientation", "vertical cards orientation"))
    && (A[6121].HasValue("landscape orientation", "horizontal cards orientation") 
        || A[6087].HasValue("landscape orientation", "horizontal cards orientation") 
        || A[4783].HasValue("landscape orientation", "horizontal cards orientation")
        || A[5944].HasValue("Yes"))) {
            Add($"NameTagsOrientation⸮Use in horizontal or vertical orientation");            
    }
    else if (A[6121].HasValue("portrait orientation", "vertical cards orientation") 
    || A[6087].HasValue("portrait orientation", "vertical cards orientation") 
    || A[4783].HasValue("portrait orientation", "vertical cards orientation")) {
        Add($"NameTagsOrientation⸮Portrait orientation for easy loading of the badge");    
    }
    else if (A[6121].HasValue("landscape orientation", "horizontal cards orientation") 
    || A[6087].HasValue("landscape orientation", "horizontal cards orientation") 
    || A[4783].HasValue("landscape orientation", "horizontal cards orientation")
    || A[5944].HasValue("Yes")) {
        Add($"NameTagsOrientation⸮Landscape orientation to slip badge easily into the holder");            
    }
}
// --[FEATURE #10]
// -- Additional PVC-free
void AdditionalPVCFree() {
    if (A[6087].HasValue("PVC-free") 
    || A[4783].HasValue("PVC-free")) {
        Add($"AdditionalPVCFree⸮The product is PVC-free");
    }
}
// --[FEATURE #11]
// -- Additional material color

// --[FEATURE #12]
// -- Additional Pre Punched
void AdditionalPrePunchedNameTags() {
    if (A[6087].HasValue("pre-punched holes", "pre-drilled holes") 
    || A[6121].HasValue("%pre-punched%")) {
        Add($"AdditionalPrePunchedNameTags⸮Pre-punched for use with strap clips, lanyards, reels, chains and more");
    }
}

// --[FEATURE #13]
// -- Additional Garment Friendly clip
void AdditionalGarmentFriendlyClip() {
    if (A[6087].HasValue("Garment Friendly clip")) {
        Add($"AdditionalGarmentFriendlyClip⸮Garment-friendly clip helps protect clothing");
    }
}
// --[FEATURE #14]
// -- Additional lacquered magnetic side
void AdditionalLacqueredMagneticSide() {
    if (A[6121].HasValue("lacquered magnetic side") && A[6086].HasValue("magnet")) {
        Add($"AdditionalLacqueredMagneticSide⸮Magnet attaches easily and won't mar clothing");
    }
}
// --[FEATURE #15]
// -- Additional Removable Adhesive
void AdditionalNameTagRemovableAdhesive() {
    if (GetReferenceBase("SP-18516").HasValue("Sticker Name Tags/Labels") && A[4783].HasValue("removable adhesive")) {
        Add($"AdditionalNameTagRemovableAdhesive⸮Magnet attaches easily and won't mar clothing");
    }
}
// --[FEATURE #16]
// -- Additional
//§§542129029641140965  end of "Name Tags & Badges"