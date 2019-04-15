
//§1676378310636165557 -- "Notebooks" "Alex K."

NotebookTypeAndUse();
NumSheetDimension();
RuleType();
//"Bullet 4" "True color"
NotebookCoverMaterial();
Perforation();
NotebookBinding();
// "Pack size"
// "Recycled Content"
// "Additional Acid Free"
RemovableDividers();

// "Bullet 1" "Notebook Type & Use"
void NotebookTypeAndUse() {
    var result = "";
    var manufacturerProductType = A[6014];
    var brand = A[606];
    var numberOfSubjects = "";//R("SP-21618").Text;
    var notebookType = "";//R("SP-18656").Text;
    if (manufacturerProductType.HasValue("%business%")) {
        result = "Business notebook helps you easily keep notes organized and in one place";
    }
    else if (manufacturerProductType.HasValue("arc customizable notebook")) {
        result = "Keep your essentials in one convenient place with the Arc customizable notebook system";
    }
    else if (manufacturerProductType.HasValue("%notebook%") && brand.HasValue("Cambridge")) {
        result = "Cambridge notebook that keeps up with your professional look and style";
    }
    else if (manufacturerProductType.HasValue("composition notebook")) {
        result = "This secure-bound composition notebook is ideal for taking notes";
    }
    else if (!String.IsNullOrEmpty(numberOfSubjects)) {
        var tmp = numberOfSubjects.Replace("1", "One")
            .Replace("2", "Two")
            .Replace("3", "Three")
            .Replace("4", "Four")
            .Replace("5", "Five")
            .Replace("6", "Six")
            .Replace("7", "Seven")
            .Replace("8", "Eight")
            .Replace("9", "Nine");
        result =  $"{tmp}-subject notebook is great for school, home, or work projects";
    }
    else if (manufacturerProductType.HasValue("composition book")) {
        result = "Composition book is great for note-taking in class or at home";
    }
    else if (!String.IsNullOrEmpty(notebookType)) {
        var tmp  = notebookType[0].ToString().ToUpper() + notebookType.Substring(1);
        result = $"{tmp}  are a great place to take notes and stay organized";
    }
    else if (manufacturerProductType.HasValue("ark refill")) {
        result = "Arc refill paper";
    }
    
    else if (manufacturerProductType.HasValue()) {
        var tmp = manufacturerProductType.FirstValue().ToString();
        result = $"{tmp[0].ToString().ToUpper()}{tmp.Substring(1)} is a great place to take notes and stay organized";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NotebookTypeAndUse⸮{result}");
    }
}

// return value in XX"W x YY"H format
string cnet_sheet_dimension_notebooks_notepads() {
    var result = "";
    var sheetDimention = !(R("SP-18229") is null) || !R("SP-18229").Text.Equals("<NULL>") ? R("SP-18229").Text : 
    !(R("cnet_common_SP-18229") is null) || !R("cnet_common_SP-18229").Text.Equals("<NULL>") ? R("cnet_common_SP-18229").Text : "";
    var paperFormat = Coalesce(A[6018], A[6069]);
    if (!String.IsNullOrEmpty(sheetDimention) && !sheetDimention.ToLower().Equals("other")) {
        var tmp = Coalesce(sheetDimention).RegexReplace(@"(\d*"")(\sx\s)(\d*"")", "$1H$2$3L" );
        result = tmp;
    }
    else if (paperFormat.HasValue()) {
        if (paperFormat.FirstValueUsm().ExtractNumbers().Count() == 2) {
            var num1 = Math.Round((double)paperFormat
                .FirstValueUsm()
                .Replace("0.02", "")
                .Replace(".51", ".5")
                .ExtractNumbers()[0], 2);
            var num2 = Math.Round((double)paperFormat
                .FirstValueUsm()
                .Replace("0.02", "")
                .Replace(".51", ".5")
                .ExtractNumbers()[1], 2);
            result = $"{num1}\"W x {num2}\"H";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        return result;
    } else {
        return "";
    }
}

// "Bullet 2" "Number of Sheets and Sheet Dimension"
void NumSheetDimension() {
    var result = "";
    var cnetSize = !String.IsNullOrEmpty(cnet_sheet_dimension_notebooks()) ? cnet_sheet_dimension_notebooks_notepads() : "";
    var sheetsPerPad = !(R("SP-21611") is null) || !R("SP-21611").Text.Equals("<NULL>") ? R("SP-21611").Text : 
    !(R("cnet_common_SP-21611") is null) || !R("cnet_common_SP-21611").Text.Equals("<NULL>") ? R("cnet_common_SP-21611").Text : "";
    if (!String.IsNullOrEmpty(cnetSize)) {
        if (!String.IsNullOrEmpty(sheetsPerPad)) {
            result = $"This {cnetSize} notebook has {sheetsPerPad} sheets";
        } else {
            result = $"Sheet size: {cnetSize}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NumSheetDimension⸮{result}");
    }
}

// "Bullet 3" "Rule Type"
void RuleType() {
    //Gregg|Unruled|Law|Narrow|Graph|Dotted|Cornell|College|Wide|Quad|Pitman
    var result = "";
    var rulingType = A[6023];
    var ruleType = !(R("SP-18657") is null) || !R("SP-18657").Text.Equals("<NULL>") ? R("SP-18657").Text : 
    !(R("cnet_common_SP-18657") is null) || !R("cnet_common_SP-18657").Text.Equals("<NULL>") ? R("cnet_common_SP-18657").Text : "";
    if (rulingType.HasValue("%squared%")) {
        var tmp  = "";
        if (CAT.MainAlt.Key.HasValue("14872")) {
            tmp = "notepad";
        } else {
            tmp = "notebook";
        }
        result = $"Solve mathematical equations or create a scale drawing using a quadrille-ruled {tmp}";
    }
    else if (rulingType.HasValue("%Legal - ruled%")) {
        result = "Legal-ruled paper keeps handwriting neat";
    }
    else if (!String.IsNullOrEmpty(ruleType)) {
        if (ruleType.ToLower().Equals("college")) {
            result = "College-ruled for efficient use of space";
        }
        else if (ruleType.ToLower().Equals("narrow")) {
            result = "The narrow-ruled format provides plenty of space for your notes";
        }
        else if (ruleType.ToLower().Equals("wide")) {
            result = "The wide-ruled paper makes it easy to write legible notes that aren't too cramped";
        }
        else if (!ruleType.ToLower().Equals("unruled")) {
            result = $"Rule type: {ruleType.ToLower()}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"RuleType⸮{result}");
    }
}	

   
//"Bullet 4" "True color" need to do this

//"Bullet 5" "Notebook Cover Material"

void NotebookCoverMaterial() {
    var result = "";
    var material = A[6024];
    var coverMateral =!R("SP-24138").Text.Equals("<NULL>") ? R("SP-24138").Text : !R("cnet_common_SP-24138").Text.Equals("<NULL>") ? R("cnet_common_SP-24138").Text : "";
    if (material.HasValue("%poly%")) {
        result = "Durable polypropylene covers protect sheets from damage";
    }
    else if (material.HasValue("%cardboard%")) {
        result = "Sturdy cardboard allows you to write without a desk surface";
    }
    else if (material.HasValue("pressboard")) {
        result = "Pressboard covers are durable and keep your notes private";
    }
    else if (!String.IsNullOrEmpty(coverMateral)) {
        result = $"{coverMateral.Replace(" cover", "")} cover";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NotebookCoverMaterial⸮{result}");
    }
}

// "Bullet 6" "Perforation"
void Perforation() {
    var result = "";
    var material = A[6028];
    if (material.HasValue("Yes")) {
        result = "Micro-perforated sheets for neat and easy sheet removal";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"Perforation⸮{result}");
    }
}

// "Bullet 7" "Notebook Binding"
void NotebookBinding() {
    var result = "";
    var bindingType = A[6017];
    //Tape|Thermal|Velobind|Spiral|Sewn
    var notebookBinding = !R("SP-24139").Text.Equals("<NULL>") ? R("SP-24139").Text : !R("cnet_common_SP-24139").Text.Equals("<NULL>") ? R("cnet_common_SP-24139").Text : "";
    
    if (bindingType.HasValue("casebound")) {
        result = "Case-bound construction is resilient";
    }
    else if (bindingType.HasValue("spiral-bound")) {
        result = "Spiral-bound design for easy access of sheets inside";
    }
    else if (bindingType.HasValue("wire-bound")) {
        result = "Wire binding allows book to lie flat on desk surface for maximum writing area";
    }
    else if (!String.IsNullOrEmpty(notebookBinding)) {
        if (notebookBinding.ToLower().Equals("tape")) {
            result = "Tape bound for easy, long-lasting use";
        }
        else if (notebookBinding.ToLower().Equals("sewn")) {
            result = "Sewn binding, so pages won't become loose";
        }
        else {
            result = $"Notebook binding: {notebookBinding.ToLower()}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NotebookBinding⸮{result}");
    }
}

// "Bullet 8" "Pack size"

// "Bullet 9" "Recycled Content"

// "Additional" "Acid Free" need to do this

// "Additional" "Removable dividers"

void RemovableDividers() {
    var result = "";
    var checkedVal = A[6031];
    if (checkedVal.HasValue("removable dividers")) {
        result = "Removable dividers allow you to create the number of subjects you need";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalRemovableDividers⸮{result}");
    }
}

// "Additional" "Pen Holder"
PenHolder();
void PenHolder() {
    var result = "";
    var checkedVal = A[6031];
    if (checkedVal.HasValue("pen holder")) {
        result = "Exterior pen loop ensures your pen is always within reach for quick, convenient use";
    } 
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalPenHolder⸮{result}");
    }
}

//§1676378310636165557 end of "Notebooks"