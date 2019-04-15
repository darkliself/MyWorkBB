//§13529397020006 -- "Binder Accessories" "Alex K."

BinderAccessoryTypeAndUse();
// True color & Material of Item
// Dimensions
TabIndexPunchInformation();
// Pack Size
ArchivalSafe();

// "Bullet 1" "Binder Accessory Type & Use"
void BinderAccessoryTypeAndUse() {
    var result = "";
    //Binder Pockets|Spine Inserts|Tab Dividers|Job Ticket Holder|Sheet Lifter|Index Tabs|Zipper Pouch|Fasteners|Sheet Protectors|File Folder Frames|Reinforcement Labels
    var type = !(R("SP-18542") is null) || !R("SP-18542").Text.Equals("<NULL>") ? R("SP-18542").Text : 
    (R("cnet_common_SP-18542") is null) || !R("cnet_common_SP-18542").Text.Equals("<NULL>") ? R("cnet_common_SP-18542").Text : "";
    if (!String.IsNullOrEmpty(type)) {
        switch(Coalesce(type.ToLower())) {
            case var a when a.HasValue("<null>"):
                break;
            case var a when a.HasValue("sheet protectors"):
                result = "Sheet protectors to prevent degradation of paper inserts";
                break;
           case var a when a.HasValue("tab dividers"):
                result = "Tab dividers let you organize projects, personal documents, and more";
                break;
            case var a when a.HasValue("reinforcement labels"):
                result = "For reinforcing or repairing punched holes in loose-leaf paper";
                break;
            case var a when a.HasValue("binder pockets"):
                result = "Convenient pockets help you organize your binder";
                break;
            case var a when a.HasValue("document holder"):
                result = "Document holder to keep the documents you're working with secure and easy to see";
                break;
            case var a when a.HasValue("file folder frames"):
                result = "File folder frame expands your filing system";
                break;
            case var a when a.HasValue("file pockets"):
                result = "File pockets to sort your documents by topic";
                break;
            case var a when a.HasValue("index tabs"):
                result = "Organize your work and make specific pages easier to find with index tabs";
                break;
            case var a when a.HasValue("job ticket holder"):
                result = "Job ticket holder is ideal for storing documents, files and presentation papers";
                break;
            case var a when a.HasValue("sheet lifter"):
                result = "Sheet lifters keep file pages flat and free from curling";
                break;
            case var a when a.HasValue("card protectors"):
                result = "Card Protectors are perfect for storing cards with more protection";
                break;
            case var a when a.HasValue("zipper pouch"):
                result = "Zipper pouch is designed to store items and documents";
                break;
            case var a when a.HasValue("usb pockets"):
                result = "USB pockets hold and protect your USB drive";
                break;
            case var a when a.HasValue("spine inserts"):
                result = "Label binder spine inserts for organized and professional look";
                break;
            case var a when a.HasValue():
                var tmp = a.Text.First().ToString().ToUpper() + a.Text.Substring(1);
                result = $"{tmp} for organized and professional look";
                break;
        }
    }
    if (!String.IsNullOrEmpty(result)) {
       Add($"BinderAccessoryTypeAndUse⸮{result}"); 
    }
}

// "Bullet 2" "True color & Material of Item"

// "Dimensions" "Bullet 3"

// "Bullet 4" "Tab/Index/Punch Information"

void TabIndexPunchInformation() {
    var result = "";
    var tab = A[5947];
    var numOfPunches = A[5940];
    var numOfHoles = A[6548];
    var partsQty = A[5946];
    if (Coalesce(tab.Where("%A-Z%")).HasValue()) {
        result = "A-Z dividers for alphabetical organization";
    }
    else if (Coalesce(tab.Where("Jan%Dec%", "Jan%Dez%", "Jan%Dec%", "Jan%Déc%")).HasValue()) {
        result = "Monthly tab dividers for impactful organizing";
    }
    else if (Coalesce(tab.Where("Mon-Sun", "Mon-Fri", "Mon-Sat")).HasValue()) {
        var tmp = tab.HasValue("Mon-Sun") ? "sunday" : tab.HasValue("Mon-Fri") ? "friday" : tab.HasValue("Mon-Sat") ? "saturday" : "";
        result = $"Organize your documents quickly and easily monday through {tmp}";
    }
    else if (numOfPunches.HasValue() && numOfPunches.Values.ExtractNumbers().Any()) {
        var tmp = numOfPunches.Values.ExtractNumbers().First()
            .Replace("2", "Two")
            .Replace("3", "Three")
            .Replace("4", "Four")
            .Replace("5", "Five")
            .Replace("6", "Six")
            .Replace("7", "Seven")
            .Replace("8", "Eght")
            .Replace("9", "Nine");
        result = $"{tmp}-hole punched and ready for use";
        if (partsQty.HasValue() && partsQty.FirstValue().ExtractNumbers().Any()) {
            result = $"{result}; {partsQty.FirstValue().ExtractNumbers().First()}-tab dividers";
        }
    }
    else if (numOfHoles.HasValue() && numOfHoles.Values.ExtractNumbers().First() > 1) {
        var tmp = numOfHoles.Values.ExtractNumbers().First()
            .Replace("2", "Two")
            .Replace("3", "Three")
            .Replace("4", "Four")
            .Replace("5", "Five")
            .Replace("6", "Six")
            .Replace("7", "Seven")
            .Replace("8", "Eght")
            .Replace("9", "Nine");
        result = $"{tmp}-hole punched and ready for use";
        if (partsQty.HasValue() && partsQty.FirstValue().ExtractNumbers().Any()) {
            result = $"{result}; {partsQty.FirstValue().ExtractNumbers().First()}-tab dividers";
        }    
    }
    else if (partsQty.HasValue() && partsQty.FirstValue().ExtractNumbers().First() > 1) {
        var tmp = partsQty.FirstValue().ExtractNumbers().First()
            .Replace("2", "Two")
            .Replace("3", "Three")
            .Replace("4", "Four")
            .Replace("5", "Five")
            .Replace("6", "Six")
            .Replace("7", "Seven")
            .Replace("8", "Eght")
            .Replace("9", "Nine");
        result = $"{tmp}-tab dividers";
    }    
    if (!String.IsNullOrEmpty(result)) {
       Add($"TabIndexPunchInformation⸮{result}"); 
    }
}
// "Bullet 5" "Pack Size"

// "Additional Archival Safe"
void ArchivalSafe() {
    var result = "";
    var archivalSafe = Coalesce(A[5945].Where("archival safe"));
    if (archivalSafe.HasValue()) {
        result = "Archival-safe for long-lasting protection";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalArchivalSafe⸮{result}");
    }
}

//§13529397020006 end of "Binder Accessories" 