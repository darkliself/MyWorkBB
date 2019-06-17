//§§548610392096142807 "Blank Media" "Alex K."

BlankMediaTypeAndUse();
MaxSpeedTrueColorPrintable();


// --[FEATURE #1]
// --Blank media type & Use
void BlankMediaTypeAndUse() {
    // DVD-R|DVD-RW|DVD+RW|DVD+R|CD-R|DVD+R DL|Blu-Ray|Data Cartridges|DVD-RAM|CD-RW|Cassettes
    var blankMediaType = GetReferenceBase("SP-18301");
    if (blankMediaType.In("DVD-R", "DVD+R")) {
        Add($"BlankMediaTypeAndUse⸮{blankMediaType} provides ample storage space for your data");
    }
    else if (blankMediaType.In("DVD-RW", "DVD+RW")) {
        Add($"BlankMediaTypeAndUse⸮{blankMediaType} discs are ideal for data-intensive, high-performance, and rewritable data storage");
    }
    else if (blankMediaType.HasValue("Blu-Ray")) {
        Add($"BlankMediaTypeAndUse⸮Blu-ray ##Discs are an ideal personal or professional solution for storing large amounts of data");
    }
    else if (blankMediaType.HasValue("CD-R")) {
        Add($"BlankMediaTypeAndUse⸮CD-R helps you safely burn your personal and work files for convenient use anytime");
    }
    else if (blankMediaType.HasValue("CD-RW")) {
        Add($"BlankMediaTypeAndUse⸮CD-RW can be used multiple times to store and share data");
    }
    else if (blankMediaType.HasValue("Data Cartridges")) {
        Add($"BlankMediaTypeAndUse⸮Data cartridge delivers faster access time and enhanced media monitoring");
    }
    else if (blankMediaType.HasValue("%DVD-RAM")) {
        Add($"BlankMediaTypeAndUse⸮DVD-RAM with functionality similar to an external drive for data-intensive applications");
    }
     else if (blankMediaType.HasValue()) {
        Add($"BlankMediaTypeAndUse⸮{blankMediaType}");
    }
}

// --[FEATURE #2]
// --Max speed supported, True color & Printable
void MaxSpeedTrueColorPrintable() {
    
    var maxSpeed = GetReferenceBase("SP-21994");
    var trueColor = GetReferenceBase("SP-22967");
    var printableType = GetReferenceBase("SP-21995");
  
    if (maxSpeed.HasValue() && trueColor.HasValue() && printableType.HasValue()) {
        Add($"MaxSpeedTrueColorPrintable⸮{trueColor} {printableType.ToLower(true)} discs offer {maxSpeed} write speed");
    }
    else if (maxSpeed.HasValue() && printableType.HasValue()) {
        Add($"MaxSpeedTrueColorPrintable⸮{printableType.ToLower(true).ToUpperFirstChar()} discs offer {maxSpeed} write speed");
    }
    else if (maxSpeed.HasValue() && trueColor.HasValue()) {
        Add($"MaxSpeedTrueColorPrintable⸮Comes in {trueColor.ToLower(true)} with {maxSpeed} write speed for quick data transfer");
    }
    else if (trueColor.HasValue() && printableType.HasValue()) {
        Add($"MaxSpeedTrueColorPrintable⸮{printableType.ToLower(true).ToUpperFirstChar()} discs come in {trueColor.ToLower(true)}");
    }
    else if (trueColor.HasValue()) {
        Add($"MaxSpeedTrueColorPrintable⸮Comes in {trueColor.ToLower(true)}");
    }
    else if (maxSpeed.HasValue()) {
        Add($"MaxSpeedTrueColorPrintable⸮Maximum write speed of {maxSpeed} for high-speed performance");
    }
     else if (printableType.HasValue()) {
        Add($"MaxSpeedTrueColorPrintable⸮Comes with {printableType.ToLower(true)} surface");
    }
}

// --[FEATURE #3]
// --Blank media data capacity

// --[FEATURE #4]
// --Maximum video recording time (minutes)/Maximum audio recording time (minutes)

// --[FEATURE #5]
// --Compatible with

// --[FEATURE #6]
// --Blank media package type

// --[FEATURE #7]
// --Blank media grade or quality

// --[FEATURE #8]
// --Rewritable

// --[FEATURE #9]
// --Media pack size (If more than 1)

// --[FEATURE #10]
// --

// --[FEATURE #11]
// --

// --[FEATURE #12]
// --Warranty information

//§§548610392096142807 end of "Blank Media"