//§§135391203140599 "Hanging File Folders" "Alex K."

// true color and material
// FileFoldersTabPostion
HangingFileFolderFeature();
// Expansion
// FileFoldersPaperSize
HangingFileDetailsConstruction();
// pack size
// Recycled
// Post Consumer Content
AdditionalWaterproof();
// AssemblyInformation
AdditionalHangingFileSwingHooks();

// --[FEATURE #1]
// --<Folder Material>, <True Color> & <Folder Durability (If Standard = Leave blank); Including stock & weight #

// --[FEATURE #2]
// --<Tab Position> & <Tabs>

// --[FEATURE #3]
// --<Folder Feature> (If Applicable)

void HangingFileFolderFeature() {
    if (A[5945].HasValue("closed sides")) {
        Add($"HangingFileFolderFeature⸮Closed sides so papers do not fall out");
    }
    else if (A[5945].HasValue("Easy Slide tab")) {
        Add($"HangingFileFolderFeature⸮Easy ##Slide tab easily slides and secures into any position");
    }
    else if (A[5945].HasValue("SureLock")) {
        Add($"HangingFileFolderFeature⸮Features ##SureLock locking round rings");
    } 
}


// --[FEATURE #4]
// --<Folder Expansion (Inches)> (If Applicable)

// --[FEATURE #5]
// --<Paper Size>

// --[FEATURE #6]
// --Hanging File attachments details/Construction
void HangingFileDetailsConstruction() {
    if (A[5945].HasValue("coated hanger")) {
        Add($"HangingFileDetailsConstruction⸮Durable coated hangers for easy sliding");
    }
    else if (A[5945].HasValue("built-in tension springs")) {
        Add($"HangingFileDetailsConstruction⸮Long plastic hooks with built-in tension springs to grip rails");
    }
    else if (A[6547].HasValue("%frame%")) {
        Add($"HangingFileDetailsConstruction⸮{A[6547].Where("%frame%").Select( o => o.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} provides long-lasting use");
    }
    else if (A[5945].HasValue("lowered top rail")) {
        Add($"HangingFileDetailsConstruction⸮Lowered top rail edge eliminates need for separate file folder tabs");
    }
    else if (A[5945].HasValue("reinforced gusset")) {
        Add($"HangingFileDetailsConstruction⸮Reinforced gusset provides extra strength at critical points");
    }
}
// --[FEATURE #7]
// --<Pack Qty (If more than 1)>

// --[FEATURE #8]
// --<Recycled Content (%)>

// --[FEATURE #9]
// --<Post Consumer Content (%)>

// --[FEATURE #10]
// --Additional Waterproof
void AdditionalWaterproof() {
    if (A[5945].HasValue("waterproof")) {
        Add($"AdditionalWaterproof⸮Closed sides so papers do not fall out");
    }
 
}
// --[FEATURE #11]
// --Additional AssemblyInformation

// --[FEATURE #13]
// -- Additional Hanging File Fits
void AdditionalHangingFileFits() {
    if (A[5921].HasValue("%Hanging File%")) {
        Add($"AdditionalHangingFileFits⸮{A[5921].FirstValueOrDefault().ToUpperFirstChar()} fits in a file drawer or desktop filing system");
    }
    else if (A[5922].HasValue("%Hanging File%")) {
        Add($"AdditionalHangingFileFits⸮{A[5922].FirstValueOrDefault().ToUpperFirstChar()} fits in a file drawer or desktop filing system");
    }
}
// --[FEATURE #14]
// -- Additional Hanging File Swing Hooks
void AdditionalHangingFileSwingHooks() {
    if (A[5945].HasValue("%Swing hooks")) {
        Add($"AdditionalHangingFileSwingHooks⸮Swing hooks fold in or swing out as needed for use as a hanging file or a regular expanding pocket");
    }
}
// --[FEATURE #15]
// -- Additional Hanging File Tab Positions
void AdditionalHangingFileTabPositions() {
    if (A[5945].HasValue("%tab positions")) {
        Add($"AdditionalHangingFileTabPositions⸮Tabs and inserts are included for placement in {A[5945].Where("%tab positions").First().Value()}");
    }
}

// --[FEATURE #16]
// -- Additional Additional Hanging File Box Bottom
void AdditionalHangingFileBoxBottom() {
    if (A[5945].HasValue("box bottom")) {
        Add($"AdditionalHangingFileBoxBottom⸮Box-bottom hanging file folders are ideal for large files");
    }
}
// --[FEATURE #16]
// --
//§§135391203140599 end of "Hanging File Folders"