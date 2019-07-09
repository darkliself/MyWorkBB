//§§23210552166358 "Stacking & Folding Chairs" "Alex K."
TypeOfChairAndUse();
TrueColorAndUpholsteryMaterial();
// Dimensions
// SeatDimension -- all
// Pack Size
// ArmType -- all
CapacityTime();
// TypeOfSupportAndErgonomic
// Assembly required -- all
// Standards
// { Addional features from Design
    LumbarSupportTiltHeight();
    WaterFallSeatEdge();
// }




// TiltMechanismType -- all
// Warranty



// --[FEATURE #1]
// -- Type of Chair & Use
void TypeOfChairAndUse() {
    //Office|Student/School|Banquet/Reception
    var type = GetReferenceBase("SP-18239");
    if (A[7156].HasValue() && type.HasValue()) {
        Add($"TypeOfChairAndUse⸮{type.ToLower().ToUpperFirstChar()} {A[7156].FirstValueOrDefault().ToLower()} adds extra seating to any family or work function");
    }
}
// --[FEATURE #2]
// -- True color & Upholstery material (Note: if back upholstery and seat upholstery differ include both within this bullet)
void TrueColorAndUpholsteryMaterial() {
    //Office|Student/School|Banquet/Reception
    var seatMaterial = GetReferenceBase("SP-522");
    var backMaterial = GetReferenceBase("SP-524");
    var trueColor = GetReferenceBase("SP-22967");
    if (seatMaterial.HasValue() && backMaterial.HasValue() && trueColor.HasValue() 
    && !seatMaterial.HasValue($"%{backMaterial.Text}%") && !backMaterial.HasValue($"%{seatMaterial.Text}%")) {
        Add($"TrueColorAndUpholsteryMaterial⸮Comes in {trueColor.ToLower()} and is made of {seatMaterial.ToLower(true)} seat with {backMaterial.ToLower(true)} back");
    }
    else if (seatMaterial.HasValue() && backMaterial.HasValue()) {
        Add($"TrueColorAndUpholsteryMaterial⸮Made of {seatMaterial.ToLower(true)} seat with {backMaterial.ToLower(true)} back");
    }
    else if (seatMaterial.HasValue() && trueColor.HasValue() 
    && !seatMaterial.HasValue($"%{backMaterial.Text}%") && !backMaterial.HasValue($"%{seatMaterial.Text}%")) {
        Add($"TrueColorAndUpholsteryMaterial⸮Comes in {trueColor.ToLower()} and is made of {seatMaterial.ToLower(true)}");
    }
    else if (trueColor.HasValue() && backMaterial.HasValue()) {
        Add($"TrueColorAndUpholsteryMaterial⸮Comes in {trueColor.ToLower()} and is made of {backMaterial.ToLower(true)}");
    }
    else if (trueColor.HasValue()) {
        Add($"TrueColorAndUpholsteryMaterial⸮Comes in {trueColor.ToLower()}");
    }
    else if (seatMaterial.HasValue()) {
        Add($"TrueColorAndUpholsteryMaterial⸮Made of {seatMaterial.ToLower(true)}");
    }
    else if (backMaterial.HasValue()) {
        Add($"TrueColorAndUpholsteryMaterial⸮Made of {backMaterial.ToLower(true)}");
    }
}


// --[FEATURE #3]
// -- Overall Dimensions (in Inches): Height (should include range) x Width x Depth
// Dimensions

// --[FEATURE #4]
// -- Seat Dimension (in Inches): Width x Depth
// SeatDimension -- all

// --[FEATURE #5]
// -- Back Dimensions (in Inches): Width x Depth
// BackDimension -- all

// --[FEATURE #6]
// -- Pack Size if more than  1

// --[FEATURE #7]
// -- Arm Type (if applicable)

// --[FEATURE #8]
// -- Capacity in lbs. as well as time (if available)
void CapacityTime() {
    var ratedLevelOfUse = GetReferenceBase("SP-1099");
    if (ratedLevelOfUse.HasValue() && A[7171].HasValue() && A[7171].Units.First().Name == "kg") {
        Add($"CapacityTime⸮Weight is rated up to {Math.Round(A[7171].FirstValueOrDefault() * 2.20462, 0)} lbs. for up to {ratedLevelOfUse.ToLower()} of use per day");
    }
    else if (ratedLevelOfUse.HasValue() && A[7171].HasValue() && A[7171].Units.First().Name == "g") {
        Add($"CapacityTime⸮Weight is rated up to {Math.Round(A[7171].FirstValueOrDefault() * 2.20462, 0)} lbs. for up to {ratedLevelOfUse.ToLower()} of use per day");
    }
    else if (A[7171].HasValue() && A[7171].Units.First().Name == "kg") {
        Add($"CapacityTime⸮Weight is rated up to {Math.Round(A[7171].FirstValueOrDefault() * 0.00220461999989109, 2)} lbs.");
    }
    else if (A[7171].HasValue() && A[7171].Units.First().Name == "g") {
         Add($"CapacityTime⸮Weight is rated up to {Math.Round(A[7171].FirstValueOrDefault() * 0.00220461999989109, 2)} lbs.");
    }
}

// --[FEATURE #9]
// -- Type of support and Ergonomic Construction/Design
// All

// --[FEATURE #10]
// -- Assembly required
// Assembly required - all

// --[FEATURE #11]
// -- Certifications & Standards (ANSI/BIFMA)

void LumbarSupportTiltHeight() {
    if (A[7199].HasValue("Yes") && A[7188].HasValue("Yes") && A[7188].HasValue("Yes") && A[7188].HasValue("Yes")) {
        Add($"LumbarSupportTiltHeight⸮Lumbar support, seat height adjustment with tilt tension and tilt lock offer customized comfort");
    }
}

void WaterFallSeatEdge() {
    if (A[7176].HasValue("waterfall edge")) {
        Add($"WaterFallSeatEdge⸮Waterfall seat edge promotes better leg circulation");
    }
}

// --[FEATURE #12]
// --Warranty

//§§23210552166358 end of "Stacking & Folding Chairs"