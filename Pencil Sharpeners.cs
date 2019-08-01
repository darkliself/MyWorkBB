//§§11110715110300 "Pencil Sharpeners" "Alex K."
PencilSharpenerTypeUse();
PencilSharpenerWorkload();
// TrueColorMaterial
PencilSharpenerMountType();
PencilSharpenerSelectorDial();
PencilSharpenerAutoShutOff();
// PackSize
PencilSharpenerAntimicrobial();
PencilSharpenerHardenedHelicalCutter();
PencilSharpenerPencilSaver();
PencilSharpenerOverheatProtection();
PencilSharpenerSafeStart();
// Warranty


// --[FEATURE #1]
// -- Pencil sharpener type & Use
void PencilSharpenerTypeUse() {
    // Electric|Manual|Battery Powered
    var type = GetReferenceBase("SP-22593");
    if (type.HasValue("Electric")) {
        Add($"PencilSharpenerTypeUse⸮Electric pencil sharpener provides clean, precise sharpening");
    }
    else if (type.HasValue("Manual")) {
        Add($"PencilSharpenerTypeUse⸮Manual pencil sharpener is quick and convenient for anywhere use");
    }
    else if (type.HasValue("Battery Powered")) {
        Add($"PencilSharpenerTypeUse⸮Battery-powered pencil sharpener is ideal for high-quality clean line sharpening");
    }
}
// --[FEATURE #2]
// -- Workload
void PencilSharpenerWorkload() {
    var duty = GetReferenceBase("SP-23550");
    if (duty.HasValue("Light-duty")) {
        Add($"PencilSharpenerWorkload⸮Pencil sharpener is ideal for light-duty pencil sharpening");
    }
    else if (duty.HasValue("Heavy-duty")) {
        Add($"PencilSharpenerWorkload⸮Heavy-duty versatile sharpener");
    }
    else if (duty.HasValue("%Medium%Duty%")) {
        Add($"PencilSharpenerWorkload⸮Pencil sharpener usage type: medium-duty");
    }
}
// --[FEATURE #3]
// -- Material of item & Product Color

// --[FEATURE #4]
// -- Pencil sharpener mount type
 void PencilSharpenerMountType() {
    var mountType = GetReferenceBase("SP-16315");
    if (mountType.HasValue("Desktop")) {
        Add($"PencilSharpenerMountType⸮Desktop pencil sharpener for your convenience");
    }
    else if (mountType.HasValue("Wall")) {
        Add($"PencilSharpenerMountType⸮Wall-mountable pencil sharpener");
    }
    else if (mountType.HasValue("Handheld")) {
        Add($"PencilSharpenerMountType⸮Handheld pencil sharpener");
    }
}
// --[FEATURE #5]
// -- Pencil sharpener selector dial
void PencilSharpenerSelectorDial() {
    var holes = GetReferenceBase("SP-18669");
    if (holes.HasValue("1 %")) {
        Add($"PencilSharpenerSelectorDial⸮One-hole pencil sharpener is quick and convenient for use anywhere");
    }
    else if (holes.HasValue("2 %") || holes.HasValue("3 %") || holes.HasValue("4 %") ) {
        Add($"PencilSharpenerSelectorDial⸮{holes.Replace(" holes", "-hole")} dial accommodates various pencil widths");
    }
    else if (holes.HasValue()) {
        Add($"PencilSharpenerSelectorDial⸮Features a {holes.Replace(" holes", "-hole")} dial which conveniently sharpens assorted pencil widths");
    }
}
// --[FEATURE #6]
// -- Pencil sharpener auto-shut off feature (If Applicable)
 void PencilSharpenerAutoShutOff() {
    if (GetReferenceBase("SP-16316").HasValue("Yes")) {
        Add($"PencilSharpenerAutoShutOff⸮Auto shut off when pencil is completely sharpened");
    }
}
// --[FEATURE #7]
// -- Pack Size (If more than 1)

// --[FEATURE #8]
// -- Additional antimicrobial
void PencilSharpenerAntimicrobial() {
    if (A[6189].HasValue("%antimicrobial%")) {
        Add($"PencilSharpenerAntimicrobial⸮Features antimicrobial protection for the life of the product");
    }
}
// --[FEATURE #9]
// -- Additional hardened helical cutter
void PencilSharpenerHardenedHelicalCutter() {
    if (A[6189].HasValue("%hardened helical cutter%")) {
        Add($"PencilSharpenerHardenedHelicalCutter⸮Hardened helical cutter for maximum precision and durability");
    }
}

// --[FEATURE #10]
// -- Additional PencilSaver
void PencilSharpenerPencilSaver() {
    if (A[6189].HasValue("%PencilSaver%")) {
        Add($"PencilSharpenerPencilSaver⸮Pencil saver technology prevents oversharpening");
    }
}

// --[FEATURE #11]
// --Additional overheat protection
void PencilSharpenerOverheatProtection() {
    if (A[6189].HasValue("overheat protection")) {
        Add($"PencilSharpenerOverheatProtection⸮Overheat protection prevents motor from overheating");
    }
}

// --[FEATURE #11]
// --Additional SafeStart
void PencilSharpenerSafeStart() {
    if (A[6189].HasValue("SafeStart")) {
        Add($"PencilSharpenerSafeStart⸮SafeStart system prevents sharpening until receptacle is in place");
    }
}

// --[FEATURE #11]
// --Additional

// --[FEATURE #12]
// -- Warranty

//§§11110715110300 end of "Pencil Sharpeners"