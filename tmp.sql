//§§5585232188142681 "Business Envelopes" "Alex K."
UseOfEnvelope();
TrueColorMaterialPaperWeight();
// EnvelopeSize - All
ClosureTypeFlapStyle();
InteriorCushioning();
SecurityTinted();
// PackSize
// PostConsumerContent
// RecycledContent
AdditionalBusinessEnvelopesProtection();
AdditionalBusinessEnvelopesFeatures();
AdditionalBusinessEnvelopesOpeningStyle();

// --[FEATURE #1]
// -- Use of envelope
void UseOfEnvelope() {
    if (A[6481].HasValue("envelope") && A[6514].HasValue()) {
        Add($"UseOfEnvelope⸮{A[6514].FirstValueOrDefault().Replace("No. ","#").Replace("size ", "")} business envelopes are suitable for many mailing projects");
    }
    else if (A[6481].HasValue()) {
        Add($"UseOfEnvelope⸮Business envelopes are suitable for many mailing projects");
    }
}
// --[FEATURE #2]
// -- True color & Material of item; including paper weight (If Applicable)
void TrueColorMaterialPaperWeight() {
    var paperWeight = GetReferenceBase("SP-21543");
    var trueColor = GetReferenceBase("SP-22967");
    var material = GetReferenceBase("SP-21408");
    // if no paperWeight uses TrueColorMaterial from common pull
    if (paperWeight.HasValue() && trueColor.HasValue() && material.HasValue()) {
        Add($"TrueColorMaterialPaperWeight⸮Made of {material.ToLower(true)} and comes in {trueColor.ToLower().Replace("assoeted", "assorted colors")}; {paperWeight} lb. paper weight");
    }
    else  if (paperWeight.HasValue() && material.HasValue()) {
        Add($"TrueColorMaterialPaperWeight⸮Made of {material.ToLower(true)}; {paperWeight} lb. paper weight");
    }
    else  if (paperWeight.HasValue() && trueColor.HasValue()) {
        Add($"TrueColorMaterialPaperWeight⸮Comes in {trueColor.ToLower().Replace("assoeted", "assorted colors")}; {paperWeight} lb. paper weight");
    }
}
// --[FEATURE #3]
// -- Envelope size
// EnvelopeSize -All

// --[FEATURE #4]
// -- Closure type & flap style
void ClosureTypeFlapStyle() {
    // V-Flap|Open End|Wallet|Square|Commercial|Rectangular
    var flapStyle = GetReferenceBase("SP-18916"); 
    // Gummed|EasyClose|Redi-Seal|Button & String|Reveal-N-Seal|Moistenable Glue|Hook & Loop|Buckle|Hook|Clasp & Moistenable Glue|Self Seal|Elastic|Zip|Clasp|Open End|Snap
    var closure = GetReferenceBase("SP-18915");

    // if no paperWeight uses TrueColorMaterial from common pull
    if (flapStyle.HasValue() && closure.HasValue()) {
        Add($"ClosureTypeFlapStyle⸮{closure.ToLower().ToUpperFirstChar()} closure with {flapStyle.ToLower(true)} flap keeps contents in place");
    }
    else if (flapStyle.HasValue()) {
        Add($"ClosureTypeFlapStyle⸮{flapStyle.ToLower(true).ToUpperFirstChar()} flap keeps contents in place");
    }
    else if (closure.HasValue()) {
        Add($"ClosureTypeFlapStyle⸮{closure.ToLower().ToUpperFirstChar()} closure keeps contents in place");
    }
}
// --[FEATURE #5]
// -- Interior cushioning (if any)
void InteriorCushioning() {
    if (A[6497].HasValue("%bubble wrap%")) {
        Add($"InteriorCushioning⸮Interior bubble wrap keeps mail cushioned during bumpy rides");
    }
}
// --[FEATURE #6]
// -- Security tinted
void SecurityTinted() {
    var tined = GetReferenceBase("SP-18919");
    if (A[6497].HasValue("Yes")) {
        Add($"SecurityTinted⸮Security-tinted pattern helps to discourage from reading sensitive information through the outside of the envelope");
    }
}


// --[FEATURE #10]
// --Additional protection
void AdditionalBusinessEnvelopesProtection() {
    if (A[6498].HasValue() && A[6500].HasValue("%resistant" , "%proof", "%repellent")) {
        Add($"AdditionalBusinessEnvelopesProtection⸮{A[6498].Where("%resistant", "%proof", "%repellent").Flatten(", ").ToLower(true).ToUpperFirstChar().Replace("resistant", "")}, {A[6500].Where("%resistant" , "%proof", "%repellent").Select(o => o.Value()).FlattenWithAnd().Replace("resistant", "proof", "").Replace("repellent")} resistant envelope provides extra security for item being shipped");
    }
    else if (A[6498].HasValue("%resistant")) {
        Add($"AdditionalBusinessEnvelopesProtection⸮{A[6498].Values.Select(o => o.Value()).FlattenWithAnd().Replace("resistant", "").ToUpperFirstChar()}-resistant envelope provides extra security for item being shipped");
    }
    else if (A[6500].HasValue("%resistant", "%proof", "%repellent")) {
        Add($"AdditionalBusinessEnvelopesProtection⸮{A[6500].Where("%resistant" , "%proof", "%repellent").Select(o => o.Value()).FlattenWithAnd().Replace("resistant", "proof", "").Replace("repellent").ToLower(true).ToUpperFirstChar()}-resistant envelope provides extra security for item being shipped");
    }
}
// --[FEATURE #11]
// --Additional
void AdditionalBusinessEnvelopesFeatures() {
    if (A[6500].HasValue("%address window%") && A[6495].HasValue("%left%")) {
        Add($"AdditionalBusinessEnvelopesFeatures⸮Simply position the address to show through the single-left window");
    }
    else if (A[6493].HasValue("1") && A[6495].HasValue("%left%")) {
        Add($"AdditionalBusinessEnvelopesFeatures⸮Simply position the address to show through the single-left window");
    }
    else if (A[6493].HasValue("2")) {
        Add($"AdditionalBusinessEnvelopesFeatures⸮Double-window envelopes conveniently display addresses");
    }
    else if (A[6500].HasValue("%address window%") && A[6495].HasValue("%right%")) {
        Add($"AdditionalBusinessEnvelopesFeatures⸮Simply position the address to show through the single-right window");
    }
    else if (A[6500].HasValue("1") && A[6495].HasValue("%right%")) {
        Add($"AdditionalBusinessEnvelopesFeatures⸮Simply position the address to show through the single-right window");
    }
    else if (A[6500].HasValue("2")) {
        Add($"AdditionalBusinessEnvelopesFeatures⸮Double-window envelopes conveniently display addresses");
    }
}
// --[FEATURE #12]
// -- Additional opening style
void AdditionalBusinessEnvelopesOpeningStyle() {
    if (A[6488].HasValue("open side" )) {
        Add($"AdditionalBusinessEnvelopesOpeningStyle⸮The flap is on the long side for easier filling");
    }
    else if (A[6488].HasValue("open end" ) || GetReferenceBase("SP-18916").HasValue("Open End")) {
        Add($"AdditionalBusinessEnvelopesOpeningStyle⸮Flap is on the short side, helping to keep things from falling out after opening");
    }
}

//§§5585232188142681 end of "Business Envelopes"