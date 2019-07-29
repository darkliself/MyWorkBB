//§§135391203220757  "Accordion Folders" "Alex K."
FileTypeNumberOfFilePockets();
AccordionFoldersClosure();
// FolderMaterialColorDurability - All
// FileFoldersPaperSize - All
// FileFoldersFolderFeatures - ALL
// IndexTypes - All
// PackSize
// PostConsumerContent
// RecycledContent
// AdditionalWaterproof -All
// AdditionalAntimicrobialOfClassificationFolders - All
// AdditionalFoldersWearResistant - All
// AdditionalTyvekGussetOfClassificationFolders - All
// AdditionalReinforcedTabsOfClassificationFolders - All
// AdditionalSafeSHIELDOfClassificationFolders - All

// Standards

// --[FEATURE #1]
// -- <File Type>, <Number of File Pockets (If Applicable)> & Use
void FileTypeNumberOfFilePockets() {
    //Expanding file includes 21 compartments and an A-Z index for organizing papers & documents
    //Expanding desk file features an A-Z index for organization and easy access to your files
    //Expanding file features seven pockets for easy organization of your papers
    // Envelope|Accordion File|File Pocket|Document File|Expanding Wallet
    var type = GetReferenceBase("SP-24101");
    if (type.HasValue() && A[6546].HasValue()) {
        var pluralPockets = A[6546].HasValue("1") ? "pocket" : "pockets";
        Add($"NumOfPocketFolder⸮{type.ToLower().ToUpperFirstChar()} features {A[6546].FirstValueOrDefault()} {pluralPockets} for easy organizing papers ##and documents");
    }
    else if (type.HasValue()) {
        var folder = type.HasValue("Envelope") ? "" : "folder";
        Add($"NumOfPocketFolder⸮{type.ToLower().ToUpperFirstChar()} {folder} to keep your papers and documents organized");
    }
}
// --[FEATURE #2]
// -- <Accordion File Closure Type>
 void AccordionFoldersClosure(){
    //Elastic Closure|Zipper Closure|Open|Twist Lock|Flap & Cord Closure|Hook & Loop Closure|Snap Closure|Button & String Closure
    var closure = GetReferenceBase("SP-24103");
    // ensures your papers stay in place
    if(closure.HasValue()){
        Add($"AccordionFoldersClosure⸮{closure.ToLower().ToUpperFirstChar().Replace("&", "##and").Replace(" closure", "")} closure ensures your papers stay in place");
    }
}

// --[FEATURE #3]
// -- <Durability (If Standard = Leave Blank)>, <True color> & <Folder Material>
// FolderMaterialColorDurability - All

// --[FEATURE #4]
// -- <Paper Size>
// FileFoldersPaperSize - All

// --[FEATURE #5]
// -- <Folder Expansion (inches)>
// Expansion - All

// --[FEATURE #6]
// -- <Folder Features (If Applicable)>
// FileFoldersFolderFeatures - All

// --[FEATURE #7]
// -- <Type of Index>
// IndexTypes - All

// --[FEATURE #8]
// -- <Pack Qty (If more than 1)>

// --[FEATURE #9]
// -- <Post Consumer Content (%)>

// --[FEATURE #10]
// -- <Recycled Content (%)>

// --[FEATURE #11]
// -- Additional Waterproof

// --[FEATURE #12]
// -- Additional antimicrobial
// AdditionalAntimicrobialOfClassificationFolders

// --[FEATURE #12]
// -- Additional Wear Resistant - All


//§§135391203220757  end of "Accordion Folders"
