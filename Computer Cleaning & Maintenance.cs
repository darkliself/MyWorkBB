//§§590434034623142034 "Computer Cleaning & Maintenance" "Alex K."
ComputerCleaningTypeAndUse();
ComputerCleaningOdorOrScent();
// CapacityOz();
ComputerCleaningKeyIngredient();
// pack size
// Certification & standards
ComputerCleaningAntiStatic();


// --[FEATURE #1]
// --Computer cleaning type & use
void ComputerCleaningTypeAndUse() {
    // Vacuum|CD/DVD Cleaners|Cleaning Kit|Wipes/Duster Combo|Wipes/Cloths|Air Duster|Repair Kit|Keyboard Cover|Laser Lens Cleaners|Keyboard Swabs|Screen Cleaner Sprays|Cleaning Card
    var type = GetReferenceBase("SP-18424");
    
    if (type.HasValue("Cleaning Kit")) {
        Add($"ComputerCleaningTypeAndUse⸮Cleaning kit provides safe and gentle cleaning for all your mobile and electronic devices");
    }
    else if (type.HasValue("Laser Lens Cleaners")) {
        Add($"ComputerCleaningTypeAndUse⸮Cleaning kit allow each brush to retract into a recess to avoid knocking laser lens out of alignment");
    }
    else if (type.HasValue("Repair Kit")) {
        Add($"ComputerCleaningTypeAndUse⸮Tool kit is ideal for electronics, computer maintenance and home repairs");
    }
    else if (type.HasValue("CD/DVD Cleaners")) {
        Add($"ComputerCleaningTypeAndUse⸮CD/DVD cleaner cleans up to 99% of all scratched ##DVDs, ##CDs, game discs, ##VCDs");
    }
    else if (type.HasValue("Vacuum")) {
        Add($"ComputerCleaningTypeAndUse⸮Vacuum computer cleaning system is designed to remove dust, dirt and debris from computer systems");
    }
    else if (type.HasValue("Air Duster")) {
        Add($"ComputerCleaningTypeAndUse⸮Air duster is perfect for removing dust or debris from sensitive electronic devices");
    }
    else if (type.HasValue("Screen Cleaner Sprays")) {
        Add($"ComputerCleaningTypeAndUse⸮Cleaning spray is suitable for cleaning notebook ##PCs, touchscreens, keyless pads and phones");
    }
    else if (type.HasValue("Wipes/Cloths")) {
        Add($"ComputerCleaningTypeAndUse⸮Screen wipes provide safe cleaning to any tablet, laptop, monitor, or cell phone screen");
    }
    else if (type.HasValue("Wipes/Duster Combo")) {
        Add($"ComputerCleaningTypeAndUse⸮Duster is ideal for cleaning keyboard or CPU and wipes are safe to use on notebooks");
    }
    else if (type.HasValue("Cleaning Card")) {
        Add($"ComputerCleaningTypeAndUse⸮Cleans card reader with one easy swipe");
    }
    else if (type.HasValue("Keyboard Guard")) {
        Add($"ComputerCleaningTypeAndUse⸮Guard protect your keyboard from dust and spills");
    }
    else if (type.HasValue("Printer Cleaning Kit")) {
        Add($"ComputerCleaningTypeAndUse⸮Printer cleaning kit offers a safe, easy way to clean printers, copiers and fax machines");
    }
    else if (type.HasValue()) {
        Add($"ComputerCleaningTypeAndUse⸮{type.ToLower(true).ToUpperFirstChar()}");
    }
}


// --[FEATURE #2]
// --Odor or scent
void ComputerCleaningOdorOrScent() {
    // Clean|Unscented|Bitterant|Slight Ethereal
    var odorOrScent = GetReferenceBase("SP-4886");
    
    if (odorOrScent.HasValue("Bitterant")) {
        Add($"ComputerCleaningOdorOrScent⸮Contains bitterant to help discourage inhalant abuse");
    }
    else if (odorOrScent.HasValue("Slight Ethereal")) {
        Add($"ComputerCleaningOdorOrScent⸮Slightly ethereal scent");
    }
    else if (odorOrScent.HasValue("Clean scent")) {
        Add($"ComputerCleaningOdorOrScent⸮Clean scent for pleasant effect");
    }
    else if (odorOrScent.HasValue("Unscented")) {
        Add($"ComputerCleaningOdorOrScent⸮Unscented to cause less irritation");
    }
}

// --[FEATURE #3]
// --Capacity
// CapacityOz


// --[FEATURE #4]
// --Key ingredient
void ComputerCleaningKeyIngredient() {
    // Clean|Unscented|Bitterant|Slight Ethereal
    var keyIngradient = GetReferenceBase("SP-24202");
    
    if (keyIngradient.HasValue("%Ozone Safe%")) {
        Add($"ComputerCleaningKeyIngredient⸮The 100% ozone-safe formula");
    }
    else if (keyIngradient.HasValue("%microfiber%")) {
        Add($"ComputerCleaningKeyIngredient⸮Manufactured using microfiber for added safety");
    }
    else if (keyIngradient.HasValue("%streak free%") && keyIngradient.HasValue("%alcohol-free%")) {
        Add($"ComputerCleaningKeyIngredient⸮Streak free and alcohol free");
    }
    else if (keyIngradient.HasValue("%antimicrobial finish%")) {
        Add($"ComputerCleaningKeyIngredient⸮Antimicrobial protection keeps product cleaner");
    }
    else if (keyIngradient.HasValue("%, %")) {
        Add($"ComputerCleaningKeyIngredient⸮Key ingredients: {keyIngradient.ToLower(true)}");
    }
    else if (keyIngradient.HasValue("")) {
        Add($"ComputerCleaningKeyIngredient⸮Key ingredient: {keyIngradient.ToLower(true)}");
    }
}

// --[FEATURE #5]
// --Computer cleaning pack size

// --[FEATURE #6]
// --Certification & standards

// --[FEATURE #7]
// -- Additional dimentions

// --[FEATURE #8]
// -- Additional
void ComputerCleaningAntiStatic() {
    if (A[374].HasValue("%anti-static%") || A[5810].HasValue("%anti-static%") || A[6786].HasValue("%anti-static%")) {
        Add($"ComputerCleaningAntiStatic⸮Anti-static formula cleans and protects");
    }
  
}
// --[FEATURE #9]
// -- Additional

// --[FEATURE #10]
// -- Additional

// --[FEATURE #11]
// -- Additional

// --[FEATURE #12]
// -- Additional

//§§590434034623142034 end of "Computer Cleaning & Maintenance"