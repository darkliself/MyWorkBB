//§§1684381310759142175 "Disposable Plates" "Alex K."
DisposablePlateType();
DisposableDesignColorMaterial();
// Dimensions
// PackSize
Microwaveability();
// RecycledContent
// PostConsumerContent
DisposablePlatesCapacity();
NumberOfDisposablePlates();
FreezerSafe();

// --[FEATURE #1]
// -- Disposable plate type
void DisposablePlateType() {
    // Sets|Platters|Lids|Plates|Trays
    var type = GetReferenceBase("SP-22650");
    if (type.HasValue("Sets")) {
        Add($"DisposablePlateType⸮These set is great for parties and other events");
    }
    else if (type.HasValue("Platters")) {
        Add($"DisposablePlateType⸮Enhance the food presentation at your next celebration with platters");
    }
    else if (type.HasValue("Lids")) {
        Add($"DisposablePlateType⸮Lid ensures that spillage of food is prevented by fitting your dinnerware completely");
    }
    else if (type.HasValue("Plates")) {
        Add($"DisposablePlateType⸮Plates will save you from the hassle of washing dishes");
    }
    else if (type.HasValue("Trays")) {
        Add($"DisposablePlateType⸮Lid ensures that spillage of food is prevented by fitting your dinnerware completely");
    }
    else if (type.HasValue()) {
        Add($"DisposablePlateType⸮These {type.ToLower()} are great for parties and other events");
    }
}
// --[FEATURE #2]
// -- True color (or design) & Material
void DisposableDesignColorMaterial() {
    var trueColor = GetReferenceBase("SP-22967");
    var material = GetReferenceBase("SP-17447");
    if (trueColor.HasValue() && material.HasValue() && A[6799].HasValue()) {
        Add($"DisposableDesignColorMaterial⸮{A[6799].FirstValueOrDefault().ToUpperFirstChar()} shape, comes in {trueColor.ToLower()} and is made of {material.ToLower(true)}");
    }
    else if (material.HasValue() && A[6799].HasValue()) {
        Add($"DisposableDesignColorMaterial⸮{A[6799].FirstValueOrDefault().ToUpperFirstChar()} shape and is made of {material.ToLower(true)}");
    }
    else if (trueColor.HasValue() && A[6799].HasValue()) {
        Add($"DisposableDesignColorMaterial⸮{A[6799].FirstValueOrDefault().ToUpperFirstChar()} shape, comes in {trueColor.ToLower()}");
    }
    // if no shape then called standard bullet TrueColorMaterial
}
// --[FEATURE #3]
// -- Diameter or Length x width

// --[FEATURE #4]
// -- Plates per Pack (if multipack then also Packs per Case)

// --[FEATURE #5]
// -- Microwaveability
void Microwaveability() {
    if (A[10039].HasValue("Yes")) {
        Add($"Microwaveability⸮Microwave-safe for reheating");
    }
}
// --[FEATURE #6]
// -- Recycled Info (if applicable)

// --[FEATURE #7]
// -- Additional PostConsumerContent

// --[FEATURE #8] 
// -- Additional Capacity (oz.) 
void DisposablePlatesCapacity() {
    if (A[6803].HasValue() && A[6803].Units.First().Name == "ml") {
        Add($"DisposablePlatesCapacity⸮Capacity: {Math.Round(A[6803].FirstValueOrDefault() * 0.033814, 2)} oz.");
    }
    else if (A[6803].HasValue() && A[6803].Units.First().Name == "l") {
        Add($"DisposablePlatesCapacity⸮Capacity: {Math.Round(A[6803].FirstValueOrDefault() * 33.814, 2)} oz.");
    }
    else if (A[6803].HasValue() && A[6803].Units.First().Name == "cl") {
        Add($"DisposablePlatesCapacity⸮Capacity: {Math.Round(A[6803].FirstValueOrDefault() * 0.33814, 2)} oz.");
    }
}
// --[FEATURE #9]
// -- Additional Number of disposable plates    
void NumberOfDisposablePlates() {
    var count = GetReferenceBase("SP-467");
    if (count.HasValue()) {
        Add($"NumberOfDisposablePlates⸮This case of {count} provides multi-event use");
    }
}
// --[FEATURE #10]
// -- Additional Freezer Safe

void FreezerSafe() {
    if (A[7832].HasValue()) {
        Add($"FreezerSafe⸮Freezer safe");
    }
}

// --[FEATURE #11]
// -- Additional

// --[FEATURE #12]
// -- Additional

//§§1684381310759142175 end of "Disposable Plates"