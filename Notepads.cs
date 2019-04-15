
//§§1676393410999220835 -- "Notepads" "Alex K."

NotepadTypeAndUse();
NotepadsSheetDimension();
NumberSheetsPerPadAndPerforation();
// Pack Size
BindingPosition();
// Rule type -- All
TrueColorAndCoverMateral();
// "Bullet 8" "Recycled Content"

// Recycled Content
HolePunchedNotepad();
// acid free
FormsPerBook();

// "Bullet 1" "Notepad type & Use"

void NotepadTypeAndUse() {
    var result = "";
    var type = !(R("SP-23332") is null) || !R("SP-23332").Text.Equals("<NULL>") ? R("SP-23332").Text : 
    !(R("cnet_common_SP-23332") is null) || !R("cnet_common_SP-23332").Text.Equals("<NULL>") ? R("cnet_common_SP-23332").Text : "";

    //Graph Pad|Memo Pad|Message Pad|Notepad|Steno Pad|Notepad &amp; Refill|Refill
    if (!String.IsNullOrEmpty(type)) {
        if(type.ToLower().Equals("graph pad")) {
            result = "Graph pad is great for scale, drawings, drafting, planning, engineering and technical applications";
        }
        else if(type.ToLower().Equals("memo pad")) {
            result = "Memo pad is ideal when you need to write down important information at a remote meeting or job site";
        }
        else if(type.ToLower().Equals("message pad")) {
            result = "This message pad helps you to quickly organize, distribute, and display urgent messages";
        }
        else if(type.ToLower().Equals("steno pad")) {
            result = "Steno Pad for general office note taking and shorthand applications";
        }
        else if(type.ToLower().Equals("notepad")) {
            result = "This notepad is perfect for taking notes and writing down appointments";
        }
        else {
            result = $"This {type.ToLower()} is perfect for taking notes";
        }
    } 
    if (!String.IsNullOrEmpty(result)) {
        Add($"NotepadTypeAndUse⸮{result}");
    }   
}

// "Bullet 2" "Sheet Dimension"	


void NotepadsSheetDimension() {
    var result = "";
    var cnetSize = !String.IsNullOrEmpty(cnet_sheet_dimension_notebooks_notepads()) ? cnet_sheet_dimension_notebooks_notepads() : "";
    if (!String.IsNullOrEmpty(cnetSize)) {
        result = "Sheet size: {cnetSize}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NotepadsSheetDimension⸮{result}");
    }
}

// "Bullet 3" "Number sheets per pad & Perforation"	

void NumberSheetsPerPadAndPerforation() {
    var result = "";
    var preforation = A[6028];
    var NumberSheetsPerPad = !(R("SP-21611") is null) || !R("SP-21611").Text.Equals("<NULL>") ? R("SP-21611").Text : 
    !(R("cnet_common_SP-21611") is null) || !R("cnet_common_SP-21611").Text.Equals("<NULL>") ? R("cnet_common_SP-21611").Text : "";

    if (!String.IsNullOrEmpty(NumberSheetsPerPad)) {
       if (preforation.HasValue()) {
           result = $"{NumberSheetsPerPad} micro-perforated sheets per notepad for tidy and efficient sheet removal";
       }
       else  {
           result = $"{NumberSheetsPerPad} sheets per pad ensure you don't run out of space for notes";
       }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NumberSheetsPerPadAndPerforation⸮{result}");
    }
}

// "Bullet 4" "Number of pads per pack"/"Pack Size"

// "Bullet 5" "Binding position" 

void BindingPosition() {
    var result = "";
    var bound = A[6016];
    var bindingType = A[6017];
    if (bound.HasValue() && bindingType.HasValue()) {
        var tmp = bound.FirstValue().ToString()[0].ToString().ToUpper() + bound.FirstValue().Replace(" bound", "").ToString().Substring(1);
        result = $"{tmp} {bindingType.FirstValue().ToString().ToLower()} design keeps sheets secure and easily accessible";
    }
    else if (bound.HasValue()) {
        var tmp = bound.FirstValue().ToString()[0].ToString().ToUpper() +  bound.FirstValue().ToString().Substring(1);
        result = $"{tmp} design lets you quickly flip through papers to find information";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BindingPosition⸮{result}");
    }
}	

// "Bullet 6" "Rule type"	


// "Bullet 7" "True color & Notepad Cover Material" 	
void TrueColorAndCoverMateral() {
    //Gregg|Unruled|Law|Narrow|Graph|Dotted|Cornell|College|Wide|Quad|Pitman
    var result = "";
    var trueColor = !(R("SP-22967") is null) || !R("SP-22967").Text.Equals("<NULL>") ? R("SP-22967").Text : 
    !(R("cnet_common_SP-22967") is null) || !R("cnet_common_SP-22967").Text.Equals("<NULL>") ? R("cnet_common_SP-22967").Text : "";
    var coverMaterial = !(R("SP-24688") is null) || !R("SP-24688").Text.Equals("<NULL>") ? R("SP-24688").Text : 
    !(R("cnet_common_SP-24688") is null) || !R("cnet_common_SP-24688").Text.Equals("<NULL>") ? R("cnet_common_SP-24688").Text : "";
    if (!String.IsNullOrEmpty(trueColor) && !String.IsNullOrEmpty(coverMaterial)) {
        result = $"Comes in {trueColor.ToLower()} and {coverMaterial} cover";
    }
    else if (!String.IsNullOrEmpty(trueColor)) {
        result = $"Comes in {trueColor.ToLower()}";
    }
    else if (!String.IsNullOrEmpty(coverMaterial)) {
        var tmp = coverMaterial[0].ToString().ToUpper() + coverMaterial.Substring(1);
        result = $"{tmp} cover";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"TrueColorAndCoverMateral⸮{result}");
    }
}	

// "Bullet 8" "Recycled Content"	


// Additional acid free

// Additional hole-punched design

void HolePunchedNotepad() {
    var result = "";
    var holePunched = A[6562];
    var numOfholes = A[6566];
    if (holePunched.HasValue("Yes") && numOfholes.HasValue("3")) {
        result = "A three-hole punched design lets you easily integrate the pad or individual sheets into a binder for archiving and storage";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"HolePunchedNotepad⸮{result}");
    }
}	

// Additional froms per book
void FormsPerBook() {
    var result = "";
    var numOfForms = A[6053];
   
    if (numOfForms.HasValue()) {
        result = $"{numOfForms.FirstValue()} total forms per book to record ample messages";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"FormsPerBook⸮{result}");
    }
}	
