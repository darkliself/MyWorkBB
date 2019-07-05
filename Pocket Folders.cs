//§§135391203140674 "Pocket Folders" "Alex K."

//  - All
// NumberOfPockets - All
// FileFoldersPaperSize - All
// Expansion - All
// Pack Size
// Recycled Content 
// acid free
AdditionalBusinessCardHolder();
AdditionalClosedSides();
AdditionalSheetCapacity();
AdditionalDoubleTangFasteners();
AdditionalCDPocket();
Additional3Holes();
AdditionalCoverFlap();
//


// --[FEATURE #1]
// --Pocket Folder Type, Color & Pocket Folder Material


// --[FEATURE #2]
// --# of Pockets


// --[FEATURE #3]
// --Paper Size


// --[FEATURE #4]
// --Expandable Folder (If Applicable)


// --[FEATURE #5]
// --Pocket Folder Pack Size (If more than 1)


// --[FEATURE #6]
// --Recycled Content % (If Applicable)

// --[FEATURE #7]
// -- Additional Business Card Holder
void AdditionalBusinessCardHolder() {
    if (A[5965].HasValue("business card%")) {
        Add($"AdditionalBusinessCardHolder⸮A business card holder adds an extra professional touch to each folder");
    }
}
// --[FEATURE #8]
// -- Additional Closed Sides
void AdditionalClosedSides() {
    if (A[5945].HasValue("closed sides")) {
        Add($"AdditionalClosedSides⸮Closed sides keep materials safe and secure");
    }
}

// --[FEATURE #9]
// -- Additional sheet capacity
void AdditionalSheetCapacity() {
    if (A[6576].HasValue()) {
        Add($"AdditionalSheetCapacity⸮{A[6576].FirstValueOrDefault()}-{A[6576].Units.First().Name.Replace("sheets", "sheet", "pages", "page")} capacity allows for convenient organization of loose papers");
    }
}

// --[FEATURE #10]
// -- Additional double-tang fasteners
void AdditionalDoubleTangFasteners() {
    if (A[5950].HasValue("3 double-tang fasteners")) {
        Add($"AdditionalDoubleTangFasteners⸮Includes stitched-in gussets with three double-tang fasteners to bind punched sheets");
    }
}
// --[FEATURE #11]
// -- Additional CD pocket
void AdditionalCDPocket() {
    if (A[5965].HasValue("%CD pocket%")) {
        Add($"AdditionalCDPocket⸮Has slots for CDs too, making it great for presentations and press kits");
    }
}

// --[FEATURE #12]
// -- Additional
void Additional3Holes() {
    if (A[6548].HasValue("3") || A[5940].HasValue("3 holes")) {
        Add($"Additional3Holes⸮Folder is 3-hole punched for greater ease and is ideal for office use");
    }
}

// --[FEATURE #13]
// -- Additional Cover Flap
void AdditionalCoverFlap() {
    if (A[5939].HasValue("%flap%")) {
        Add($"AdditionalCoverFlap⸮Get the extra capacity of a file pocket with the added security of a protective cover flap");
    }
}

//§§135391203140674 end of "Pocket Folders"