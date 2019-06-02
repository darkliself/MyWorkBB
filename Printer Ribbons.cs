//§§1241158330102 "Printer Ribbons" "Alex K."

// Compatibility
PrinterTrueColor();
// --Dimensions
// --Pack size
AdditionalDutyCycle();
AdditionalTypeOfUse();
//Printing Technology
AdditionalCleaningRoller();
AdditionalRemanufactured();
// Warranty

// --[FEATURE #1]
// --Compatibility

// --[FEATURE #2]
// --Printer True color (Black, Blue, Black/Red, etc.)
void PrinterTrueColor() {
    if (A[4760].HasValue("%ribbon") && A[494].HasValue("%black")) {
        Add($"PrinterTrueColor⸮Black printer ribbon provides crisp, cost-effective printing suitable for a commercial environment");
    }
    else if (A[494].HasValue() && A[494].Values.Count() == 1 && A[494].HasValue("color (%")) {
        Add($"PrinterTrueColor⸮Gives full-color prints");
    }
    else if (A[494].HasValue() && A[494].Values.Count() == 1 && A[494].HasValue("multicolor")) {
        Add($"PrinterTrueColor⸮Gives multicolor prints");
    }
    else if (A[494].HasValue() && A[494].Values.Count() == 4 
    && A[494].HasValue("black") && A[494].HasValue("cyan")
    && A[494].HasValue("magenta") &&  A[494].HasValue("yellow")) {
         Add($"PrinterTrueColor⸮Gives full-color prints");
    }
}

// --[FEATURE #3]
// --Dimensions

// --[FEATURE #4]
// --Pack size

// --[FEATURE #5]
// --Additional Duty Cycle

void AdditionalDutyCycle() {
    if (A[4785].HasValue("% images") || A[4785].HasValue("% pages")) {
        Add($"AdditionalDutyCycle⸮Prints {A[4785].FirstValue()} to maximize productivity and savings");
    }
}

// --[FEATURE #6]
// --Additional Type of Product/Use
void AdditionalTypeOfUse() {
    if (A[4760].HasValue("re-inking ribbon")) {
        Add($"AdditionalTypeOfUse⸮Re-ink printer ribbon for adding ink to compatible printers");
    }
}
// --[FEATURE #7]
// --Additional Printing Technology

// --[FEATURE #8]
// --Additional Cleaning Roller

void AdditionalCleaningRoller() {
    if (A[4760].HasValue("%with cleaning roller")) {
        Add($"AdditionalCleaningRoller⸮Comes with cleaning roller");
    }
}
// --[FEATURE #9] 
// --Additional remanufactured
void AdditionalRemanufactured() {
    if (A[5312].HasValue("remanufactured")) {
        Add($"AdditionalRemanufactured⸮Remanufactured to reduce environmental impact");
    }
}
// --[FEATURE #10]
// --Additional warranty

//§§1241158330102 end of "Printer Ribbons"